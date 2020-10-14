using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CryptoFile.Client.Commands;
using CryptoFile.Client.Configuration;
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

namespace CryptoFile.Client.Tests.Presenters {
	[TestFixture]
	public class CipherFormPresenter_Test {
		private Mock<ICipherForm> cipherForm;
		private KeySerializer keySerializer;
		private Mock<ICommandsContainer> commandsContainer;
		private Mock<IFileUnifier> fileUnifier;
		private Mock<IEnvironmentHelper> environmentHelper;
		private Options options;
		private Mock<IRsaFactory> rsaFactory;
		private Mock<IMessageHelper> messageHelper;

		[SetUp]
		public void SetUp() {
			cipherForm = new Mock<ICipherForm>();
			var serializer = new BigNumberHexSerializer();
			keySerializer = new KeySerializer(serializer);
			fileUnifier = new Mock<IFileUnifier>();
			options = new Options();
			rsaFactory = new Mock<IRsaFactory>();
			environmentHelper = new Mock<IEnvironmentHelper>();
			messageHelper = new Mock<IMessageHelper>();
			commandsContainer = new Mock<ICommandsContainer>();
		}

		[Test]
		public void Constructor_CheckSetInputFileEntities() {
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.Length).Returns(100);
			fileInfo.Setup(x => x.FullName).Returns("file.jpg");
			fileInfo.Setup(x => x.Extension).Returns(".jpg");

			var fileEntity = new FileEntity(fileInfo.Object);
			cipherForm.SetupProperty(x => x.InputFileEntities, new FileSystemEntity[0]);
			CreatePresenter(new[] { fileEntity });

			Assert.AreEqual(1, cipherForm.Object.InputFileEntities.Count());
			Assert.AreEqual(fileEntity, cipherForm.Object.InputFileEntities.ElementAt(0));
		}

		[Test]
		public void Constructor_CheckSetTotalLengthIfInputFileEntitiesIsFile() {
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.Length).Returns(500);
			fileInfo.Setup(x => x.FullName).Returns("c:\\file.txt");
			fileInfo.Setup(x => x.Extension).Returns(".txt");

			var fileEntity = new FileEntity(fileInfo.Object);
			cipherForm.SetupProperty(x => x.TotalLength, 0);
			CreatePresenter(new[] { fileEntity });

