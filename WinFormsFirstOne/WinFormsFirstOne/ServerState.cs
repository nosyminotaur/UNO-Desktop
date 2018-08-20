using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsFirstOne
{
	class ServerState
	{
		private PlayerState[] players;
		private int count;
		ServerState()
		{
			this.players = new PlayerState[Constants.MAX_PLAYERS];
			this.count = 0;
		}
		private void AddPlayer(PlayerState player)
		{
			players[count++] = player;
		}
	}
}
