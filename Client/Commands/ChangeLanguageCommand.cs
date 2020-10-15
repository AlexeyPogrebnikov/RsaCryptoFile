using CryptoFile.Client.Configuration;
using CryptoFile.Client.Forms;

namespace CryptoFile.Client.Commands
{
	public class ChangeLanguageCommand : ICommand
	{
		private readonly Options options;
		private IMainForm mainForm;

		public ChangeLanguageCommand(Options options)
		{
			this.options = options;
		}

		public void Execute()
		{
			mainForm.Language = options.Language;
		}

		public void SetMainForm(IMainForm mainForm)
		{
			this.mainForm = mainForm;
		}
	}
}