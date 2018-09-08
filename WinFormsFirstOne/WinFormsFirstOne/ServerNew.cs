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
	enum Messages2
	{
		GetPlayerCards,
        GetOtherPlayerNames,
        Null
	}

	public class GameState
	{
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
        public  byte[] buffer;
		private readonly int port = 8081;

		public GameState()
		{
			players = new List<PlayerState>();
			deck = UNOCard.Shuffle(UNOCard.GetDeck());
			currentCard = deck[0];
			deck = UNOCard.RemoveCard(deck, currentCard);
		}

		public void Initialize(string username)
		{
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ServerSocket.Bind(new IPEndPoint(GetIPAddress(), port));
            players.Add(new PlayerState(ServerSocket, username, true));
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
			for (int i = 0; i< Constants.START_CARDS; i++)
			{
				playerCards[i] = deck[0];
				deck = UNOCard.RemoveCard(deck, playerCards[i]);
			}
			playerState.playerCards = playerCards;
			players.Add(playerState);
			Debug.WriteLine("Player properly added!");
		}

        public string[] GetOtherPlayerNames(PlayerState playerState)
        {
            if (playerState == null)
			{
				return null;
			}
            string[] otherPlayerNames = new string[players.Capacity - 1];
            int count = 0;
            foreach (PlayerState player in players)
            {
                if (player.playerSocket == playerState.playerSocket)
                {
                    continue;
                }
                otherPlayerNames[count++] = player.playerName;
            }
            return otherPlayerNames;
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

        public UNOCard[] GetCardsFromDeck(int noOfCards)
        {
            if (noOfCards < 1)
            {
                return null;
            }
            UNOCard[] cards = new UNOCard[noOfCards];
            for (int i = 0; i< noOfCards; i++)
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

	public class ServerNew
	{
		private GameState gameState;
		private int message_size;
		private int receivedSize;
		private readonly int port = 8081;

		public bool StartServer(string username)
		{
			bool flag = true;
			if (Initialize(username))
			{
                Debug.WriteLine("Initialization successful, listening on :" + GetIPAddress().ToString());
				gameState.ServerSocket.BeginAccept(gameState.buffer.Length, new AsyncCallback(AcceptCallback), null);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		private void AcceptCallback(IAsyncResult ar)
		{
			try
			{
				byte[] buffer = new byte[gameState.ServerSocket.ReceiveBufferSize];
				Socket clientSocket = gameState.ServerSocket.EndAccept(out buffer, ar);
				string username = Encoding.ASCII.GetString(buffer);
				Debug.WriteLine(username + " added!");
				gameState.AddPlayer(new PlayerState(clientSocket, username));
				clientSocket.BeginReceive(gameState.buffer, 0, gameState.buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), clientSocket);
				
				if (gameState.players.Count < Constants.MAX_PLAYERS)
				{
                    Debug.WriteLine("Still listening!");
					gameState.ServerSocket.BeginAccept(gameState.buffer.Length, new AsyncCallback(AcceptCallback), null);
				}
				else
				{
					Debug.WriteLine("Max players! Now preventing further players from joining");
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
                Debug.WriteLine("Message received from client!");
				Socket clientSocket = (Socket)ar.AsyncState;
				receivedSize =clientSocket.EndReceive(ar);
				Debug.WriteLine("receivedSize is "+receivedSize);
				if (receivedSize == 0)
				{
                    Debug.WriteLine("Received nothing, waiting for a message from client again!");
					clientSocket.BeginReceive(gameState.buffer, 0, gameState.buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), clientSocket);
				}

                if (receivedSize == 4)
                {
                    Debug.WriteLine("Integer received, checking on the type of message");
                    int message = BitConverter.ToInt32(gameState.buffer, 0);
                    CheckMessage(message, clientSocket);
                }

			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

        private void CheckMessage(int message, Socket clientSocket)
        {
            Messages2 mess = Messages2.Null;
            mess = (Messages2)message;
            switch (mess)
            {
                case Messages2.GetPlayerCards:
                    Debug.WriteLine("GetPlayerCards message received!");
                    SendCards(clientSocket);
					break;
                case Messages2.GetOtherPlayerNames:
                    Debug.WriteLine("GetOtherPlayerNames message received!");
                    SendOtherPlayerNames(clientSocket);
					break;
            }
        }

        private PlayerState GetPlayerStateFromSocket(Socket clientSocket)
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
            return player;
        }

        private void SendOtherPlayerNames(Socket clientSocket)
        {
            PlayerState player = GetPlayerStateFromSocket(clientSocket);
            if (player != null)
            {
                string[] otherPlayerNames = gameState.GetOtherPlayerNames(player);
                Debug.WriteLine("Sending otherPlayerNames!");
                byte[] data = ConvertListToByte(otherPlayerNames);
                Send(player, data);
            }
            else
            {
                Debug.WriteLine("Corresponding Player for socket not found!");
            }
        }

        private void SendCards(Socket clientSocket)
		{
			PlayerState player = GetPlayerStateFromSocket(clientSocket);
            if (player != null)
            {
                UNOCard[] playerCards = gameState.GetPlayerCards(player);
                byte[] data = ConvertListToByte(playerCards);
                Debug.WriteLine("Sending playerCards, type List<UNOCard>");
                Send(player, data);
            }
            else
            {
                Debug.WriteLine("Corresponding Player for socket not found!");
            }
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
					Debug.WriteLine("Size of data sent: "+data.Length);
					player.playerSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallBack), player.playerSocket);
				}
			}
		}

		//private void SendCallBack2(IAsyncResult ar)
		//{
		//	Socket clientSocket = (Socket)ar.AsyncState;
		//	int sizeOfDataSent = clientSocket.EndSend(ar);
		//	Debug.WriteLine("Size of data sent: "+sizeOfDataSent);
		//	clientSocket.BeginReceive(gameState.buffer, 0, gameState.buffer.Length, SocketFlags.None, new AsyncCallback(TempReceiveCallback), clientSocket);
		//}

		//private void TempReceiveCallback(IAsyncResult ar)
		//{
		//	Debug.WriteLine("Inside TempReceiveCallback");
		//	Socket clientSocket = (Socket)ar.AsyncState;
		//	clientSocket.EndReceive(ar);
		//	string message = Encoding.ASCII.GetString(gameState.buffer, 0, message_size);
		//	Debug.WriteLine("Message Received: "+message);
		//	Debug.WriteLine("Message size: " + message.Length);
		//	CheckReply(clientSocket, message);
		//}

		//private  void DataReceiveCallback(IAsyncResult ar)
		//{
		//	Socket clientSocket = (Socket)ar.AsyncState;
		//	try
		//	{
		//		int receivedSize = gameState.ServerSocket.EndReceive(ar);
		//		if (receivedSize != message_size)
		//		{
		//			Debug.WriteLine("Full message not received");
		//			clientSocket.BeginReceive(gameState.buffer, receivedSize, gameState.buffer.Length - receivedSize, SocketFlags.None, new AsyncCallback(DataReceiveCallback), clientSocket);
		//		}
		//		string message_received = Encoding.ASCII.GetString(gameState.buffer);
		//		Debug.WriteLine(message_received);
		//		CheckReply(clientSocket, message_received);
		//	}
		//	catch (Exception e)
		//	{
		//		Debug.WriteLine(e.ToString());
		//	}
		//}

		//private  void CheckReply(Socket clientSocket, string message_received)
		//{
		//	Debug.WriteLine("Inside CheckReply");
		//	if (message_received == "ClientReady")
		//	{
		//		Debug.WriteLine("ClientReady received!");
		//	}
		//	if (Enum.TryParse(message_received, out Messages2 message))
		//	{
		//		switch (message)
		//		{
		//			case Messages2.ClientReady:
		//				SendCards(clientSocket);
		//				break;
		//		}
		//	}
		//}

		private  void SendCallBack(IAsyncResult ar)
		{
			Socket clientSocket = (Socket)ar.AsyncState;
			Debug.WriteLine("Cards sent!");
			clientSocket.EndSend(ar);
			clientSocket.BeginReceive(gameState.buffer, 0, gameState.buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), clientSocket);
		}

		private bool Initialize(string username)
		{
			bool flag = true;
			try
			{
				gameState = new GameState();
				gameState.Initialize(username);
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