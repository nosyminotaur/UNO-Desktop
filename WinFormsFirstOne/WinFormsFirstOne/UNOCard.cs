/*
10 * 4 number cards
2 * 4 * 3 power cards
4 plus four cards
4 wild cards

72*2 = 144

4 skip of each color
4 reverse of each color
4 plus two of each color
8 plus four
8 wild

naming convention ->

 blue_0_large
 plus_four_large
 wild_large
 green_plus_two_large
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace WinFormsFirstOne
{
	public class UNOCard
	{
		public static Random random;
		public int Number;
		public int Color;
		public int Power;
		public UNOCard()
		{
			//Empty default constructor, do not remove!
		}
		public UNOCard(int Number, int Color, int Power)
		{
			random = new Random();
			this.Number = Number;
			this.Color = Color;
			this.Power = Power;
		}

		public int GetNumber()
		{
			return this.Number;
		}

		public int GetColor()
		{
			return this.Color;
		}

		public int GetPower()
		{
			return this.Power;
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
			UNOCard[] powerCards = new UNOCard[64];
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
				powerCards[count++] = new UNOCard(-1, -1, 3);
				powerCards[count++] = new UNOCard(-1, -1, 4);
			}
			//Debug.WriteLine(count);
			return powerCards;
		}

		public static UNOCard[] GetDeck()
		{
			UNOCard[] cards = new UNOCard[Constants.TOTAL_CARDS];
			UNOCard[] numberCards = GetNumberCards();
			UNOCard[] powerCards = GetPowerCards();
			numberCards.CopyTo(cards, 0);
			powerCards.CopyTo(cards, numberCards.Length);
			return cards;
		}

		public static UNOCard[] RemoveCard(UNOCard[] cards, UNOCard removalCard)
		{
			int index = Array.IndexOf(cards, removalCard);
			if (index == -1)
			{
				return cards;
			}
			UNOCard[] newCards = new UNOCard[cards.Length - 1];
			int count = 0;
			for (int i = 0; i < cards.Length; i++)
			{
				if (i == index)
					continue;
				newCards[count++] = cards[i];
			}
			return newCards;
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
					if (power != 3 || power != 4) //Plus 4 or wild
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

		public static UNOCard GetRandomCard()
		{
			UNOCard[] deck = new UNOCard[144];
			deck = UNOCard.GetDeck();
			return GetRandomCard(deck);
		}

		public static UNOCard GetRandomCard(UNOCard[] cards)
		{
			//Debug.WriteLine(cards.Length);
			int rand_val_1 = random.Next(cards.Length);
			int rand_val_2 = random.Next(cards.Length);
			int rand_val_3 = random.Next(cards.Length);
			int rand = Math.Max(rand_val_1, Math.Max(rand_val_2, rand_val_3));
			Debug.WriteLine("Random integer: "+rand);
			return cards[rand];
		}
	}
}
