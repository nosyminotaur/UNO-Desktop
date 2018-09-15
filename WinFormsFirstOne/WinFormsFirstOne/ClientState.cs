using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsFirstOne
{
	public class ClientState
	{
		public Socket clientSocket;
		public byte[] buffer;
		public string userName;
		//Still not sure whether to use list or array
		public List<string> otherPlayerNames;
		public UNOCard currentCard;
		public List<UNOCard> userCards;
		public List<int> otherPlayerCards;

		public ClientState(string username)
		{
			clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			userName = username;
			buffer = new byte[clientSocket.ReceiveBufferSize];
			otherPlayerNames = new List<string>();
			userCards = new List<UNOCard>();
			otherPlayerCards = new List<int>();
		}

		public void SetPlayerNames(List<string> playernames)
		{
			if (playernames == null)
			{
				return;
			}
			this.otherPlayerNames = playernames;
		}

		public void setCurrentCard(UNOCard currentcard)
		{
			if (currentcard == null)
			{
				return;
			}
			this.currentCard = currentcard;
		}

		public void setuserCards(List<UNOCard> usercards)
		{
			if (usercards == null)
			{
				return;
			}
			this.userCards = usercards;
		}

		public void setOtherPlayersCards(List<int> otherplayercards)
		{
			if (otherplayercards == null)
			{
				return;
			}
			this.otherPlayerCards = otherplayercards;
		}
	}
}
