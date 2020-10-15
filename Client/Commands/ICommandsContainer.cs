using CryptoFile.Client.Presenters;

namespace CryptoFile.Client.Commands
{
	public interface ICommandsContainer
	{
		void SetFilesViewPresenter(IFilesViewPresenter filesViewPresenter);
		ICommand RefreshDirectoryCommand { get; }
		ICommand DecipherCommand { get; }
		ICommand CipherCommand { get; }
		ICommand GenerateKeysCommand { get; }
		ICommand AboutProgramCommand { get; }
		ICommand ChangeLanguageCommand { get; }
		ICommand ToUpperFolderCommand { get; }
		ICommand RefreshCryptoViewsCommand { get; }
	}
}