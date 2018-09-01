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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.startServerButton = new System.Windows.Forms.Button();
			this.userNameTextBox = new System.Windows.Forms.TextBox();
			this.usernameLabel = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.LeftButton = new System.Windows.Forms.Button();
			this.RightButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(17, 54);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(273, 320);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 11;
			this.pictureBox1.TabStop = false;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(17, 401);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(110, 53);
			this.button1.TabIndex = 12;
			this.button1.Text = "Change";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(471, 122);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(112, 20);
			this.textBox1.TabIndex = 14;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(371, 129);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 13);
			this.label1.TabIndex = 15;
			this.label1.Text = "Server started at:";
			// 
			// startServerButton
			// 
			this.startServerButton.Location = new System.Drawing.Point(471, 68);
			this.startServerButton.Name = "startServerButton";
			this.startServerButton.Size = new System.Drawing.Size(112, 48);
			this.startServerButton.TabIndex = 16;
			this.startServerButton.Text = "Start server";
			this.startServerButton.UseVisualStyleBackColor = true;
			this.startServerButton.Click += new System.EventHandler(this.startServerButton_Click);
			// 
			// userNameTextBox
			// 
			this.userNameTextBox.Location = new System.Drawing.Point(471, 42);
			this.userNameTextBox.Name = "userNameTextBox";
			this.userNameTextBox.Size = new System.Drawing.Size(112, 20);
			this.userNameTextBox.TabIndex = 17;
			// 
			// usernameLabel
			// 
			this.usernameLabel.AutoSize = true;
			this.usernameLabel.Location = new System.Drawing.Point(371, 45);
			this.usernameLabel.Name = "usernameLabel";
			this.usernameLabel.Size = new System.Drawing.Size(58, 13);
			this.usernameLabel.TabIndex = 18;
			this.usernameLabel.Text = "Username:";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.RightButton);
			this.panel1.Controls.Add(this.LeftButton);
			this.panel1.Controls.Add(this.pictureBox2);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.startServerButton);
			this.panel1.Controls.Add(this.usernameLabel);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.userNameTextBox);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(666, 502);
			this.panel1.TabIndex = 20;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Location = new System.Drawing.Point(352, 188);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(231, 266);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox2.TabIndex = 19;
			this.pictureBox2.TabStop = false;
			this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
			// 
			// LeftButton
			// 
			this.LeftButton.Location = new System.Drawing.Point(374, 471);
			this.LeftButton.Name = "LeftButton";
			this.LeftButton.Size = new System.Drawing.Size(75, 23);
			this.LeftButton.TabIndex = 20;
			this.LeftButton.Text = "Left";
			this.LeftButton.UseVisualStyleBackColor = true;
			this.LeftButton.Click += new System.EventHandler(this.LeftButton_Click);
			// 
			// RightButton
			// 
			this.RightButton.Location = new System.Drawing.Point(490, 471);
			this.RightButton.Name = "RightButton";
			this.RightButton.Size = new System.Drawing.Size(75, 23);
			this.RightButton.TabIndex = 21;
			this.RightButton.Text = "Right";
			this.RightButton.UseVisualStyleBackColor = true;
			this.RightButton.Click += new System.EventHandler(this.RightButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(690, 602);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.infoLabel);
			this.Name = "Form1";
			this.Text = "Client";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label infoLabel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button startServerButton;
		private System.Windows.Forms.TextBox userNameTextBox;
		private System.Windows.Forms.Label usernameLabel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Button RightButton;
		private System.Windows.Forms.Button LeftButton;
	}
}

