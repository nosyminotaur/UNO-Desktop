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
	enum Messages
	{
		ClientReady = 0,
		IsChance = 1,

	}
	class Server
	{
		private static byte[] _buffer = new byte[4096];
		private static Socket _serverSocket;
		private int chance;
		private PlayerState[] players;
		private int count;
		private readonly int port = 8081;
		public Server()
		{
			this.players = new PlayerState[Constants.MAX_PLAYERS];
			this.count = 0;
			this.chance = 0;
		}
		private void AddPlayer(PlayerState player)
		{
			players[count++] = player;
		}

		public void StartServer(string username)
		{
			try
			{
				_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				_serverSocket.Bind(new IPEndPoint(GetIPAddress(), port));
				players[count++] = new PlayerState(_serverSocket, username, true, true);
				Debug.WriteLine("Count incresed to: " + count);
				_serverSocket.Listen(Constants.MAX_PLAYERS);
				Debug.WriteLine("Server started at: " + GetIPAddress().ToString());
				_serverSocket.BeginAccept(_serverSocket.ReceiveBufferSize, new AsyncCallback(AcceptCallback), null);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private void AcceptCallback(IAsyncResult ar)
		{
			try
			{
				byte[] _buffer = new byte[_serverSocket.ReceiveBufferSize];
				Socket _clientSocket = _serverSocket.EndAccept(out _buffer, ar);
				string username = Encoding.ASCII.GetString(_buffer);
				Debug.WriteLine(username + " added!");
				players[count++] = new PlayerState(_clientSocket, username);
				Initialize();
				if (count < Constants.MAX_PLAYERS)
				{
					_serverSocket.BeginAccept(_serverSocket.ReceiveBufferSize, new AsyncCallback(AcceptCallback), null);
				}
				else
				{
					Debug.WriteLine("Max players!");
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		public static bool Receive()
		{
			bool flag = true;
			try
			{
				_serverSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _serverSocket);
			}
			catch (Exception e)
			{
				flag = false;
				Debug.WriteLine(e.ToString());
			}
			return flag;
		}

		private void Initialize()
		{
			
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
				string message = Encoding.ASCII.GetString(_buffer);
				CheckMessage(socket, message);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private static void CheckMessage(Socket socket, string message)
		{
			Messages messages;
			if (Enum.TryParse(message, out messages))
			{
				switch (messages)
				{
					case Messages.ClientReady:
						UpdateClient(socket);
						break;
					case Messages.IsChance:
						CheckChance(socket);
						break;
					default:
						SendDefaultReply(socket);
						break;
				}
			}
		}

		private static void SendDefaultReply(Socket socket)
		{
			throw new NotImplementedException();
		}

		private static void CheckChance(Socket socket)
		{
			throw new NotImplementedException();
		}

		private static void UpdateClient(Socket socket)
		{

		}

		private void SendMessage()
		{

		}

		private IPAddress GetIPAddress()
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
