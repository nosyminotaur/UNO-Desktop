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
	enum Messages2
	{
		ClientReady,
		GetGameStatus,
		UIUpdated,
		ClientCurrentStatus
	}

	class GameState
	{
		public bool isOver;
		private List<PlayerState> players;
		private UNOCard[] deck;
		private UNOCard currentCard;

		public GameState(List<PlayerState> all_players)
		{
			players = all_players;
			isOver = false;
		}

		public void Initialize()
		{
			deck = UNOCard.Shuffle(UNOCard.GetDeck());
			currentCard = deck[0];
			int count = 1;
			foreach (PlayerState player in players)
			{
				player.currentCard = currentCard;
				player.playerCards = SubArray(deck, count, Constants.START_CARDS);
				player.noOfCards = Constants.START_CARDS;
				player.validCards = UNOCard.GetValidCards(currentCard, player.playerCards);
				count += Constants.START_CARDS;
			}
		}

		private void UpdateChance()
		{

		}

		private T[] SubArray<T>(T[] data, int index, int length)
		{
			T[] result = new T[length];
			Array.Copy(data, index, result, 0, length);
			return result;
		}

		public UNOCard GetCurrentCard()
		{
			return players[0].currentCard;
		}

		public int GetNoOfPlayers()
		{
			return players.Count;
		}

		public string[] GetPlayerNames()
		{
			string[] playerNames = new string[players.Count];
			int i = 0;
			foreach (PlayerState player in players)
			{
				playerNames[i++] = player.playerName;
			}
			return playerNames;
		}
	}

	public class Server2
	{
		private static int message_size;
		private static UNOCard[] deck;
		private static UNOCard currentCard;
		private static byte[] _buffer = new byte[4096];
		private static Socket _serverSocket;
		private static List<PlayerState> players;
		private static readonly int port = 8081;
		private static int chance = 0;

		public static bool StartServer(string username)
		{
			bool flag = true;
			if (Initialize(username))
			{
				_serverSocket.BeginAccept(_serverSocket.ReceiveBufferSize, new AsyncCallback(AcceptCallback), null);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		private static void AcceptCallback(IAsyncResult ar)
		{
			try
			{
				byte[] _buffer = new byte[_serverSocket.ReceiveBufferSize];
				Socket _clientSocket = _serverSocket.EndAccept(out _buffer, ar);
				string username = Encoding.ASCII.GetString(_buffer);
				Debug.WriteLine(username + " added!");
				players.Add(new PlayerState(_clientSocket, username));
				if (players.Count < Constants.MAX_PLAYERS)
				{
					_serverSocket.BeginAccept(_serverSocket.ReceiveBufferSize, new AsyncCallback(AcceptCallback), null);
				}
				else
				{
					Debug.WriteLine("Max players!");
					return;
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		public static void Receive()
		{
			try
			{
				_serverSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _serverSocket);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private static void ReceiveCallback(IAsyncResult ar)
		{
			try
			{
				Socket socket = (Socket)ar.AsyncState;
				int receivedSize = _serverSocket.EndReceive(ar);
				if (receivedSize == 0)
				{
					Receive();
				}

				if (receivedSize == 4)
				{
					//received data is int, indicating incoming message size
					string size_str = Encoding.ASCII.GetString(_buffer);
					message_size = Int32.Parse(size_str);
					_serverSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(DataReceiveCallback), socket);
				}

			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private static void DataReceiveCallback(IAsyncResult ar)
		{
			Socket clientSocket = (Socket)ar.AsyncState;
			try
			{
				int receivedSize = _serverSocket.EndReceive(ar);
				if (receivedSize != message_size)
				{
					_serverSocket.BeginReceive(_buffer, receivedSize, _buffer.Length - receivedSize, SocketFlags.None, new AsyncCallback(DataReceiveCallback), clientSocket);
				}
				string message_received = Encoding.ASCII.GetString(_buffer);
				CheckReply(clientSocket, message_received);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private static void CheckReply(Socket clientSocket, string message_received)
		{
			if (Enum.TryParse(message_received, out Messages2 message))
			{
				switch (message)
				{
					case Messages2.ClientReady:
						byte[] response = Encoding.ASCII.GetBytes("ServerReady");
						clientSocket.BeginSend(response, 0, response.Length, SocketFlags.None, new AsyncCallback(SendCallBack), clientSocket);
						break;
					case Messages2.GetGameStatus:
						SendGameStatus(clientSocket);
						break;
					default:
						break;
				}
			}
		}

		private static void SendGameStatus(Socket clientSocket)
		{
			//create a byte array of gamestate and send!
		}

		private static void SendCallBack(IAsyncResult ar)
		{
			Socket clientSocket = (Socket)ar.AsyncState;
			clientSocket.EndSend(ar);
		}

		private static bool Initialize(string username)
		{
			bool flag = true;
			try
			{
				_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				_serverSocket.Bind(new IPEndPoint(GetIPAddress(), port));
				players.Add(new PlayerState(_serverSocket, username, true, true));
				Debug.WriteLine("Count increased to: " + players.Count);
				_serverSocket.Listen(Constants.MAX_PLAYERS);
				Debug.WriteLine("Server started at: " + GetIPAddress().ToString());
			}
			catch (Exception e)
			{
				flag = false;
				Debug.WriteLine(e.ToString());
				throw;
			}
			return flag;
		}

		private static T[] SubArray<T>(T[] data, int index, int length)
		{
			T[] result = new T[length];
			Array.Copy(data, index, result, 0, length);
			return result;
		}

		private static IPAddress GetIPAddress()
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