using System;
using CryptoFile.Client.Commands;
using CryptoFile.Client.Controls;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Forms;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Entities.Wrappers;
using CryptoFile.IO.Unification;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Commands
{
	[TestFixture]
	public class DecipherCommand_Test
	{
		private DecipherCommand command;
		private Mock<ICommandsContainer> commandsContainer;
		private Mock<IFilesView> filesView;
		private Mock<IRsaFactory> rsaFactory;
		private Mock<IEnvironmentHelper> environmentHelper;
		private Mock<IFormFactory> formFactory;
		private Mock<IMessageHelper> messageHelper;
		private Mock<IFileUnifier> fileUnifier;

		[SetUp]
		public void SetUp()
		{
			commandsContainer = new Mock<ICommandsContainer>();
			filesView = new Mock<IFilesView>();
			rsaFactory = new Mock<IRsaFactory>();
			environmentHelper = new Mock<IEnvironmentHelper>();
			formFactory = new Mock<IFormFactory>();
			messageHelper = new Mock<IMessageHelper>();
			fileUnifier = new Mock<IFileUnifier>();
			command = new DecipherCommand(commandsContainer.Object,
				filesView.Object,
				rsaFactory.Object,
				environmentHelper.Object,
				formFactory.Object,
				messageHelper.Object,
				fileUnifier.Object);
		}

		[Test]
		public void Execute_SelectedFileIsNotRsa()
		{
			var decipherForm = new Mock<IDecipherForm>();
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.FullName).Returns("c:\\file.txt");
			fileInfo.Setup(x => x.Extension).Returns(".txt");
			fileInfo.Setup(x => x.Exists).Returns(true);

			var fileEntity = new FileEntity(fileInfo.Object);

			var rsaFileDecipher = new Mock<IRsaFileDecipher>();
			rsaFactory.Setup(x => x.CreateRsaFileDecipher(fileEntity.FullName)).Returns(rsaFileDecipher.Object);

			filesView.Setup(x => x.SelectedFile).Returns(fileEntity);
			formFactory.Setup(x => x.CreateDecipherForm()).Returns(decipherForm.Object);

			Assert.Throws(typeof(InvalidOperationException), () => command.Execute());
		}

		[Test]
		public void ExecuteTest()
		{
			var decipherForm = new Mock<IDecipherForm>();
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.FullName).Returns("c:\\file.rsa");
			fileInfo.Setup(x => x.Extension).Returns(".rsa");
			fileInfo.Setup(x => x.Exists).Returns(true);

			var fileEntity = new FileEntity(fileInfo.Object);

			var rsaFileDecipher = new Mock<IRsaFileDecipher>();
			rsaFactory.Setup(x => x.CreateRsaFileDecipher(fileEntity.FullName)).Returns(rsaFileDecipher.Object);

			filesView.Setup(x => x.SelectedFile).Returns(fileEntity);
			formFactory.Setup(x => x.CreateDecipherForm()).Returns(decipherForm.Object);

			command.Execute();

			formFactory.Verify(x => x.CreateDecipherForm());
		}
	}
}