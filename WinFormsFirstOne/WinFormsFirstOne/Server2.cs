using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WinFormsFirstOne
{
	class GameState2
	{
		public List<PlayerState> players;
		public UNOCard currentCard;
		public int chance = 0;
		private UNOCard[] deck;
		public Socket ServerSocket;

		public GameState()
		{
			players = new List<PlayerState>();
			deck = UNOCard.Shuffle(UNOCard.GetDeck());
			currentCard = deck[0];
			deck = UNOCard.RemoveCard(deck, currentCard);
		}

		public void Initialize()
		{
			Debug.WriteLine("Deck size: " + deck.Length);
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

		public void AddPlayer(PlayerState playerState)
		{
			playerState.currentCard = currentCard;
			playerState.noOfCards = Constants.START_CARDS;
			UNOCard[] playerCards = new UNOCard[Constants.START_CARDS];
			for (int i = 0; i< Constants.START_CARDS; i++)
			{
				UNOCard card = deck[0];
				playerCards[i] = card;
				deck = UNOCard.RemoveCard(deck, card);
			}
			playerState.playerCards = playerCards;
			players.Add(playerState);
			Debug.WriteLine("Player properly added!");
		}

		public UNOCard[] GetPlayerCards(PlayerState playerState)
		{
			if (playerState == null)
			{
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

		public UNOCard getCurrentCard()
		{
			return currentCard;
		}
	}

	//class GameState2
	//{
	//	public bool isOver;
	//	private List<PlayerState> players;
	//	private UNOCard[] deck;
	//	private UNOCard currentCard;

	//	public GameState2(List<PlayerState> all_players)
	//	{
	//		players = all_players;
	//		isOver = false;
	//	}

	//	public void Initialize()
	//	{
	//		deck = UNOCard.Shuffle(UNOCard.GetDeck());
	//		currentCard = deck[0];
	//		int count = 1;
	//		foreach (PlayerState player in players)
	//		{
	//			player.currentCard = currentCard;
	//			player.playerCards = SubArray(deck, count, Constants.START_CARDS);
	//			player.noOfCards = Constants.START_CARDS;
	//			player.validCards = UNOCard.GetValidCards(currentCard, player.playerCards);
	//			count += Constants.START_CARDS;
	//		}
	//	}

	//	private T[] SubArray<T>(T[] data, int index, int length)
	//	{
	//		T[] result = new T[length];
	//		Array.Copy(data, index, result, 0, length);
	//		return result;
	//	}

	//	public UNOCard GetCurrentCard()
	//	{
	//		return players[0].currentCard;
	//	}

	//	public int GetNoOfPlayers()
	//	{
	//		return players.Count;
	//	}

	//	public string[] GetPlayerNames()
	//	{
	//		string[] playerNames = new string[players.Count];
	//		int i = 0;
	//		foreach (PlayerState player in players)
	//		{
	//			playerNames[i++] = player.playerName;
	//		}
	//		return playerNames;
	//	}
	//}

	public class Server2
	{
		private GameState gameState;
		private  int message_size;
		private int receivedSize;
		private  byte[] _buffer = new byte[4096];
		private  readonly int port = 8081;

		public  bool StartServer(string username)
		{
			bool flag = true;
			if (Initialize(username))
			{
				gameState.ServerSocket.BeginAccept(_buffer.Length, new AsyncCallback(AcceptCallback), null);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		private  void AcceptCallback(IAsyncResult ar)
		{
			try
			{
				byte[] buffer = new byte[gameState.ServerSocket.ReceiveBufferSize];
				Socket _clientSocket = gameState.ServerSocket.EndAccept(out buffer, ar);
				string username = Encoding.ASCII.GetString(buffer);
				Debug.WriteLine(username + " added!");
				gameState.AddPlayer(new PlayerState(_clientSocket, username));
				_clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _clientSocket);
				
				if (gameState.players.Count < Constants.MAX_PLAYERS)
				{
					gameState.ServerSocket.BeginAccept(_buffer.Length, new AsyncCallback(AcceptCallback), null);
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

		private void ReceiveCallback(IAsyncResult ar)
		{
			try
			{
				Debug.WriteLine("Coming into ReceiveCallback");
				Socket _clientSocket = (Socket)ar.AsyncState;
				receivedSize =_clientSocket.EndReceive(ar);
				Debug.WriteLine("receivedSize is "+receivedSize);
				if (receivedSize == 0)
				{
					_clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _clientSocket);
				}

				if (receivedSize == 4)
				{
					//received data is int, indicating incoming message size
					message_size = BitConverter.ToInt32(_buffer, 0);
					Debug.WriteLine("Received size is "+message_size);
					Debug.WriteLine("Integer received, val: " +message_size);
					bool val = true;
					byte[] buffer = BitConverter.GetBytes(val);
					_clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack2), _clientSocket);
				}

				if (receivedSize == 15)
				{
					Debug.WriteLine("Inside if 15");
					_clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(TempReceiveCallback), _clientSocket);
				}

			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private void SendCallBack2(IAsyncResult ar)
		{
			Socket clientSocket = (Socket)ar.AsyncState;
			int sizeOfDataSent = clientSocket.EndSend(ar);
			Debug.WriteLine("Size of data sent: "+sizeOfDataSent);
			clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(TempReceiveCallback), clientSocket);
		}

		private void TempReceiveCallback(IAsyncResult ar)
		{
			Debug.WriteLine("Inside TempReceiveCallback");
			Socket clientSocket = (Socket)ar.AsyncState;
			clientSocket.EndReceive(ar);
			string message = Encoding.ASCII.GetString(_buffer, 0, message_size);
			Debug.WriteLine("Message Received: "+message);
			Debug.WriteLine("Message size: " + message.Length);
			CheckReply(clientSocket, message);
		}

		private  void DataReceiveCallback(IAsyncResult ar)
		{
			Socket clientSocket = (Socket)ar.AsyncState;
			try
			{
				int receivedSize = gameState.ServerSocket.EndReceive(ar);
				if (receivedSize != message_size)
				{
					Debug.WriteLine("Full message not received");
					clientSocket.BeginReceive(_buffer, receivedSize, _buffer.Length - receivedSize, SocketFlags.None, new AsyncCallback(DataReceiveCallback), clientSocket);
				}
				string message_received = Encoding.ASCII.GetString(_buffer);
				Debug.WriteLine(message_received);
				CheckReply(clientSocket, message_received);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private  void CheckReply(Socket clientSocket, string message_received)
		{
			Debug.WriteLine("Inside CheckReply");
			if (message_received == "ClientReady")
			{
				Debug.WriteLine("ClientReady received!");
			}
			if (Enum.TryParse(message_received, out Messages2 message))
			{
				switch (message)
				{
					case Messages2.ClientReady:
						SendCards(clientSocket);
						break;
				}
			}
		}

		private void SendCards(Socket clientSocket)
		{
			PlayerState player = null;
			foreach (PlayerState playerState in gameState.players)
			{
				if (playerState.playerSocket == clientSocket)
				{
					Debug.WriteLine("Player found!");
					player = playerState;
				}
			}

			UNOCard[] playerCards = gameState.GetPlayerCards(player);
			Debug.WriteLine("Color: "+playerCards[0].GetColor());
			byte[] data = ConvertListToByte(playerCards);
			Debug.WriteLine("Cards size: " + data.Length);
			Send(player, data);
		}

		private byte[] ConvertListToByte<T>(T[] array)
		{
			Debug.WriteLine("array length: " + array.Length);
			List<T> list = array.ToList<T>();
			string output = JsonConvert.SerializeObject(list, Formatting.Indented);
			Debug.WriteLine(output);
			return Encoding.ASCII.GetBytes(output);
		}

		private byte[] ConvertArrayToByte<T>(T[] array)
		{
			Debug.WriteLine("array length: " + array.Length);
			string output = JsonConvert.SerializeObject(array, Formatting.Indented);
			Debug.WriteLine(output);
			return Encoding.ASCII.GetBytes(output);
		}

		private byte[] GetByteArrayFromArray<T>(T[] array)
		{
			List<byte[]> data_partial = new List<byte[]>();
			int length = array.Length;
			int size = 0;
			for (int i = 0; i < length; i++)
			{
				data_partial[i] = GetByteFromObject(array[i]);
				size += data_partial[i].Length;
			}
			size += length * 4;
			byte[] data = new byte[size];
			int count = 0;
			for (int i = 0; i < length; i++)
			{
				int index_size = data_partial[i].Length;
				byte[] int_to_byte = BitConverter.GetBytes(index_size);
				Array.Copy(int_to_byte, 0, data, count, int_to_byte.Length);
				count += int_to_byte.Length;
				byte[] data_to_byte = GetByteFromObject(data_partial[i]);
				Array.Copy(data_to_byte, 0, data, count, data_to_byte.Length);
			}
			return data;
		}

		private byte[] GetByteFromObject(object obj)
		{
			var binFormatter = new BinaryFormatter();
			var mStream = new MemoryStream();
			binFormatter.Serialize(mStream, obj);

			//This gives you the byte array.
			return mStream.ToArray();
		}

		private object GetObjectFromByte(byte[] objectBytes)
		{
			var mStream = new MemoryStream();
			var binFormatter = new BinaryFormatter();

			// Where 'objectBytes' is your byte array.
			mStream.Write(objectBytes, 0, objectBytes.Length);
			mStream.Position = 0;

			return binFormatter.Deserialize(mStream);
		}

		public void Send(PlayerState player, byte[] data)
		{
			foreach (PlayerState playerName in gameState.players)
			{
				if (playerName.playerName == player.playerName)
				{
					Debug.WriteLine("Send: "+data.Length);
					player.playerSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), player.playerSocket);
				}
			}
		}

		private  void SendCallBack(IAsyncResult ar)
		{
			Socket clientSocket = (Socket)ar.AsyncState;
			Debug.WriteLine("Cards sent!");
			clientSocket.EndSend(ar);
			clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), clientSocket);
		}

		private  bool Initialize(string username)
		{
			bool flag = true;
			try
			{
				gameState = new GameState();
				gameState.Initialize();

				gameState.ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				gameState.ServerSocket.Bind(new IPEndPoint(GetIPAddress(), port));
				gameState.players.Add(new PlayerState(gameState.ServerSocket, username, true));
				Debug.WriteLine("Count increased to: " + gameState.players.Count);
				gameState.ServerSocket.Listen(Constants.MAX_PLAYERS);
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

		private  T[] SubArray<T>(T[] data, int index, int length)
		{
			T[] result = new T[length];
			Array.Copy(data, index, result, 0, length);
			return result;
		}

		public  IPAddress GetIPAddress()
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