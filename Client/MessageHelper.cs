using System.Windows.Forms;
using CryptoFile.Client.Configuration;

namespace CryptoFile.Client
{
	public class MessageHelper : IMessageHelper
	{
		private readonly Options options;

		public MessageHelper(Options options)
		{
			this.options = options;
		}

		public DialogResult Show(string englishMessage, string russianMessage)
		{
			return Show(englishMessage, russianMessage, MessageBoxButtons.OK);
		}

		public DialogResult Show(string englishMessage, string russianMessage, MessageBoxButtons buttons)
		{
			var message = "";
			if (options.Language == Language.English)
			{
				message = englishMessage;
			}

			if (options.Language == Language.Russian)
			{
				message = russianMessage;
			}

			return MessageBox.Show(message, "", buttons);
		}
	}
}