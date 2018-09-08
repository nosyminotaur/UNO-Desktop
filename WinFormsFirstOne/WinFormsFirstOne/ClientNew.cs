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
	public class ClientStateEventArgs : EventArgs
	{
		public ClientState clientState { get; set; }
	}

	public class ClientNew
	{
		private IPAddress ipAddress;
		private readonly int port;
        private ClientState clientState;

		public ClientNew(IPAddress IPAddress, int port, string username, ClientState client)
		{
			this.ipAddress = IPAddress;
			this.port = port;
            this.clientState = client;
		}

		public event EventHandler<ClientStateEventArgs> PlayerCardsReceived;

		protected virtual void OnPlayerCardsReceived()
		{

			PlayerCardsReceived?.Invoke(this, new ClientStateEventArgs() { clientState = this.clientState});
		}

		public void Start()
		{
			try
			{
				clientState.clientSocket.BeginConnect(new IPEndPoint(ipAddress, port), new AsyncCallback(ConnectCallback), null);
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
				byte[] data = Encoding.ASCII.GetBytes(clientState.userName);
				clientState.clientSocket.Send(data);
				clientState.clientSocket.EndConnect(ar);
				int message = (int)Messages2.GetPlayerCards;
				Debug.WriteLine("Sending: " + message);
				data = BitConverter.GetBytes(message);
				clientState.clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
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
				clientState.clientSocket.EndSend(ar);
				Debug.WriteLine("GetPlayerCards sent!");
				clientState.clientSocket.BeginReceive(clientState.buffer, 0, clientState.buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback3), null);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private void ReceiveCallback3(IAsyncResult ar)
		{
			Debug.WriteLine("Some data arrived!");
			byte[] data = clientState.buffer;
			string cards = Encoding.ASCII.GetString(data);
			ConvertAndDisplay2(cards);
		}

		private void ReceiveCallback2(IAsyncResult ar)
		{
			Debug.WriteLine("Bool value received, sending ClientReady");
			clientState.clientSocket.EndReceive(ar);
			bool val = BitConverter.ToBoolean(clientState.buffer, 0);
			if (val)
			{
				Debug.WriteLine("Buffer length is 1");
				string sendData = "ClientReady";
				byte[] data = Encoding.ASCII.GetBytes(sendData);
				clientState.clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback2), null);
			}
		}

		private void SendCallback2(IAsyncResult ar)
		{
			try
			{
				Debug.WriteLine("Data sent");
				clientState.clientSocket.BeginReceive(clientState.buffer, 0, clientState.buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
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
				int receivedSize = clientState.clientSocket.EndReceive(ar);
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
			string data = Encoding.ASCII.GetString(clientState.buffer, 0, receivedSize);
			Debug.WriteLine(data.Length);
			List<UNOCard> deserializedProduct = JsonConvert.DeserializeObject<List<UNOCard>>(data);
			Debug.WriteLine(deserializedProduct[0].GetColor());
			Constants.Colors colors = (Constants.Colors)deserializedProduct[0].GetColor();
			Debug.WriteLine(colors.ToString());
		}

		private void ConvertAndDisplay2(string cardString)
		{
			List<UNOCard> cards = JsonConvert.DeserializeObject<List<UNOCard>>(cardString);
			Debug.WriteLine(cards[0].GetColor());
			clientState.setuserCards(cards);
			OnPlayerCardsReceived();
		}
	}
}
