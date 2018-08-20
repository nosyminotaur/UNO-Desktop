using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsFirstOne
{
	public abstract class Card
	{
		public int Number;
		public int Color;
		public int Power;
		public Card(int Number, int Color, int Power)
		{
			Debug.WriteLine("Card constructor called");
			this.Number = Number;
			this.Color = Color;
			this.Power = Power;
		}
	}
}
