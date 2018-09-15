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
		List<PictureBox> UserCardsPictureBoxes;
		const int port = 8081;
		ServerNew server;
		ClientNew client;
		GameState gameState;
		ClientState clientState;
		UIUpdater uiUpdater;
		private BackgroundWorker backgroundWorker1;
		private BackgroundWorker backgroundWorker2;

		public Form1()
		{
			InitializeComponent();
			StartPanel.Visible = true;
			GamePanel.Visible = false;
			GetAllPictureBoxes();
			gameState = new GameState();
			gameState.ReceivedSizedSizeChanged += GameState_ReceivedSizedSizeChanged;
			backgroundWorker1 = new BackgroundWorker();
			backgroundWorker2 = new BackgroundWorker();

			backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
			backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;

			backgroundWorker2.DoWork += BackgroundWorker2_DoWork;
			backgroundWorker2.RunWorkerCompleted += BackgroundWorker2_RunWorkerCompleted;

			uiUpdater = new UIUpdater(this);
		}

		private void BackgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
		{
			e.Result = e.Argument;
		}

		private void BackgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			GamePanel.Visible = true;
			StartPanel.Visible = false;
			UNOCard card = (UNOCard)e.Result;
			currentCardPictureBox.Load(GetImageName(card));
		}

		private void GameState_ReceivedSizedSizeChanged(object sender, EventArgs e)
		{
			Debug.WriteLine("Received Size changed");
		}

		private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			GamePanel.Visible = true;
			StartPanel.Visible = false;
			List<UNOCard> cards = (List<UNOCard>)e.Result;
			for (int i = 0; i < UserCardsPictureBoxes.Count; i++)
			{
				string cardName = GetImageName(cards[i]);
				UserCardsPictureBoxes[i].Load(cardName);
			}
		}

		private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			e.Result = e.Argument;
		}

		private void GetAllPictureBoxes()
		{
			UserCardsPictureBoxes = new List<PictureBox>();
			foreach (Control control in UserCardsPanel.Controls)
			{
				PictureBox pictureBox = (PictureBox)control;
				pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
				UserCardsPictureBoxes.Add(pictureBox);
			}
			currentCardPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
		}

		//private void PictureBoxUpdated(object sender, DoWorkEventArgs e)
		//{
		//	string text = (string)e.Argument;
		//	this.pictureBox1.Load(text);
		//}

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

		private void OnCurrentCardReceived(object sender, ClientStateEventArgs e)
		{
			//string text = GetImageName(card);
			//uiUpdater.SetCard(text);
		}

		//private void OnUserCardsReceived(object sender, ClientStateEventArgs e)
		//{
		//	StringBuilder stringBuilder = new StringBuilder();
		//	foreach (UNOCard card in e.clientState.userCards)
		//	{
		//		int data = card.GetColor();
		//		Constants.Colors colors = (Constants.Colors)data;
		//		stringBuilder.Append(colors.ToString());
		//	}
		//	uiUpdater.SetText(stringBuilder.ToString());
		//	UNOCard _card = e.clientState.userCards[0];
		//	//string text = GetImageName(_card);
		//	//uiUpdater.SetCard(text);
		//	//backgroundWorker1.RunWorkerAsync(text);
		//}

		//private void OnOtherPlayerNamesReceived(object sender, ClientStateEventArgs e)
		//{
		//	string data = e.clientState.otherPlayerNames[0];
		//	Debug.WriteLine("Setting name!");
		//	uiUpdater.SetText(data);
		//}

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

		private void HostGameButton_Click(object sender, EventArgs e)
		{
			HostGameButton.Visible = false;
			if (!JoinGameButton.Visible)
			{
				JoinGameButton.Visible = true;
			}
			StartGameJoinButton.Text = "Start Server";
			ipAddressLabel.Text = "IP Address of Server:";
			InfoLabel.Text = "Press Start Server to Start!";
		}

		private void JoinGameButton_Click(object sender, EventArgs e)
		{
			JoinGameButton.Visible = false;
			if (!HostGameButton.Visible)
			{
				HostGameButton.Visible = true;
			}
			StartGameJoinButton.Text = "Connect";
			InfoLabel.Text = "Press Connect to Start!";
		}

		private void StartGameJoinButton_Click(object sender, EventArgs e)
		{
			//Same for both hosting and joining game
			if (JoinGameButton.Visible && !HostGameButton.Visible)
			{
				//User wants to Host game
				Debug.WriteLine("Trying to Host game");

				string userName = UserNameTextBox.Text;
				
				if (CheckUserName(userName))
				{
					Debug.WriteLine("Username is valid");
					//disable connect button to prevent further tries
					StartGameJoinButton.Visible = false;
					InfoLabel.Text = "Starting server, waiting for players...";
					//Starts server and also connects to the server
					if (StartServer(userName))
					{
						InfoLabel.Text = "Successfully connected to server!";
						IPAddressTextBox.Text = server.GetIPAddress().ToString().Split(':')[0];
					}
				}
				else
				{
					Debug.WriteLine("Invalid username");
					MessageBox.Show("Special Characters not allowed, try again", "Error", MessageBoxButtons.OK);
				}
			}
			else
			{
				//User wants to join game
				Debug.WriteLine("Trying to Join game");
				string userName = UserNameTextBox.Text;
				if (CheckUserName(userName))
				{
					Debug.WriteLine("Username is valid");
					//disable connect button to prevent further tries
					StartGameJoinButton.Visible = false;
					InfoLabel.Text = "Trying to connect...";
					//Connects to server
					if (!IPAddress.TryParse(IPAddressTextBox.Text, out IPAddress ipAddress))
					{
						MessageBox.Show("IP Address entered incorrectly", "Error");
						StartGameJoinButton.Visible = true;
					}
					else
					{
						if (JoinServer(userName, ipAddress))
						{
							InfoLabel.Text = "Client successfully connected, waiting for information";
						}
						else
						{
							InfoLabel.Text = "Server does not exist, please try again!";
						}
					}
				}
				else
				{
					Debug.WriteLine("Invalid username");
					MessageBox.Show("Special Characters not allowed, try again", "Error", MessageBoxButtons.OK);
				}
			}
		}

		private bool StartServer(string username)
		{
			server = new ServerNew(gameState);
			server.PlayersChanged += GameState_PlayersChanged;
			if (server.Initialize(username))
			{
				//Server started successfully, begin accepting connections
				server.StartAccept();
				IPAddress ip = server.GetIPAddress();
				clientState = new ClientState(username);
				client = new ClientNew(ip, port, username, clientState);
				client.UserCardsReceived += Client_UserCardsReceived;
				client.CurrentCardReceived += Client_CurrentCardReceived;
				client.OtherPlayerCardsReceived += Client_OtherPlayerCardsReceived;
				client.OtherPlayerNamesReceived += Client_OtherPlayerNamesReceived;
				client.Start();
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool JoinServer(string username, IPAddress ipAddress)
		{
			clientState = new ClientState(username);
			client = new ClientNew(ipAddress, port, username, clientState);

			client.UserCardsReceived += Client_UserCardsReceived;
			client.CurrentCardReceived += Client_CurrentCardReceived;
			client.OtherPlayerCardsReceived += Client_OtherPlayerCardsReceived;
			client.OtherPlayerNamesReceived += Client_OtherPlayerNamesReceived;

			try
			{
				client.Start();
				return true;
			}
			catch (Exception e)
			{
				Debug.WriteLine("Some error occured, "+ e.ToString());
				MessageBox.Show("Error, try again", "Connection failed", MessageBoxButtons.OK);
				Debug.WriteLine(e.ToString());
				return false;
			}
		}

		private void Client_OtherPlayerNamesReceived(object sender, ClientStateEventArgs e)
		{
			Debug.WriteLine("OtherPlayerNames Received");
		}

		private void Client_OtherPlayerCardsReceived(object sender, ClientStateEventArgs e)
		{
			Debug.WriteLine("OtherPlayerCards Received");
		}

		private void Client_CurrentCardReceived(object sender, ClientStateEventArgs e)
		{
			Debug.WriteLine("CurrentCard received");
			UNOCard currentCard = e.currentCard;
			backgroundWorker2.RunWorkerAsync(currentCard);
		}

		private void Client_UserCardsReceived(object sender, ClientStateEventArgs e)
		{
			List<UNOCard> userCards = e.userCards;
			backgroundWorker1.RunWorkerAsync(userCards);
		}

		private void GameState_PlayersChanged(object sender, PlayersEventArgs e)
		{
			Debug.WriteLine("Inside GameState_PlayersChanged");

			List<PlayerState> players = e.playerList;
			string text = players[players.Count - 1].playerName + " Connected!";

			uiUpdater.SetUpdatedPlayers(text);
		}

		private bool CheckUserName(string username)
		{
			return !username.Any(ch => !Char.IsLetterOrDigit(ch));
		}

		private void UserCardPictureBox_Click(object sender, EventArgs e)
		{

		}
	}
}
