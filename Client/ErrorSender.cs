using System;
using System.Windows.Forms;
using CryptoFile.Client.Mapi;
using CryptoFile.Client.Properties;

namespace CryptoFile.Client {
	public static class ErrorSender {
		public static void Send(Exception e) {
			var email = Settings.Default.Email;
			var body = e.ToString();
			try {
				var message = new MapiMessage("Unhandled Exception.", body, email);
				message.OpenInMailClient();
			} catch {
				var message = string.Format("An error occurred while sending a report. Please send the report on our e-mail: {0}",
				                            email);
				MessageBox.Show(message);
			}
		}
	}
}