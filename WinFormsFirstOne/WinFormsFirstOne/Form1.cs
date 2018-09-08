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
		ServerNew server;
		ClientNew client;
		ClientState clientState;
		public Form1()
		{
			InitializeComponent();
			server = new ServerNew();
		}

		delegate void SetTextCallback(string text);

		private void SetText(string text)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (textBox1.InvokeRequired)
			{
				SetTextCallback d = new SetTextCallback(SetText);
				this.Invoke(d, new object[] { text });
			}
			else
			{
				this.textBox1.Text = text;
			}
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
			if (IPAddress.TryParse(ip, out ipAddress))
			{
				string username = UserNameTextBox2.Text;
				clientState = new ClientState(username);
				client = new ClientNew(ipAddress, port, username, clientState);
				client.PlayerCardsReceived += Client_PlayerCardsReceived;
				client.Start();
			}
			else
			{
				MessageBox.Show("IP incorrectly entered, try again!");
			}
		}

		private void Client_PlayerCardsReceived(object source, ClientStateEventArgs args)
		{
			Debug.WriteLine("Inside Delegate method");
			int color = args.clientState.userCards[0].GetColor();
			Messages2 message = (Messages2)color;
			SetText(message.ToString());
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
