using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WinFormsFirstOne
{
	class Client2
	{
		private IPAddress ipAddress;
		private int port;
		private Socket _clientSocket;
		private string userName;
		private byte[] _buffer;

		public Client2(IPAddress IPAddress, int port, string username)
		{
			_buffer = new byte[4096];
			this.ipAddress = IPAddress;
			this.port = port;
			this.userName = username;
		}

		public void Start()
		{
			try
			{
				_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				_clientSocket.BeginConnect(new IPEndPoint(ipAddress, port), new AsyncCallback(ConnectCallback), null);
			}
			catch (Exception ef)
			{
				Debug.WriteLine(ef.ToString());
			}
		}

		private void ConnectCallback(IAsyncResult ar)
		{
			try
			{
				byte[] data = Encoding.ASCII.GetBytes(userName);
				_clientSocket.Send(data);
				_clientSocket.EndConnect(ar);
				string sendData = "ClientReady";
				int output = sendData.Length;
				data = BitConverter.GetBytes(output);
				Debug.WriteLine("Sending size of ClientReady");
				Debug.WriteLine("Size: "+output);
				_clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private void SendCallback(IAsyncResult ar)
		{
			try
			{
				string sendData = "ClientReady";
				_clientSocket.EndSend(ar);
				Debug.WriteLine("Size of ClientReady sent!");
				byte[] data = Encoding.ASCII.GetBytes(sendData);
				_clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback2), null);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private void ReceiveCallback2(IAsyncResult ar)
		{
			Debug.WriteLine("Bool value received, sending ClientReady");
			_clientSocket.EndReceive(ar);
			bool val = BitConverter.ToBoolean(_buffer, 0);
			if (val)
			{
				Debug.WriteLine("Buffer length is 1");
				string sendData = "ClientReady";
				byte[] data = Encoding.ASCII.GetBytes(sendData);
				_clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback2), null);
			}
		}

		private void SendCallback2(IAsyncResult ar)
		{
			try
			{
				Debug.WriteLine("Data sent");
				_clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
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
				int receivedSize = _clientSocket.EndReceive(ar);
				Debug.WriteLine("Client: "+receivedSize);
				ConvertAndDisplay(receivedSize);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private void ConvertAndDisplay(int receivedSize)
		{
			string data = Encoding.ASCII.GetString(_buffer, 0, receivedSize);
			Debug.WriteLine(data.Length);
			List<UNOCard> deserializedProduct = JsonConvert.DeserializeObject<List<UNOCard>>(data);
			Debug.WriteLine(deserializedProduct[0].GetColor());
			Constants.Colors colors = (Constants.Colors)deserializedProduct[0].GetColor();
			Debug.WriteLine(colors.ToString());
		}
	}
}
