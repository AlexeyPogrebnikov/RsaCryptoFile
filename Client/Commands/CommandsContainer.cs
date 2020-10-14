using CryptoFile.Client.Compression;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Controls;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Forms;
using CryptoFile.Client.Presenters;
using CryptoFile.IO;
using CryptoFile.IO.Unification;
using CryptoFile.Library.Keys;
using CryptoFile.Library.Prime;

namespace CryptoFile.Client.Commands {
	class CommandsContainer : ICommandsContainer {
		private readonly ToUpperFolderCommand toUpperFolderCommand;
		private readonly RefreshDirectoryCommand refreshDirectoryCommand;
		private readonly ChangeLanguageCommand changeLanguageCommand;

		public CommandsContainer(Options options,
		                         IMainForm mainForm,
		                         IEnvironmentHelper environmentHelper,
		                         IMessageHelper messageHelper,
		                         IFormFactory formFactory) {
			var primeTest = new RabinMillerTest(20);
			var primeGenerator = new PrimeGenerator(primeTest);
			var keyGenerator = new KeyGenerator(new RsaKeyGenerator(), primeGenerator);
			var fileUnifier = new FileUnifier();
			var zipAlgorithm = new ZipAlgorithm(environmentHelper);
			IRsaFactory rsaFactory = new RsaFactory(new FileFactory(0), options, environmentHelper, zipAlgorithm);
			AboutProgramCommand = new AboutProgramCommand(formFactory, environmentHelper);
			CipherCommand = new CipherCommand(this, mainForm.FilesView, formFactory, fileUnifier, environmentHelper, options,
			                                  rsaFactory,
			                                  messageHelper);
			DecipherCommand = new DecipherCommand(this, mainForm.FilesView, rsaFactory, environmentHelper, formFactory,
			                                      messageHelper,
			                                      fileUnifier);
			ExitCommand = new ExitCommand();
			GenerateKeysCommand = new GenerateKeysCommand(keyGenerator, options, formFactory, messageHelper);
			refreshDirectoryCommand = new RefreshDirectoryCommand(messageHelper);
			toUpperFolderCommand = new ToUpperFolderCommand(messageHelper);
			changeLanguageCommand = new ChangeLanguageCommand(options);
			var cryptoViews = new ICryptoView[] { mainForm.MainMenu, mainForm.ToolBar, mainForm.FilesView };
			RefreshCryptoViewsCommand = new RefreshCryptoViewsCommand(mainForm.FilesView, cryptoViews);
		}

		public ICommand AboutProgramCommand { get; private set; }
		public ICommand CipherCommand { get; private set; }
		public ICommand DecipherCommand { get; private set; }
		public ICommand ExitCommand { get; private set; }
		public ICommand GenerateKeysCommand { get; private set; }
		public ICommand RefreshCryptoViewsCommand { get; private set; }

		public ICommand RefreshDirectoryCommand {
			get { return refreshDirectoryCommand; }
		}

		public ICommand ToUpperFolderCommand {
			get { return toUpperFolderCommand; }
		}

		public ICommand ChangeLanguageCommand {
			get { return changeLanguageCommand; }
		}

		public void SetFilesViewPresenter(IFilesViewPresenter filesViewPresenter) {
			toUpperFolderCommand.SetFilesViewPresenter(filesViewPresenter);
			refreshDirectoryCommand.FilesViewPresenter = filesViewPresenter;
		}

		public void SetMainForm(IMainForm mainForm) {
			changeLanguageCommand.SetMainForm(mainForm);
		}
	}
}