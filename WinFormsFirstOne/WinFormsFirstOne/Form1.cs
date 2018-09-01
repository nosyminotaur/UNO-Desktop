using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsFirstOne
{
	public partial class Form1 : Form
	{
		UNOCard[] RandomCards;
		int count = 0;
		int RandomCardsLength = 10;
		const int port = 8081;
		Server server;
		public Form1()
		{
			InitializeComponent();
			LoadRandomCards();
			server = new Server();
		}

		public void LoadRandomCards()
		{
			pictureBox1.Load("./images/card_back_alt_large.png");
			RandomCards = new UNOCard[RandomCardsLength];
			UNOCard[] deck = UNOCard.GetDeck(); 
			for (int i = 0; i < RandomCardsLength; i++)
			{
				UNOCard card = UNOCard.GetRandomCard(deck);
				deck = UNOCard.RemoveCard(deck, card);
				RandomCards[i] = card;
				//Debug.WriteLine(deck.Length);
				Debug.WriteLine(GetImageName(card));
			}
			pictureBox2.Load(GetImageName(RandomCards[count]));
		}

		private IPAddress GetIPAddress()
		{
			IPAddress ipAddress = IPAddress.Any;
			foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					ipAddress = ip;
					break;
				}
			}
			return ipAddress;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			UNOCard randomCard = UNOCard.GetRandomCard();
			string resource = GetImageName(randomCard);
			Debug.WriteLine(resource);
			pictureBox1.Load(resource);
		}

		private string GetImageName(UNOCard card)
		{
			int color = card.GetColor();
			int power = card.GetPower();
			int number = card.GetNumber();
			string resource = "./images/";
			if (color != -1)
			{
				resource += Enum.GetName(typeof(Constants.Colors), color);
				resource += "_";
				if (power != -1)
				{
					resource += Enum.GetName(typeof(Constants.Powers), power);
				}
				else
				{
					resource += number;
				}
			}
			else
			{
				resource += Enum.GetName(typeof(Constants.Powers), power);
			}
			resource += "_large.png";
			return resource;
		}

		private void startServerButton_Click(object sender, EventArgs e)
		{
			server.StartServer(userNameTextBox.Text);
			textBox1.Text = GetIPAddress().ToString() + ": " + port;
			Debug.WriteLine(textBox1.Text);
		}

		private void LeftButton_Click(object sender, EventArgs e)
		{
			count--;
			if (count < 0)
				count += RandomCardsLength;
			UpdateImage();
		}

		private void UpdateImage()
		{
			Debug.WriteLine("UpdateImage called");
			UNOCard card = RandomCards[count];
			string resource = GetImageName(card);
			Debug.WriteLine(resource);
			pictureBox2.Load(resource);
		}

		private void RightButton_Click(object sender, EventArgs e)
		{
			count++;
			if (count > RandomCardsLength -1)
				count -= RandomCardsLength;
			UpdateImage();
		}

		private void pictureBox2_Click(object sender, EventArgs e)
		{
			Debug.WriteLine(pictureBox2.ImageLocation);
		}
	}
}
