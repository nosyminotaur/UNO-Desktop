using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsFirstOne
{
	public class UNOCard
	{
		protected int Number;
		protected int Color;
		protected int Power;
		UNOCard(int Number, int Color, int Power)
		{
			this.Number = Number;
			this.Color = Color;
			this.Power = Power;
		}
		
		public int GetNumber()
		{
			return Number;
		}

		public int GetColor()
		{
			return Color;
		}

		public int GetPower()
		{
			return Power;
		}

		private static UNOCard[] GetNumberCards()
		{
			UNOCard[] numberCards = new UNOCard[80];
			int index = 0;
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					for (int k = 0; k < 2; k++)
					{
						numberCards[index] = new UNOCard(i, j, -1);
						index += 1;
					}
				}
			}
			return numberCards;
		}

		private static UNOCard[] GetPowerCards()
		{
			UNOCard[] powerCards = new UNOCard[56];
			int count = 0;
			for (int i = 0; i < 3; i++) //Power
			{
				for (int j = 0; j < 4; j++) // Color
				{
					for (int k = 0; k < 4; k++) //No of cards
					{
						powerCards[count] = new UNOCard(-1, j, i);
						count++;
					}
				}
			}

			for (int i = 0; i < 8; i++)
			{
				powerCards[count] = new UNOCard(-1, 3, -1);
			}
			return powerCards;
		}

		public static UNOCard[] GetDeck()
		{
			UNOCard[] cards = new UNOCard[136];
			UNOCard[] numberCards = GetNumberCards();
			UNOCard[] powerCards = GetPowerCards();
			numberCards.CopyTo(cards, 0);
			numberCards.CopyTo(cards, numberCards.Length);
			return cards;
		}

		public static UNOCard[] Shuffle(UNOCard[] cards)
		{
			Random random = new Random();
			int length = cards.Length;
			int k;
			while (length > 1)
			{
				k = random.Next(length--);
				UNOCard temp = cards[length];
				cards[length] = cards[k];
				cards[k] = temp;
			}
			return cards;
		}

		public static UNOCard[] GetValidCards(UNOCard currentCard, UNOCard[] playerCards)
		{
			int currentCardColor = currentCard.GetColor();
			int currentCardNumber = currentCard.GetNumber();
			List<UNOCard> validCards = playerCards.ToList();
			foreach (UNOCard card in validCards)
			{
				int number = card.GetNumber();
				int power = card.GetPower();
				int color = card.GetColor();
				if (number != -1)
				{
					if (number != currentCardNumber || color != currentCardColor)
					{
						validCards.Remove(card);
					}
				}
				else
				{
					if (power != 3)
					{
						if (color != currentCardColor)
						{
							validCards.Remove(card);
						}
					}
				}
			}
			return validCards.ToArray();
		}

		public static UNOCard GetRandomCard(UNOCard[] cards)
		{
			Random random = new Random();
			return cards[random.Next(cards.Length)];
		}
	}
}
