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
	public partial class Form1Legacy1 : Form
	{
		const int port = 8081;
		ServerNew server;
		ClientNew client;
		ClientState clientState;
		UIUpdater uiUpdater;
		private BackgroundWorker backgroundWorker1;
		public Form1Legacy1()
		{
			InitializeComponent();
			server = new ServerNew();
			backgroundWorker1 = new BackgroundWorker();
			uiUpdater = new UIUpdater(this);
		}

		private void PictureBoxUpdated(object sender, DoWorkEventArgs e)
		{
			string text = (string)e.Argument;
			this.pictureBox1.Load(text);
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
				backgroundWorker1.DoWork += PictureBoxUpdated;
				client.OtherPlayerNamesReceived += OnOtherPlayerNamesReceived;
				client.UserCardsReceived += OnUserCardsReceived;
				client.CurrentCardReceived += OnCurrentCardReceived;
				client.Start();
			}
			else
			{
				MessageBox.Show("IP incorrectly entered, try again!");
			}
		}

		private void OnCurrentCardReceived(object sender, ClientStateEventArgs e)
		{
			UNOCard card = e.clientState.currentCard;
			string text = GetImageName(card);
			uiUpdater.SetCard(text);
		}

		private void OnUserCardsReceived(object sender, ClientStateEventArgs e)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (UNOCard card in e.clientState.userCards)
			{
				int data = card.GetColor();
				Constants.Colors colors = (Constants.Colors)data;
				stringBuilder.Append(colors.ToString());
			}
			uiUpdater.SetText(stringBuilder.ToString());
			UNOCard _card = e.clientState.userCards[0];
			string text = GetImageName(_card);
			//uiUpdater.SetCard(text);
			backgroundWorker1.RunWorkerAsync(text);
		}

		private void OnOtherPlayerNamesReceived(object sender, ClientStateEventArgs e)
		{
			string data = e.clientState.otherPlayerNames[0];
			Debug.WriteLine("Setting name!");
			uiUpdater.SetText(data);
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
