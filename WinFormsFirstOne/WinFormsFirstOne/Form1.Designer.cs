namespace WinFormsFirstOne
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.StartPanel = new System.Windows.Forms.Panel();
			this.UserNameLabel = new System.Windows.Forms.Label();
			this.ipAddressLabel = new System.Windows.Forms.Label();
			this.HostGameButton = new System.Windows.Forms.Button();
			this.JoinGameButton = new System.Windows.Forms.Button();
			this.StartGameJoinButton = new System.Windows.Forms.Button();
			this.IPAddressTextBox = new System.Windows.Forms.TextBox();
			this.InfoLabel = new System.Windows.Forms.Label();
			this.UserNameTextBox = new System.Windows.Forms.TextBox();
			this.GamePanel = new System.Windows.Forms.Panel();
			this.UserCardsPanel = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox5 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.currentCardPictureBox = new System.Windows.Forms.PictureBox();
			this.StartPanel.SuspendLayout();
			this.GamePanel.SuspendLayout();
			this.UserCardsPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.currentCardPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// StartPanel
			// 
			this.StartPanel.BackColor = System.Drawing.Color.White;
			this.StartPanel.Controls.Add(this.UserNameLabel);
			this.StartPanel.Controls.Add(this.ipAddressLabel);
			this.StartPanel.Controls.Add(this.HostGameButton);
			this.StartPanel.Controls.Add(this.JoinGameButton);
			this.StartPanel.Controls.Add(this.StartGameJoinButton);
			this.StartPanel.Controls.Add(this.IPAddressTextBox);
			this.StartPanel.Controls.Add(this.InfoLabel);
			this.StartPanel.Controls.Add(this.UserNameTextBox);
			this.StartPanel.Location = new System.Drawing.Point(-3, 0);
			this.StartPanel.Name = "StartPanel";
			this.StartPanel.Size = new System.Drawing.Size(805, 606);
			this.StartPanel.TabIndex = 11;
			// 
			// UserNameLabel
			// 
			this.UserNameLabel.AutoSize = true;
			this.UserNameLabel.BackColor = System.Drawing.Color.White;
			this.UserNameLabel.Font = new System.Drawing.Font("Gabriola", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.UserNameLabel.ForeColor = System.Drawing.Color.Black;
			this.UserNameLabel.Location = new System.Drawing.Point(94, 58);
			this.UserNameLabel.Name = "UserNameLabel";
			this.UserNameLabel.Size = new System.Drawing.Size(127, 39);
			this.UserNameLabel.TabIndex = 0;
			this.UserNameLabel.Text = "Enter Username!";
			// 
			// ipAddressLabel
			// 
			this.ipAddressLabel.AutoSize = true;
			this.ipAddressLabel.BackColor = System.Drawing.Color.White;
			this.ipAddressLabel.Font = new System.Drawing.Font("Gabriola", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ipAddressLabel.ForeColor = System.Drawing.Color.Black;
			this.ipAddressLabel.Location = new System.Drawing.Point(94, 124);
			this.ipAddressLabel.Name = "ipAddressLabel";
			this.ipAddressLabel.Size = new System.Drawing.Size(189, 39);
			this.ipAddressLabel.TabIndex = 2;
			this.ipAddressLabel.Text = "Enter IP Address of Server!";
			// 
			// HostGameButton
			// 
			this.HostGameButton.BackColor = System.Drawing.Color.RoyalBlue;
			this.HostGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.HostGameButton.Font = new System.Drawing.Font("Gabriola", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.HostGameButton.ForeColor = System.Drawing.Color.White;
			this.HostGameButton.Location = new System.Drawing.Point(401, 477);
			this.HostGameButton.Name = "HostGameButton";
			this.HostGameButton.Size = new System.Drawing.Size(389, 85);
			this.HostGameButton.TabIndex = 13;
			this.HostGameButton.Text = "HOST GAME!";
			this.HostGameButton.UseVisualStyleBackColor = false;
			this.HostGameButton.Click += new System.EventHandler(this.HostGameButton_Click);
			// 
			// JoinGameButton
			// 
			this.JoinGameButton.BackColor = System.Drawing.Color.RoyalBlue;
			this.JoinGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.JoinGameButton.Font = new System.Drawing.Font("Gabriola", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.JoinGameButton.ForeColor = System.Drawing.Color.White;
			this.JoinGameButton.Location = new System.Drawing.Point(15, 477);
			this.JoinGameButton.Name = "JoinGameButton";
			this.JoinGameButton.Size = new System.Drawing.Size(376, 85);
			this.JoinGameButton.TabIndex = 14;
			this.JoinGameButton.Text = "JOIN GAME!";
			this.JoinGameButton.UseVisualStyleBackColor = false;
			this.JoinGameButton.Click += new System.EventHandler(this.JoinGameButton_Click);
			// 
			// StartGameJoinButton
			// 
			this.StartGameJoinButton.BackColor = System.Drawing.Color.RoyalBlue;
			this.StartGameJoinButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.StartGameJoinButton.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.StartGameJoinButton.ForeColor = System.Drawing.Color.White;
			this.StartGameJoinButton.Location = new System.Drawing.Point(465, 205);
			this.StartGameJoinButton.Name = "StartGameJoinButton";
			this.StartGameJoinButton.Size = new System.Drawing.Size(147, 56);
			this.StartGameJoinButton.TabIndex = 4;
			this.StartGameJoinButton.Text = "Connect";
			this.StartGameJoinButton.UseVisualStyleBackColor = false;
			this.StartGameJoinButton.Click += new System.EventHandler(this.StartGameJoinButton_Click);
			// 
			// IPAddressTextBox
			// 
			this.IPAddressTextBox.Location = new System.Drawing.Point(465, 124);
			this.IPAddressTextBox.Name = "IPAddressTextBox";
			this.IPAddressTextBox.Size = new System.Drawing.Size(226, 20);
			this.IPAddressTextBox.TabIndex = 3;
			// 
			// InfoLabel
			// 
			this.InfoLabel.AutoSize = true;
			this.InfoLabel.BackColor = System.Drawing.Color.White;
			this.InfoLabel.Font = new System.Drawing.Font("Gabriola", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.InfoLabel.ForeColor = System.Drawing.Color.Black;
			this.InfoLabel.Location = new System.Drawing.Point(94, 345);
			this.InfoLabel.Name = "InfoLabel";
			this.InfoLabel.Size = new System.Drawing.Size(162, 39);
			this.InfoLabel.TabIndex = 15;
			this.InfoLabel.Text = "Press Connect to Start!";
			// 
			// UserNameTextBox
			// 
			this.UserNameTextBox.Location = new System.Drawing.Point(465, 61);
			this.UserNameTextBox.Name = "UserNameTextBox";
			this.UserNameTextBox.Size = new System.Drawing.Size(226, 20);
			this.UserNameTextBox.TabIndex = 1;
			// 
			// GamePanel
			// 
			this.GamePanel.BackColor = System.Drawing.Color.White;
			this.GamePanel.Controls.Add(this.UserCardsPanel);
			this.GamePanel.Controls.Add(this.label2);
			this.GamePanel.Controls.Add(this.label1);
			this.GamePanel.Controls.Add(this.currentCardPictureBox);
			this.GamePanel.Location = new System.Drawing.Point(0, 0);
			this.GamePanel.Name = "GamePanel";
			this.GamePanel.Size = new System.Drawing.Size(802, 606);
			this.GamePanel.TabIndex = 16;
			// 
			// UserCardsPanel
			// 
			this.UserCardsPanel.Controls.Add(this.pictureBox1);
			this.UserCardsPanel.Controls.Add(this.pictureBox5);
			this.UserCardsPanel.Controls.Add(this.pictureBox2);
			this.UserCardsPanel.Controls.Add(this.pictureBox4);
			this.UserCardsPanel.Controls.Add(this.pictureBox3);
			this.UserCardsPanel.Location = new System.Drawing.Point(213, 37);
			this.UserCardsPanel.Name = "UserCardsPanel";
			this.UserCardsPanel.Size = new System.Drawing.Size(524, 140);
			this.UserCardsPanel.TabIndex = 8;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(100, 140);
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.UserCardPictureBox_Click);
			// 
			// pictureBox5
			// 
			this.pictureBox5.Location = new System.Drawing.Point(424, 0);
			this.pictureBox5.Name = "pictureBox5";
			this.pictureBox5.Size = new System.Drawing.Size(100, 140);
			this.pictureBox5.TabIndex = 7;
			this.pictureBox5.TabStop = false;
			this.pictureBox5.Click += new System.EventHandler(this.UserCardPictureBox_Click);
			// 
			// pictureBox2
			// 
			this.pictureBox2.Location = new System.Drawing.Point(106, 0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(100, 140);
			this.pictureBox2.TabIndex = 4;
			this.pictureBox2.TabStop = false;
			this.pictureBox2.Click += new System.EventHandler(this.UserCardPictureBox_Click);
			// 
			// pictureBox4
			// 
			this.pictureBox4.Location = new System.Drawing.Point(318, 0);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(100, 140);
			this.pictureBox4.TabIndex = 6;
			this.pictureBox4.TabStop = false;
			this.pictureBox4.Click += new System.EventHandler(this.UserCardPictureBox_Click);
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(212, 0);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(100, 140);
			this.pictureBox3.TabIndex = 5;
			this.pictureBox3.TabStop = false;
			this.pictureBox3.Click += new System.EventHandler(this.UserCardPictureBox_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(413, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Your Cards";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Current Card";
			// 
			// currentCardPictureBox
			// 
			this.currentCardPictureBox.Location = new System.Drawing.Point(15, 37);
			this.currentCardPictureBox.Name = "currentCardPictureBox";
			this.currentCardPictureBox.Size = new System.Drawing.Size(100, 140);
			this.currentCardPictureBox.TabIndex = 0;
			this.currentCardPictureBox.TabStop = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DimGray;
			this.ClientSize = new System.Drawing.Size(799, 602);
			this.Controls.Add(this.GamePanel);
			this.Controls.Add(this.StartPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "UNO";
			this.StartPanel.ResumeLayout(false);
			this.StartPanel.PerformLayout();
			this.GamePanel.ResumeLayout(false);
			this.GamePanel.PerformLayout();
			this.UserCardsPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.currentCardPictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel StartPanel;
		private System.Windows.Forms.Button HostGameButton;
		private System.Windows.Forms.Button JoinGameButton;
		private System.Windows.Forms.Label UserNameLabel;
		private System.Windows.Forms.TextBox IPAddressTextBox;
		private System.Windows.Forms.Label ipAddressLabel;
		private System.Windows.Forms.TextBox UserNameTextBox;
		private System.Windows.Forms.Button StartGameJoinButton;
		public System.Windows.Forms.Label InfoLabel;
		private System.Windows.Forms.Panel GamePanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox currentCardPictureBox;
		private System.Windows.Forms.PictureBox pictureBox5;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel UserCardsPanel;
	}
}