			Assert.AreEqual(500, cipherForm.Object.TotalLength);
		}

		[Test]
		public void Constructor_CheckSetTotalLengthIfInputFileEntitiesIsTwoFiles() {
			var parentDirectory = new Mock<IDirectoryInfo>();
			parentDirectory.Setup(x => x.FullName).Returns("c:\\parent");
			parentDirectory.Setup(x => x.Name).Returns("parent");
			parentDirectory.Setup(x => x.Exists).Returns(true);

			var firstFileInfo = new Mock<IFileInfo>();
			firstFileInfo.Setup(x => x.Exists).Returns(true);
			firstFileInfo.Setup(x => x.Length).Returns(500);
			firstFileInfo.Setup(x => x.GetParentDirectory()).Returns(parentDirectory.Object);

			var firstFileEntity = new FileEntity(firstFileInfo.Object);

			var secondFileInfo = new Mock<IFileInfo>();
			secondFileInfo.Setup(x => x.Exists).Returns(true);
			secondFileInfo.Setup(x => x.Length).Returns(200);
			secondFileInfo.Setup(x => x.GetParentDirectory()).Returns(parentDirectory.Object);

			var secondFileEntity = new FileEntity(secondFileInfo.Object);
			cipherForm.SetupProperty(x => x.TotalLength, 0);
			CreatePresenter(new[] { firstFileEntity, secondFileEntity });

			Assert.AreEqual(700, cipherForm.Object.TotalLength);
		}

		[Test]
		public void Constructor_CheckOutputFileNameIfInputFileEntitiesIsOneFile() {
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.Extension).Returns(".so");
			fileInfo.Setup(x => x.FullName).Returns("c:\\hello\\file.so");
			var fileEntity = new FileEntity(fileInfo.Object);
			cipherForm.SetupProperty(x => x.OutputFileName);
			CreatePresenter(new[] { fileEntity });

			Assert.AreEqual("c:\\hello\\file.rsa", cipherForm.Object.OutputFileName);
		}

		[Test]
		public void Constructor_CheckOutputFileNameIfInputFileEntitiesIsTwoFiles() {
			var parentDirectory = new Mock<IDirectoryInfo>();
			parentDirectory.Setup(x => x.Exists).Returns(true);
			parentDirectory.Setup(x => x.FullName).Returns("d:\\root");
			parentDirectory.Setup(x => x.Name).Returns("root");

			var firstFileInfo = new Mock<IFileInfo>();
			firstFileInfo.Setup(x => x.Exists).Returns(true);
			firstFileInfo.Setup(x => x.Extension).Returns(".jpg");
			firstFileInfo.Setup(x => x.FullName).Returns("first.jpg");
			firstFileInfo.Setup(x => x.GetParentDirectory()).Returns(parentDirectory.Object);
			var firstFileEntity = new FileEntity(firstFileInfo.Object);

			var secondFileInfo = new Mock<IFileInfo>();
			secondFileInfo.Setup(x => x.Exists).Returns(true);
			secondFileInfo.Setup(x => x.Extension).Returns(".bmp");
			secondFileInfo.Setup(x => x.FullName).Returns("second.bmp");
			secondFileInfo.Setup(x => x.GetParentDirectory()).Returns(parentDirectory.Object);
			var secondFileEntity = new FileEntity(firstFileInfo.Object);

			cipherForm.SetupProperty(x => x.OutputFileName);
			CreatePresenter(new[] { firstFileEntity, secondFileEntity });

			Assert.AreEqual("d:\\root\\root.rsa", cipherForm.Object.OutputFileName);
		}

		[Test]
		public void Constructor_CheckOutputFileNameIfInputFileEntitiesIsOneDirectory() {
			var directoryInfo = new Mock<IDirectoryInfo>();
			directoryInfo.Setup(x => x.Exists).Returns(true);
			directoryInfo.Setup(x => x.FullName).Returns("c:\\hello");
			directoryInfo.Setup(x => x.Name).Returns("hello");

			var directoryEntity = new DirectoryEntity(directoryInfo.Object);

			cipherForm.SetupProperty(x => x.OutputFileName);
			CreatePresenter(new[] { directoryEntity });

			Assert.AreEqual("c:\\hello.rsa", cipherForm.Object.OutputFileName);
		}

		[Test]
		public void Constructor_CheckSetZip—ompression() {
			options.Zip—ompression = true;
			cipherForm.SetupProperty(x => x.Zip—ompression, false);
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.FullName).Returns("file.txt");
			fileInfo.Setup(x => x.Extension).Returns(".txt");
			var fileEntity = new FileEntity(fileInfo.Object);
			CreatePresenter(new[] { fileEntity });

			Assert.IsTrue(cipherForm.Object.Zip—ompression);
		}

		[Test]
		public void Cipher_PublicKeyHasErrors() {
			var rsaFileCipher = new Mock<IRsaFileCipher>();
			rsaFactory.Setup(x => x.CreateRsaFileCipher()).Returns(rsaFileCipher.Object);

			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.FullName).Returns("hello.txt");
			fileInfo.Setup(x => x.Extension).Returns(".txt");

			var fileEntity = new FileEntity(fileInfo.Object);
			cipherForm.Setup(x => x.PublicKey).Returns("hello");
			CreatePresenter(new[] { fileEntity });

			cipherForm.Raise(x => x.Cipher += null, EventArgs.Empty);

			messageHelper.Verify(x => x.Show("Public key has errors.", "ŒÚÍ˚Ú˚È ÍÎ˛˜ ËÏÂÂÚ Ó¯Ë·ÍË."));
		}

		[Test]
		public void Cipher_RsaFileAlreadyExists() {
			cipherForm.SetupProperty(x => x.OutputFileName);
			environmentHelper.Setup(x => x.FileExists("hello.rsa")).Returns(true);

			var rsaFileCipher = new Mock<IRsaFileCipher>();
			rsaFactory.Setup(x => x.CreateRsaFileCipher()).Returns(rsaFileCipher.Object);

			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.FullName).Returns("hello.txt");
			fileInfo.Setup(x => x.Extension).Returns(".txt");

			var fileEntity = new FileEntity(fileInfo.Object);
			cipherForm.Setup(x => x.PublicKey).Returns("hello");
			CreatePresenter(new[] { fileEntity });
			messageHelper.Setup(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButtons>())).Returns(
				DialogResult.No);

			cipherForm.Raise(x => x.Cipher += null, EventArgs.Empty);

			environmentHelper.Verify(x => x.FileExists("hello.rsa"));
			messageHelper.Verify(
				x =>
				x.Show("RSA file already exists. Would you like to overwrite it?",
				       "RSA Ù‡ÈÎ ÛÊÂ ÒÛ˘ÂÒÚ‚ÛÂÚ. ¬˚ ‰ÂÈÒÚ‚ËÚÂÎ¸ÌÓ ıÓÚËÚÂ ÔÂÂÁ‡ÔËÒ‡Ú¸ Â„Ó?", MessageBoxButtons.YesNo));
			rsaFactory.Verify(x => x.CreateRsaFileCipher(), Times.Never());
		}

		[Test]
		public void CancelCipher_ProcessIsNotStarting() {
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.FullName).Returns("hello.txt");
			fileInfo.Setup(x => x.Extension).Returns(".txt");

			var fileEntity = new FileEntity(fileInfo.Object);

			cipherForm.SetupProperty(x => x.DialogResult, DialogResult.None);

			CreatePresenter(new[] { fileEntity });

			cipherForm.Raise(x => x.CancelCipher += null, EventArgs.Empty);

			Assert.AreEqual(DialogResult.Cancel, cipherForm.Object.DialogResult);
		}

		private void CreatePresenter(IList<FileSystemEntity> inputFileEntities) {
			new CipherFormPresenter(cipherForm.Object,
			                        inputFileEntities,
			                        rsaFactory.Object,
			                        keySerializer,
			                        commandsContainer.Object,
			                        fileUnifier.Object,
			                        environmentHelper.Object,
			                        messageHelper.Object,
			                        options);
		}
	}
}