using CryptoFile.Client.Commands;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Forms;

namespace CryptoFile.Client.Presenters
{
	internal class MainFormPresenter
	{
		public MainFormPresenter(IMainForm mainForm,
			Options options,
			IEnvironmentHelper environmentHelper,
			ICommandsContainer commandsContainer,
			IMessageHelper messageHelper,
			IFormFactory formFactory)
		{
			new MainMenuPresenter(mainForm.MainMenu, commandsContainer, options, formFactory);
			var filesViewPresenter = new FilesViewPresenter(mainForm.FilesView,
				mainForm.ToolBar,
				mainForm.AddressToolBar,
				commandsContainer,
				environmentHelper,
				options,
				messageHelper);
			commandsContainer.SetFilesViewPresenter(filesViewPresenter);
			commandsContainer.RefreshDirectoryCommand.Execute();
			new ToolBarPresenter(mainForm.ToolBar, commandsContainer);
			commandsContainer.ChangeLanguageCommand.Execute();
		}
	}
}