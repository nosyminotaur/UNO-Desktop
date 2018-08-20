using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsFirstOne
{
	class PlayerState
	{
		PlayerState(string playerName, bool isReady = false, bool isChance = false)
		{
			this.playerName = playerName;
			this.isReady = isReady;
			this.isChance = isChance;
		}
		public string playerName;
		public bool isReady;
		public bool isChance;
		public UNOCard currentCard;
		public UNOCard[] playerCardDeck;
		public int noOfCards;
		public UNOCard[] validCards;
	}
}
