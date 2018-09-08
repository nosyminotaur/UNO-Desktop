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
			this.infoLabel = new System.Windows.Forms.Label();
			this.IPAddressOutputTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.StartServerButton = new System.Windows.Forms.Button();
			this.UserNameTextBox = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.UserNameTextBox2 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.ConnectionStatusLabel = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.IPAddressInputTextBox = new System.Windows.Forms.TextBox();
			this.JoinGameButton = new System.Windows.Forms.Button();
			this.HostGamePanel = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox5 = new System.Windows.Forms.PictureBox();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.label4 = new System.Windows.Forms.Label();
			this.CurrentCardPictureBox = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.HostGamePanel.SuspendLayout();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CurrentCardPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// infoLabel
			// 
			this.infoLabel.AutoSize = true;
			this.infoLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.infoLabel.Location = new System.Drawing.Point(-1, 326);
			this.infoLabel.Name = "infoLabel";
			this.infoLabel.Size = new System.Drawing.Size(0, 21);
			this.infoLabel.TabIndex = 10;
			// 
			// IPAddressOutputTextBox
			// 
			this.IPAddressOutputTextBox.Location = new System.Drawing.Point(138, 101);
			this.IPAddressOutputTextBox.Name = "IPAddressOutputTextBox";
			this.IPAddressOutputTextBox.Size = new System.Drawing.Size(112, 20);
			this.IPAddressOutputTextBox.TabIndex = 14;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 101);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 13);
			this.label1.TabIndex = 15;
			this.label1.Text = "Server started at:";
			// 
			// StartServerButton
			// 
			this.StartServerButton.Location = new System.Drawing.Point(138, 42);
			this.StartServerButton.Name = "StartServerButton";
			this.StartServerButton.Size = new System.Drawing.Size(112, 48);
			this.StartServerButton.TabIndex = 16;
			this.StartServerButton.Text = "Start server";
			this.StartServerButton.UseVisualStyleBackColor = true;
			this.StartServerButton.Click += new System.EventHandler(this.StartServerButton_Click);
			// 
			// UserNameTextBox
			// 
			this.UserNameTextBox.Location = new System.Drawing.Point(138, 7);
			this.UserNameTextBox.Name = "UserNameTextBox";
			this.UserNameTextBox.Size = new System.Drawing.Size(112, 20);
			this.UserNameTextBox.TabIndex = 17;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.HostGamePanel);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(579, 170);
			this.panel1.TabIndex = 20;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.UserNameTextBox2);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.ConnectionStatusLabel);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.IPAddressInputTextBox);
			this.panel2.Controls.Add(this.JoinGameButton);
			this.panel2.Location = new System.Drawing.Point(295, 13);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(276, 148);
			this.panel2.TabIndex = 23;
			// 
			// UserNameTextBox2
			// 
			this.UserNameTextBox2.Location = new System.Drawing.Point(138, 7);
			this.UserNameTextBox2.Name = "UserNameTextBox2";
			this.UserNameTextBox2.Size = new System.Drawing.Size(112, 20);
			this.UserNameTextBox2.TabIndex = 22;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 10);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(86, 13);
			this.label6.TabIndex = 20;
			this.label6.Text = "Enter Username:";
			// 
			// ConnectionStatusLabel
			// 
			this.ConnectionStatusLabel.AutoSize = true;
			this.ConnectionStatusLabel.Location = new System.Drawing.Point(138, 115);
			this.ConnectionStatusLabel.Name = "ConnectionStatusLabel";
			this.ConnectionStatusLabel.Size = new System.Drawing.Size(0, 13);
			this.ConnectionStatusLabel.TabIndex = 19;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 18;
			this.label2.Text = "Enter IP:";
			// 
			// IPAddressInputTextBox
			// 
			this.IPAddressInputTextBox.Location = new System.Drawing.Point(138, 42);
			this.IPAddressInputTextBox.Name = "IPAddressInputTextBox";
			this.IPAddressInputTextBox.Size = new System.Drawing.Size(112, 20);
			this.IPAddressInputTextBox.TabIndex = 17;
			// 
			// JoinGameButton
			// 
			this.JoinGameButton.Location = new System.Drawing.Point(138, 88);
			this.JoinGameButton.Name = "JoinGameButton";
			this.JoinGameButton.Size = new System.Drawing.Size(112, 33);
			this.JoinGameButton.TabIndex = 16;
			this.JoinGameButton.Text = "Join Game";
			this.JoinGameButton.UseVisualStyleBackColor = true;
			this.JoinGameButton.Click += new System.EventHandler(this.JoinGameButton_Click);
			// 
			// HostGamePanel
			// 
			this.HostGamePanel.Controls.Add(this.label7);
			this.HostGamePanel.Controls.Add(this.label1);
			this.HostGamePanel.Controls.Add(this.UserNameTextBox);
			this.HostGamePanel.Controls.Add(this.StartServerButton);
			this.HostGamePanel.Controls.Add(this.IPAddressOutputTextBox);
			this.HostGamePanel.Location = new System.Drawing.Point(13, 13);
			this.HostGamePanel.Name = "HostGamePanel";
			this.HostGamePanel.Size = new System.Drawing.Size(276, 148);
			this.HostGamePanel.TabIndex = 22;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(15, 10);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(86, 13);
			this.label7.TabIndex = 21;
			this.label7.Text = "Enter Username:";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label5);
			this.panel3.Controls.Add(this.button2);
			this.panel3.Controls.Add(this.button1);
			this.panel3.Controls.Add(this.pictureBox1);
			this.panel3.Controls.Add(this.pictureBox5);
			this.panel3.Controls.Add(this.pictureBox4);
			this.panel3.Controls.Add(this.pictureBox3);
			this.panel3.Controls.Add(this.pictureBox2);
			this.panel3.Controls.Add(this.label4);
			this.panel3.Controls.Add(this.CurrentCardPictureBox);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Location = new System.Drawing.Point(12, 188);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(756, 369);
			this.panel3.TabIndex = 21;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(148, 314);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(79, 13);
			this.label5.TabIndex = 31;
			this.label5.Text = "Selected Cards";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(519, 211);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(85, 37);
			this.button2.TabIndex = 30;
			this.button2.Text = "Next";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(337, 211);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(85, 37);
			this.button1.TabIndex = 29;
			this.button1.Text = "Previous";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(246, 70);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(85, 120);
			this.pictureBox1.TabIndex = 28;
			this.pictureBox1.TabStop = false;
			// 
			// pictureBox5
			// 
			this.pictureBox5.Location = new System.Drawing.Point(610, 70);
			this.pictureBox5.Name = "pictureBox5";
			this.pictureBox5.Size = new System.Drawing.Size(85, 120);
			this.pictureBox5.TabIndex = 27;
			this.pictureBox5.TabStop = false;
			// 
			// pictureBox4
			// 
			this.pictureBox4.Location = new System.Drawing.Point(519, 70);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(85, 120);
			this.pictureBox4.TabIndex = 26;
			this.pictureBox4.TabStop = false;
			// 
			// pictureBox3
			// 
			this.pictureBox3.Location = new System.Drawing.Point(428, 70);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(85, 120);
			this.pictureBox3.TabIndex = 25;
			this.pictureBox3.TabStop = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Location = new System.Drawing.Point(337, 70);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(85, 120);
			this.pictureBox2.TabIndex = 24;
			this.pictureBox2.TabStop = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(443, 36);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(59, 13);
			this.label4.TabIndex = 22;
			this.label4.Text = "Your Cards";
			// 
			// CurrentCardPictureBox
			// 
			this.CurrentCardPictureBox.Location = new System.Drawing.Point(29, 70);
			this.CurrentCardPictureBox.Name = "CurrentCardPictureBox";
			this.CurrentCardPictureBox.Size = new System.Drawing.Size(85, 120);
			this.CurrentCardPictureBox.TabIndex = 21;
			this.CurrentCardPictureBox.TabStop = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(48, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(66, 13);
			this.label3.TabIndex = 20;
			this.label3.Text = "Current Card";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(622, 57);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(135, 45);
			this.textBox1.TabIndex = 22;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(799, 602);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.infoLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Form1";
			this.Text = "Client";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.HostGamePanel.ResumeLayout(false);
			this.HostGamePanel.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CurrentCardPictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label infoLabel;
		private System.Windows.Forms.TextBox IPAddressOutputTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button StartServerButton;
		private System.Windows.Forms.TextBox UserNameTextBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel HostGamePanel;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox IPAddressInputTextBox;
		private System.Windows.Forms.Button JoinGameButton;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label ConnectionStatusLabel;
		private System.Windows.Forms.PictureBox CurrentCardPictureBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox pictureBox5;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox UserNameTextBox2;
		private System.Windows.Forms.TextBox textBox1;
	}
}

