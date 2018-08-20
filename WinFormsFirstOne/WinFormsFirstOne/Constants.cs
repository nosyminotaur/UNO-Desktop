using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsFirstOne
{
	class Constants
	{
		static public int TOTAL_CARDS = 136;
		static public int START_CARDS = 7;
		static public int MAX_PLAYERS = 4;
		public enum Numbers
		{
			None = -1,
			Zero = 0,
			One = 1,
			Two = 2,
			Three = 3,
			Four = 4,
			Five = 5,
			Six = 6,
			Seven = 7,
			Eight = 8,
			Nine = 9
		}
		public enum Powers
		{
			None = -1,
			Reverse = 0,
			Skip = 1,
			Plus_Two = 2,
			Plus_Four = 3
		}
		public enum Colors
		{
			None = -1,
			Red = 0,
			Green = 1,
			Blue = 2,
			Yellow = 3,
		}
	}
}
