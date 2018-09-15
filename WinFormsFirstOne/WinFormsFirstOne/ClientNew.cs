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
		public List<string> otherPlayerNames;
		public UNOCard currentCard;
		public List<UNOCard> userCards;
		public List<int> otherPlayerCards;
	}

	public class ClientNew
	{
		ClientStateEventArgs clientArgs;
		private IPAddress ipAddress;
		private readonly int port;
        private ClientState clientState;

		public ClientNew(IPAddress IPAddress, int _port, string username, ClientState client)
		{
			ipAddress = IPAddress;
			port = _port;
            clientState = client;
			clientArgs = new ClientStateEventArgs();
		}

		//Event handlers and their methods

		public event EventHandler<ClientStateEventArgs> UserCardsReceived;
		public event EventHandler<ClientStateEventArgs> OtherPlayerNamesReceived;
		public event EventHandler<ClientStateEventArgs> OtherPlayerCardsReceived;
		public event EventHandler<ClientStateEventArgs> CurrentCardReceived;

		protected virtual void OnUserCardsReceived()
		{
			UserCardsReceived?.Invoke(this, new ClientStateEventArgs() {
				userCards = clientState.userCards
			});
		}

		protected virtual void OnOtherPlayerNamesReceived()
		{
			OtherPlayerNamesReceived?.Invoke(this, new ClientStateEventArgs()
			{
				otherPlayerNames = clientState.otherPlayerNames
			});
		}

		protected virtual void OnOtherPlayerCardsReceived()
		{
			OtherPlayerCardsReceived?.Invoke(this, new ClientStateEventArgs()
			{
				otherPlayerCards = clientState.otherPlayerCards
			});
		}

		protected virtual void OnCurrentCardReceived()
		{
			CurrentCardReceived?.Invoke(this, new ClientStateEventArgs()
			{
				currentCard = clientState.currentCard
			});
		}

		public void Start()
		{
			clientState.clientSocket.BeginConnect(new IPEndPoint(ipAddress, port), new AsyncCallback(ConnectCallback), null);
		}

		public void CheckConnection()
		{
			byte[] check = BitConverter.GetBytes(true);
			clientState.clientSocket.Send(check);
		}

		private void ConnectCallback(IAsyncResult ar)
		{
			try
			{
				byte[] data = Encoding.ASCII.GetBytes(clientState.userName);
				clientState.clientSocket.Send(data);
				clientState.clientSocket.EndConnect(ar);
				List<int> message = new List<int>();
				SendMessage(message);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private void SendMessage(List<int> message)
		{
			if (message.Count < (int)Messages2.Null)
			{
				if (!message.Any())
				{
					message.Add(0);
					byte[] data = BitConverter.GetBytes(0);
					clientState.clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), message);
				}
				else
				{
					message.Add(message.Last() + 1);
					byte[] data = BitConverter.GetBytes(message.Last());
					clientState.clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), message);
				}
			}
		}

		private void SendCallback(IAsyncResult ar)
		{
			List<int> message = (List<int>)ar.AsyncState;
			try
			{
				clientState.clientSocket.EndSend(ar);
				Debug.WriteLine("Message sent!");
				clientState.clientSocket.BeginReceive(clientState.buffer, 0, clientState.buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), message);
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
			List<int> message_list = (List<int>)ar.AsyncState;
			int message_int = message_list.Last();
			int receivedSize = clientState.clientSocket.EndReceive(ar);
			try
			{
				Debug.WriteLine("Client received a message, size: " + receivedSize);
				Messages2 message = (Messages2)message_int;
				switch (message)
				{
					case (Messages2.GetCurrentCard):
						UpdateCurrentCard(receivedSize);
						OnCurrentCardReceived();
						SendMessage(message_list);
						break;
					case (Messages2.GetOtherPlayerCards):
						UpdateOtherPlayerCards(receivedSize);
						OnOtherPlayerCardsReceived();
						SendMessage(message_list);
						break;
					case (Messages2.GetOtherPlayerNames):
						UpdateOtherPlayerNames(receivedSize);
						OnOtherPlayerNamesReceived();
						SendMessage(message_list);
						break;
					case (Messages2.GetUserCards):
						UpdateUserCards(receivedSize);
						OnUserCardsReceived();
						SendMessage(message_list);
						break;
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
		}

		private void UpdateUserCards(int receivedSize)
		{
			Debug.WriteLine("Updating UserCards");
			string data = Encoding.ASCII.GetString(clientState.buffer, 0, receivedSize);
			List<UNOCard> cards = JsonConvert.DeserializeObject<List<UNOCard>>(data);
			clientState.userCards = cards;
		}

		private void UpdateOtherPlayerNames(int receivedSize)
		{
			Debug.WriteLine("Updating OtherPlayerNames");
			string data = Encoding.ASCII.GetString(clientState.buffer, 0, receivedSize);
			List<string> playerNames = JsonConvert.DeserializeObject<List<string>>(data);
			clientState.otherPlayerNames = playerNames;
		}

		private void UpdateOtherPlayerCards(int receivedSize)
		{
			Debug.WriteLine("Updating UserCards");
			string data = Encoding.ASCII.GetString(clientState.buffer, 0, receivedSize);
			List<int> sizes = JsonConvert.DeserializeObject<List<int>>(data);
			clientState.otherPlayerCards = sizes;
		}

		private void UpdateCurrentCard(int receivedSize)
		{
			Debug.WriteLine("Updating CurrentCard");
			string data = Encoding.ASCII.GetString(clientState.buffer, 0, receivedSize);
			UNOCard card = JsonConvert.DeserializeObject<UNOCard>(data);
			clientState.currentCard = card;
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
			OnUserCardsReceived();
		}
	}
}
