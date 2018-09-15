using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsFirstOne
{
	public class GameState
	{
		public event EventHandler<EventArgs> ReceivedSizedSizeChanged;
		public int receivedSize;
		//List of players
		public List<PlayerState> players;
		//Clear naming
		public UNOCard currentCard;
		public int chance = 0;
		//Deck of cards to send cards to client when requested
		private UNOCard[] deck;
		//Clear naming
		public Socket ServerSocket;
		//Buffer to store data
		public byte[] buffer;
		private readonly int port = 8081;
		public int ReceivedSize
		{
			get
			{
				return receivedSize;
			}
			set
			{
				receivedSize = value;
				onReceivedSizedSizeChanged();
			}
		}

		protected virtual void onReceivedSizedSizeChanged()
		{
			ReceivedSizedSizeChanged?.Invoke(this, EventArgs.Empty);
		}

		public GameState()
		{
			ReceivedSize = 0;
			receivedSize = 0;
			players = new List<PlayerState>();
			deck = UNOCard.Shuffle(UNOCard.GetDeck());
			currentCard = deck[0];
			deck = UNOCard.RemoveCard(deck, currentCard);
		}

		public void Initialize()
		{
			ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			ServerSocket.Bind(new IPEndPoint(GetIPAddress(), port));
			ServerSocket.Listen(Constants.MAX_PLAYERS);
			buffer = new byte[ServerSocket.ReceiveBufferSize];
			Debug.WriteLine("Server started at: " + GetIPAddress().ToString());
		}

		//Add player and set its properties
		public void AddPlayer(PlayerState playerState)
		{
			playerState.currentCard = currentCard;
			playerState.noOfCards = Constants.START_CARDS;
			UNOCard[] playerCards = new UNOCard[Constants.START_CARDS];

			for (int i = 0; i < Constants.START_CARDS; i++)
			{
				playerCards[i] = deck[0];
				deck = UNOCard.RemoveCard(deck, playerCards[i]);
			}

			playerState.playerCards = playerCards;
			players.Add(playerState);
			Debug.WriteLine("Player properly added!");
		}

		private PlayerState GetPlayerStateFromSocket(Socket clientSocket)
		{
			PlayerState player = null;
			foreach (PlayerState playerState in players)
			{
				if (playerState.playerSocket == clientSocket)
				{
					Debug.WriteLine("Player found!");
					player = playerState;
				}
			}
			return player;
		}

		public int[] GetOtherPlayerCards(PlayerState playerState)
		{
			List<int> playerCards = new List<int>();
			foreach (PlayerState player in players)
			{
				if (player == playerState)
					continue;
				playerCards.Add(player.playerCards.Length);
			}
			return playerCards.ToArray();
		}

		public string[] GetOtherPlayerNames(PlayerState playerState)
		{
			if (playerState == null)
			{
				Debug.WriteLine("Returning null");
				return null;
			}

			List<string> otherPlayerNames = new List<string>();
			Debug.WriteLine("players size: " + players.Count);
			foreach (PlayerState player in players)
			{
				otherPlayerNames.Add(player.playerName);
			}
			return otherPlayerNames.ToArray();
		}

		public UNOCard[] GetPlayerCards(PlayerState playerState)
		{
			if (playerState == null)
			{
				Debug.WriteLine("Returning null");
				return null;
			}

			UNOCard[] playerCards = null;
			foreach (PlayerState player in players)
			{
				if (player.playerSocket == playerState.playerSocket)
				{
					playerCards = player.playerCards;
				}
			}

			return playerCards;
		}

		private T[] SubArray<T>(T[] data, int index, int length)
		{
			T[] result = new T[length];
			Array.Copy(data, index, result, 0, length);
			return result;
		}

		public UNOCard[] GetCardsFromDeck(int noOfCards)
		{
			if (noOfCards < 1)
			{
				return null;
			}
			UNOCard[] cards = new UNOCard[noOfCards];
			for (int i = 0; i < noOfCards; i++)
			{
				cards[i] = deck[0];
				deck = UNOCard.RemoveCard(deck, cards[i]);
			}
			return cards;
		}

		public UNOCard getCurrentCard()
		{
			return currentCard;
		}

		public IPAddress GetIPAddress()
		{
			IPAddress ipAddress = IPAddress.Any;
			foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					ipAddress = ip;
					break;
				}
			}
			return ipAddress;
		}
	}
}
