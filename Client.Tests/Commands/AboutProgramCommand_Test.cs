using CryptoFile.Client.Commands;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Forms;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Commands {
	[TestFixture]
	public class AboutProgramCommand_Test {
		private AboutProgramCommand command;
		private Mock<IFormFactory> formFactory;
		private Mock<IEnvironmentHelper> environmentHelper;

		[SetUp]
		public void SetUp() {
			formFactory = new Mock<IFormFactory>();
			environmentHelper = new Mock<IEnvironmentHelper>();
			command = new AboutProgramCommand(formFactory.Object, environmentHelper.Object);
		}

		[Test]
		public void ExecuteTest() {
			var aboutProgramForm = new Mock<IAboutProgramForm>();
			formFactory.Setup(x => x.CreateAboutProgramForm()).Returns(aboutProgramForm.Object);

			command.Execute();

			formFactory.Verify(x => x.CreateAboutProgramForm());
		}
	}
}