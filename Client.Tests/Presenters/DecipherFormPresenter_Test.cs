using System;
using System.Windows.Forms;
using CryptoFile.Client.Commands;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Forms;
using CryptoFile.Client.Presenters;
using CryptoFile.Client.Serialization;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Entities.Wrappers;
using CryptoFile.IO.Unification;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Presenters
{
	[TestFixture]
	public class DecipherFormPresenter_Test
	{
		private Mock<IDecipherForm> decipherForm;
		private Mock<IRsaFactory> rsaFactory;
		private KeySerializer keySerializer;
		private Mock<ICommandsContainer> commandsContainer;
		private Mock<IEnvironmentHelper> environmentHelper;
		private Mock<IMessageHelper> messageHelper;
		private Mock<IFileUnifier> fileUnifier;

		[SetUp]
		public void SetUp()
		{
			decipherForm = new Mock<IDecipherForm>();
			rsaFactory = new Mock<IRsaFactory>();
			keySerializer = new KeySerializer(new BigNumberHexSerializer());
			environmentHelper = new Mock<IEnvironmentHelper>();
			messageHelper = new Mock<IMessageHelper>();
			commandsContainer = new Mock<ICommandsContainer>();
			fileUnifier = new Mock<IFileUnifier>();
		}

		[Test]
		public void Constructor_CheckSetOutputDirectoryPath()
		{
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.Extension).Returns(".rsa");
			fileInfo.Setup(x => x.FullName).Returns("c:\\file.rsa");

			var initialFile = new FileEntity(fileInfo.Object);
			decipherForm.SetupProperty(x => x.OutputDirectoryPath);
			var rsaFileDecipher = new Mock<IRsaFileDecipher>();
			rsaFactory.Setup(x => x.CreateRsaFileDecipher(initialFile.FullName)).Returns(rsaFileDecipher.Object);
			CreatePresenter(initialFile);

			Assert.AreEqual("c:\\file", decipherForm.Object.OutputDirectoryPath);
		}

		[Test]
		public void CancelCipher_ProcessIsNotStarting()
		{
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.FullName).Returns("hello.rsa");
			fileInfo.Setup(x => x.Extension).Returns(".rsa");

			var fileEntity = new FileEntity(fileInfo.Object);

			decipherForm.SetupProperty(x => x.DialogResult, DialogResult.None);

			CreatePresenter(fileEntity);

			decipherForm.Raise(x => x.CancelDecipher += null, EventArgs.Empty);

			Assert.AreEqual(DialogResult.Cancel, decipherForm.Object.DialogResult);
		}

		[Test]
		public void Decipher_OutputPathAlreadyExists()
		{
			decipherForm.SetupProperty(x => x.OutputDirectoryPath);

			const string path = "t:\\dir";
			environmentHelper.Setup(x => x.DirectoryExists(path)).Returns(true);
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.FullName).Returns("t:\\dir.rsa");
			fileInfo.Setup(x => x.Extension).Returns(".rsa");

			var fileEntity = new FileEntity(fileInfo.Object);
			messageHelper.Setup(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>())).Returns(
				DialogResult.No);

			CreatePresenter(fileEntity);

			decipherForm.Raise(x => x.Decipher += null, EventArgs.Empty);

			environmentHelper.Verify(x => x.DirectoryExists(path));

			messageHelper.Verify(
				x =>
					x.Show("Directory already exists. Would you like to overwrite it?",
						"Папка уже существует. Вы хотите перезаписать ее?", MessageBoxButtons.YesNo));

			rsaFactory.Verify(x => x.CreateRsaFileDecipher(It.IsAny<string>()), Times.Never());
		}

		private void CreatePresenter(FileSystemEntity initialFile)
		{
			new DecipherFormPresenter(decipherForm.Object,
				rsaFactory.Object,
				keySerializer,
				commandsContainer.Object,
				initialFile,
				environmentHelper.Object,
				messageHelper.Object,
				fileUnifier.Object);
		}
	}
}