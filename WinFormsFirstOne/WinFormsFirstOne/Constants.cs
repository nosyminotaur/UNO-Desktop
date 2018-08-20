using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsFirstOne
{
	class Constants
	{
		static public int TOTAL_CARDS = 144;
		static public int START_CARDS = 7;
		static public int MAX_PLAYERS = 4;
		public enum Numbers
		{
			none = -1,
			zero = 0,
			one = 1,
			two = 2,
			three = 3,
			four = 4,
			five = 5,
			six = 6,
			seven = 7,
			eight = 8,
			nine = 9
		}
		public enum Powers
		{
			none = -1,
			reverse = 0,
			skip = 1,
			plus_two = 2,
			wild = 3,
			plus_four = 4
		}
		public enum Colors
		{
			none = -1,
			red = 0,
			green = 1,
			blue = 2,
			yellow = 3,
		}
	}
}
