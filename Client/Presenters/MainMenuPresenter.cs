using System;
using System.Windows.Forms;
using CryptoFile.Client.Commands;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Controls;
using CryptoFile.Client.Forms;

namespace CryptoFile.Client.Presenters
{
	internal class MainMenuPresenter
	{
		private readonly IMainMenuView mainMenuView;
		private readonly ICommandsContainer commandsContainer;
		private readonly Options options;
		private readonly IFormFactory formFactory;

		public MainMenuPresenter(IMainMenuView mainMenuView,
			ICommandsContainer commandsContainer,
			Options options,
			IFormFactory formFactory)
		{
			this.mainMenuView = mainMenuView;
			this.commandsContainer = commandsContainer;
			this.options = options;
			this.formFactory = formFactory;
			mainMenuView.Cipher += mainMenuView_Cipher;
			mainMenuView.Decipher += mainMenuView_Decipher;
			mainMenuView.GenerateKeys += mainMenuView_GenerateKeys;
			mainMenuView.Exit += mainMenuView_Exit;
			mainMenuView.AboutProgram += mainMenuView_AboutProgram;
			mainMenuView.Properties += mainMenuView_Properties;
			mainMenuView.LanguageChanged += mainMenuView_LanguageChanged;
			mainMenuView.Language = options.Language;
		}

		private void mainMenuView_LanguageChanged(object sender, EventArgs e)
		{
			options.Language = mainMenuView.Language;
			commandsContainer.ChangeLanguageCommand.Execute();
		}

		private void mainMenuView_Properties(object sender, EventArgs e)
		{
			using (IPropertiesForm form = formFactory.CreatePropertiesForm())
			{
				form.RsaFileColor = options.RsaFileColor.Color;
				if (form.ShowDialog() == DialogResult.OK)
				{
					options.RsaFileColor.Color = form.RsaFileColor;
					commandsContainer.RefreshDirectoryCommand.Execute();
				}
			}
		}

		private void mainMenuView_Decipher(object sender, EventArgs e)
		{
			commandsContainer.DecipherCommand.Execute();
		}

		private void mainMenuView_Cipher(object sender, EventArgs e)
		{
			commandsContainer.CipherCommand.Execute();
		}

		private static void mainMenuView_Exit(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void mainMenuView_GenerateKeys(object sender, EventArgs e)
		{
			commandsContainer.GenerateKeysCommand.Execute();
		}

		private void mainMenuView_AboutProgram(object sender, EventArgs e)
		{
			commandsContainer.AboutProgramCommand.Execute();
		}
	}
}