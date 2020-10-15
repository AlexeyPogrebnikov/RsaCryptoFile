using System;
using System.Collections.ObjectModel;
using System.Linq;
using CryptoFile.Client.Commands;
using CryptoFile.Client.Configuration;
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
	public class CipherCommand_Test
	{
		private CipherCommand cipherCommand;
		private Mock<ICommandsContainer> commandsContainer;
		private Mock<IFilesView> filesView;
		private Mock<IFormFactory> formFactory;
		private Mock<IRsaFactory> rsaFactory;
		private Mock<IEnvironmentHelper> environmentHelper;
		private Mock<IMessageHelper> messageHelper;

		[SetUp]
		public void SetUp()
		{
			var options = new Options();
			filesView = new Mock<IFilesView>();
			var fileUnifier = new Mock<IFileUnifier>();
			environmentHelper = new Mock<IEnvironmentHelper>();
			messageHelper = new Mock<IMessageHelper>();
			commandsContainer = new Mock<ICommandsContainer>();
			formFactory = new Mock<IFormFactory>();
			rsaFactory = new Mock<IRsaFactory>();
			cipherCommand = new CipherCommand(commandsContainer.Object,
				filesView.Object,
				formFactory.Object,
				fileUnifier.Object,
				environmentHelper.Object,
				options,
				rsaFactory.Object,
				messageHelper.Object);
		}

		[Test]
		public void Execute_CheckSetInputFileEntities()
		{
			var directoryInfo = new Mock<IDirectoryInfo>();
			directoryInfo.Setup(x => x.Exists).Returns(true);
			directoryInfo.Setup(x => x.FullName).Returns("c:\\documents");

			FileEntity firstFileEntity = CreateFileEntity("c:\\first.txt", directoryInfo.Object);
			FileEntity secondFileEntity = CreateFileEntity("c:\\second.txt", directoryInfo.Object);
			var fileSystemEntities = new ReadOnlyCollection<FileSystemEntity>(new[] { firstFileEntity, secondFileEntity });
			filesView.Setup(x => x.SelectedEntities).Returns(fileSystemEntities);
			var cipherForm = new Mock<ICipherForm>();
			cipherForm.SetupProperty(x => x.InputFileEntities);
			formFactory.Setup(x => x.CreateCipherForm()).Returns(cipherForm.Object);

			cipherCommand.Execute();

			Assert.AreEqual(2, cipherForm.Object.InputFileEntities.Count());
			Assert.AreEqual(firstFileEntity, cipherForm.Object.InputFileEntities.First());
			Assert.AreEqual(secondFileEntity, cipherForm.Object.InputFileEntities.ElementAt(1));
		}

		[Test]
		public void Execute_CheckErrorIfInputFileEntitiesIsEmpty()
		{
			var entities = new ReadOnlyCollection<FileSystemEntity>(new FileSystemEntity[0]);
			filesView.Setup(x => x.SelectedEntities).Returns(entities);

			Assert.Throws(typeof(InvalidOperationException), () => cipherCommand.Execute());
		}

		private static FileEntity CreateFileEntity(string fileName, IDirectoryInfo parentDirectory)
		{
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.FullName).Returns(fileName);
			fileInfo.Setup(x => x.GetParentDirectory()).Returns(parentDirectory);
			return new FileEntity(fileInfo.Object);
		}
	}
}