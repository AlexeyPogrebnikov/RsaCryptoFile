using System.Collections.ObjectModel;
using CryptoFile.Client.Commands;
using CryptoFile.Client.Controls;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Entities.Wrappers;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Commands {
	[TestFixture]
	public class RefreshCryptoViewsCommand_Test {
		private RefreshCryptoViewsCommand command;
		private Mock<IFilesView> filesView;
		private Mock<ICryptoView> cryptoView;

		[SetUp]
		public void SetUp() {
			filesView = new Mock<IFilesView>();
			cryptoView = new Mock<ICryptoView>();
			cryptoView.SetupProperty(x => x.CipherEnabled, false);
			cryptoView.SetupProperty(x => x.DecipherEnabled, false);
			command = new RefreshCryptoViewsCommand(filesView.Object, new[] { cryptoView.Object });
		}

		[Test]
		public void Execute_SelectedEntitiesHasOneFile() {
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.Extension).Returns(".txt");

			var fileEntity = new FileEntity(fileInfo.Object);
			var fileSystemEntities = new ReadOnlyCollection<FileSystemEntity>(new[] { fileEntity });
			filesView.Setup(x => x.SelectedEntities).Returns(fileSystemEntities);

			command.Execute();

			Assert.IsTrue(cryptoView.Object.CipherEnabled);
			Assert.IsFalse(cryptoView.Object.DecipherEnabled);
		}

		[Test]
		public void Execute_SelectedEntitiesHasOneRsaFile() {
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.Extension).Returns(".rsa");

			var fileEntity = new FileEntity(fileInfo.Object);
			var fileSystemEntities = new ReadOnlyCollection<FileSystemEntity>(new[] { fileEntity });
			filesView.Setup(x => x.SelectedEntities).Returns(fileSystemEntities);

			command.Execute();

			Assert.IsFalse(cryptoView.Object.CipherEnabled);
			Assert.IsTrue(cryptoView.Object.DecipherEnabled);
		}

		[Test]
		public void Execute_SelectedEntitiesHasTwoRsaFiles() {
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.Extension).Returns(".rsa");

			var fileEntity = new FileEntity(fileInfo.Object);
			var fileSystemEntities = new ReadOnlyCollection<FileSystemEntity>(new[] { fileEntity, fileEntity });
			filesView.Setup(x => x.SelectedEntities).Returns(fileSystemEntities);

			command.Execute();

			Assert.IsTrue(cryptoView.Object.CipherEnabled);
			Assert.IsFalse(cryptoView.Object.DecipherEnabled);
		}

		[Test]
		public void Execute_SelectedEntitiesIsEmpty() {
			var fileSystemEntities = new ReadOnlyCollection<FileSystemEntity>(new FileSystemEntity[0]);
			filesView.Setup(x => x.SelectedEntities).Returns(fileSystemEntities);

			command.Execute();

			Assert.IsFalse(cryptoView.Object.CipherEnabled);
			Assert.IsFalse(cryptoView.Object.DecipherEnabled);
		}
	}
}