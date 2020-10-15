using System;
using System.Windows.Forms;
using CryptoFile.Client.Configuration;

namespace CryptoFile.Client.Forms
{
	public partial class AboutProgramForm : Form, IAboutProgramForm
	{
		private Language language;

		public AboutProgramForm()
		{
			InitializeComponent();
		}

		public string Email
		{
			get => emailLinkLabel.Text;
			set => emailLinkLabel.Text = value;
		}

		public event EventHandler EmailClick;

		public Language Language
		{
			get => language;
			set
			{
				language = value;
				if (language == Language.English)
				{
					Text = @"About";
					versionLabel.Text = @"Version: 1.2";
					authorLabel.Text = @"Author: Alexey Pogrebnikov";
				}

				if (language == Language.Russian)
				{
					Text = @"О программе";
					versionLabel.Text = @"Версия: 1.2";
					authorLabel.Text = @"Автор: Алексей Погребников";
				}
			}
		}

		private void emailLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (EmailClick != null)
				EmailClick(this, e);
		}
	}
}