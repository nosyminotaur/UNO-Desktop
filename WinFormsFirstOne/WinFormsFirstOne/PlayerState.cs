using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsFirstOne
{
	public class PlayerState
	{
		public Socket playerSocket;
		public string playerName;
		public bool isChance;
		public UNOCard currentCard;
		public UNOCard[] playerCards;
		public int noOfCards;
		public UNOCard[] validCards;
		public PlayerState(Socket socket, string playerName, bool isChance = false)
		{
			this.playerSocket = socket;
			this.playerName = playerName;
			this.isChance = isChance;
		}
	}
}
