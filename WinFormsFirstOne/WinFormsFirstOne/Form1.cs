using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
		const int port = 8081;
		Server server;
		public Form1()
		{
			InitializeComponent();
			pictureBox1.Load("./images/card_back_alt_large.png");
			server = new Server();
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
			UNOCard[] deck = new UNOCard[144];
			deck = UNOCard.GetDeck();
			UNOCard randomCard = UNOCard.GetRandomCard(deck);
			int color = randomCard.GetColor();
			int power = randomCard.GetPower();
			int number = randomCard.GetNumber();
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
			Debug.WriteLine(resource);
			pictureBox1.Load(resource);
		}

		private void startServerButton_Click(object sender, EventArgs e)
		{
			server.StartServer(userNameTextBox.Text);
		}
	}
}
