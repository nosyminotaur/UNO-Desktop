using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsFirstOne
{
	class UIUpdater
	{
		Form1 form;
		public UIUpdater(Form1 form)
		{
			this.form = form;
		}
		delegate void SetCardCallback(string text);
		delegate void SetTextCallback(string text);
		delegate void SetUpdatedPlayersCallback(string text);
		delegate void SetCurrentCardCallback(string text);

		public void SetUpdatedPlayers(string text)
		{
			if (form.InfoLabel.InvokeRequired)
			{
				SetUpdatedPlayersCallback d = new SetUpdatedPlayersCallback(SetUpdatedPlayers);
				form.Invoke(d, new object[] { text });
			}
			else
			{
				form.InfoLabel.Text = text;
			}
		}

		public void UpdateCurrentCard(string text)
		{

		}

		//public void SetText(string text)
		//{
		//	// InvokeRequired required compares the thread ID of the
		//	// calling thread to the thread ID of the creating thread.
		//	// If these threads are different, it returns true.
		//	if (form.textBox1.InvokeRequired)
		//	{
		//		SetTextCallback d = new SetTextCallback(SetText);
		//		form.Invoke(d, new object[] { text });
		//	}
		//	else
		//	{
		//		form.textBox1.Text = text;
		//	}
		//}

		//public void SetCard(string text)
		//{
		//	if (form.pictureBox1.InvokeRequired)
		//	{
		//		SetCardCallback d = new SetCardCallback(SetCard);
		//		form.Invoke(d, new object[] { text });
		//	}
		//	else
		//	{
		//		form.pictureBox1.Load(text);
		//	}
		//}
	}
}
