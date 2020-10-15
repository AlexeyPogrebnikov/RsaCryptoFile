using CryptoFile.Client.Commands;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Forms;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Commands
{
	[TestFixture]
	public class GenerateKeysCommand_Test
	{
		private GenerateKeysCommand command;
		private Mock<IKeyGenerator> keyGenerator;
		private Options options;
		private Mock<IFormFactory> formFactory;
		private Mock<IMessageHelper> messageHelper;

		[SetUp]
		public void SetUp()
		{
			keyGenerator = new Mock<IKeyGenerator>();
			options = new Options();
			formFactory = new Mock<IFormFactory>();
			messageHelper = new Mock<IMessageHelper>();
			command = new GenerateKeysCommand(keyGenerator.Object, options, formFactory.Object, messageHelper.Object);
		}

		[Test]
		public void ExecuteTest()
		{
			var generateKeysForm = new Mock<IGenerateKeysForm>();
			formFactory.Setup(x => x.CreateGenerateKeysForm()).Returns(generateKeysForm.Object);

			command.Execute();

			formFactory.Verify(x => x.CreateGenerateKeysForm());
		}
	}
}