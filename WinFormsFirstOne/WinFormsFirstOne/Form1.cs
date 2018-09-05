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
		const int port = 8081;
		Server2 server;
		Client2 client;
		public Form1()
		{
			InitializeComponent();
			server = new Server2();
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

		private void StartServerButton_Click(object sender, EventArgs e)
		{
			if (UserNameTextBox.Text != "")
			{
				server.StartServer(UserNameTextBox.Text);
				IPAddressOutputTextBox.Text = server.GetIPAddress().ToString() + ": " + port;
				Debug.WriteLine(IPAddressOutputTextBox.Text);
			}
			else
			{
				MessageBox.Show("Enter a username", "Error", MessageBoxButtons.OK);
			}
		}

		private void JoinGameButton_Click(object sender, EventArgs e)
		{
			string ip = IPAddressInputTextBox.Text;
			IPAddress ipAddress;
			IPAddress.TryParse(ip, out ipAddress);
			string username = UserNameTextBox2.Text;
			Debug.WriteLine(username);
			client = new Client2(ipAddress, port, username);
			client.Start();
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
	}
}
