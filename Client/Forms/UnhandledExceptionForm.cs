using System;
using System.Windows.Forms;
using CryptoFile.Client.Configuration;

namespace CryptoFile.Client.Forms {
	partial class UnhandledExceptionForm : Form, IUnhandledExceptionForm {
		private Language language;

		public UnhandledExceptionForm() {
			InitializeComponent();
		}

		public event EventHandler Send;

		public string Email {
			get { return emailTextBox.Text; }
			set { emailTextBox.Text = value; }
		}

		public string Message {
			get { return messageRichTextBox.Text; }
			set { messageRichTextBox.Text = value; }
		}

		public Language Language {
			get { return language; }
			set {
				language = value;
				if (language == Language.English) {
					Text = @"Unhandled Exception";
					messageLabel.Text = @"Message:";
					sendButton.Text = @"Send";
					cancelButton.Text = @"Cancel";
				}
				if (language == Language.Russian) {
					Text = @"Необработанное исключение";
					messageLabel.Text = @"Сообщение:";
					sendButton.Text = @"Отправить";
					cancelButton.Text = @"Отмена";
				}
			}
		}

		private void sendButton_Click(object sender, EventArgs e) {
			if (Send != null)
				Send(this, e);
		}
	}
}