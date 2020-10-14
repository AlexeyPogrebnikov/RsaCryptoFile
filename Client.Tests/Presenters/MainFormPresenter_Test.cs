using CryptoFile.Client.Commands;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Controls;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Forms;
using CryptoFile.Client.Presenters;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Presenters {
	[TestFixture]
	public class MainFormPresenter_Test {
		private Mock<IMainForm> mainForm;
		private Options options;
		private Mock<IEnvironmentHelper> environmentHelper;
		private Mock<ICommandsContainer> commandsContainer;
		private Mock<IMessageHelper> messageHelper;
		private Mock<IFormFactory> formFactory;

		[SetUp]
		public void SetUp() {
			mainForm = new Mock<IMainForm>();
			var mainMenuView = new Mock<IMainMenuView>();
			mainForm.Setup(x => x.MainMenu).Returns(mainMenuView.Object);
			var toolBarView = new Mock<IToolBarView>();
			mainForm.Setup(x => x.ToolBar).Returns(toolBarView.Object);
			var addressToolBar = new Mock<IAddressToolBar>();
			mainForm.Setup(x => x.AddressToolBar).Returns(addressToolBar.Object);
			var filesView = new Mock<IFilesView>();
			mainForm.Setup(x => x.FilesView).Returns(filesView.Object);
			options = new Options();
			environmentHelper = new Mock<IEnvironmentHelper>();
			commandsContainer = new Mock<ICommandsContainer>();
			var refreshDirectoryCommand = new Mock<ICommand>();
			commandsContainer.Setup(x => x.RefreshDirectoryCommand).Returns(refreshDirectoryCommand.Object);
			messageHelper = new Mock<IMessageHelper>();
			formFactory = new Mock<IFormFactory>();
		}

		[Test]
		public void Constructor_CheckChangeLanguage() {
			var command = new Mock<ICommand>();
			commandsContainer.Setup(x => x.ChangeLanguageCommand).Returns(command.Object);

			CreatePresenter();

			command.Verify(x => x.Execute());
		}

		private void CreatePresenter() {
			new MainFormPresenter(mainForm.Object, options, environmentHelper.Object, commandsContainer.Object,
			                      messageHelper.Object, formFactory.Object);
		}
	}
}