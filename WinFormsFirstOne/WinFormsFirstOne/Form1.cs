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
		public Form1()
		{
			InitializeComponent();
			pictureBox1.Load("./images/blue_1_large.png");
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
			UNOCard[] deck = UNOCard.GetDeck();
			UNOCard randomCard = UNOCard.GetRandomCard(deck);
			int color = randomCard.GetColor();
			int power = randomCard.GetPower();
			int number = randomCard.GetNumber();
			if (power != -1)
			{
				String resource = "./images/";
				resource += Constants.Colors.
				Debug.WriteLine()
			}
		}
	}
}
