using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsOne
{
    class ServerNew
    {
        private byte[] _buffer = new byte[4096];
        private Socket _serverSocket;
        private readonly int port = 8081;
        public string username;
        private List<Player> players;

        public ServerNew(string username)
        {
            this.username = username;
        }

        public bool StartServer()
        {
            bool flag = true;
            try
            {
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				_serverSocket.Bind(new IPEndPoint(GetIPAddress(), port));
                players.Add(new PlayerState(_serverSocket, username, true));
                Debug.WriteLine("Server player added");
                _serverSocket.Listen(Constants.MAX_PLAYERS);
				Debug.WriteLine("Server started at: " + GetIPAddress().ToString());
				_serverSocket.BeginAccept(_serverSocket.ReceiveBufferSize, new AsyncCallback(AcceptCallback), null);
            }
            catch (System.Exception)
            {
                flag = false;
            }
            return flag;
        }

        private void AcceptCallback(IAsyncResult ar)
		{
			try
			{
				byte[] _buffer = new byte[_serverSocket.ReceiveBufferSize];
				Socket _clientSocket = _serverSocket.EndAccept(out _buffer, ar);
				
                string username = Encoding.ASCII.GetString(_buffer);
				Debug.WriteLine(username + " added!");
				players.Add(new PlayerState(_clientSocket, username));

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

        public bool Receive()
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

        private void ReceiveCallback(IAsyncResult ar)
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
    }
}