using CryptoFile.Client.Commands;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Forms;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Commands {
	[TestFixture]
	public class ChangeLanguageCommand_Test {
		private ChangeLanguageCommand command;
		private Mock<IMainForm> mainForm;
		private Options options;

		[SetUp]
		public void SetUp() {
			options = new Options();
			command = new ChangeLanguageCommand(options);
			mainForm = new Mock<IMainForm>();
			command.SetMainForm(mainForm.Object);
		}

		[Test]
		public void Execute_CheckMainFormLanguage() {
			mainForm.SetupProperty(x => x.Language, Language.English);
			options.Language = Language.Russian;

			command.Execute();
			Assert.AreEqual(Language.Russian, mainForm.Object.Language);
		}
	}
}