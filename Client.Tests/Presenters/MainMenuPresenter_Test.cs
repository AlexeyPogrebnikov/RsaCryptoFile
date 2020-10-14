using System;
using CryptoFile.Client.Commands;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Controls;
using CryptoFile.Client.Forms;
using CryptoFile.Client.Presenters;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Presenters {
	[TestFixture]
	public class MainMenuPresenter_Test {
		private Mock<IMainMenuView> mainMenuView;
		private Mock<ICommandsContainer> commandsContainer;
		private Options options;
		private Mock<IFormFactory> formFactory;

		[SetUp]
		public void SetUp() {
			mainMenuView = new Mock<IMainMenuView>();
			options = new Options();
			commandsContainer = new Mock<ICommandsContainer>();
			formFactory = new Mock<IFormFactory>();
			new MainMenuPresenter(mainMenuView.Object, commandsContainer.Object, options, formFactory.Object);
		}

		[Test]
		public void LanguageChanged_LanguageIsRussian() {
			var changeLanguageCommand = new Mock<ICommand>();
			commandsContainer.Setup(x => x.ChangeLanguageCommand).Returns(changeLanguageCommand.Object);
			mainMenuView.SetupProperty(x => x.Language, Language.Russian);
			mainMenuView.Raise(x => x.LanguageChanged += null, EventArgs.Empty);

			Assert.AreEqual(Language.Russian, options.Language);
			changeLanguageCommand.Verify(x => x.Execute());
		}

		[Test]
		public void LanguageChanged_LanguageIsEnglish() {
			options.Language = Language.Russian;
			var changeLanguageCommand = new Mock<ICommand>();
			commandsContainer.Setup(x => x.ChangeLanguageCommand).Returns(changeLanguageCommand.Object);
			mainMenuView.SetupProperty(x => x.Language, Language.English);
			mainMenuView.Raise(x => x.LanguageChanged += null, EventArgs.Empty);

			Assert.AreEqual(Language.English, options.Language);
			changeLanguageCommand.Verify(x => x.Execute());
		}

		[Test]
		public void PropertiesTest() {
			options.RsaFileColor = new ColorXml();
			var propertiesForm = new Mock<IPropertiesForm>();
			formFactory.Setup(x => x.CreatePropertiesForm()).Returns(propertiesForm.Object);
			mainMenuView.Raise(x => x.Properties += null, EventArgs.Empty);

			formFactory.Verify(x => x.CreatePropertiesForm());
		}
	}
}