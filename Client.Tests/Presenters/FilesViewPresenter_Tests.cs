using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using CryptoFile.Client.Commands;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Controls;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Presenters;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Entities.Wrappers;
using CryptoFile.IO.Sorting;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Presenters {
	[TestFixture]
	public class FilesViewPresenter_Tests {
		private FilesViewPresenter filesViewPresenter;
		private Mock<IFilesView> filesView;
		private Mock<IToolBarView> toolBarView;
		private Mock<IAddressToolBar> addressToolBar;
		private Mock<ICommandsContainer> commandsContainer;
		private Mock<IEnvironmentHelper> environmentHelper;
		private Options options;
		private Mock<IMessageHelper> messageHelper;
		private const string testFolder = "testFolder";

		[SetUp]
		public void SetUp() {
			DeleteTestFolder();
			Directory.CreateDirectory(testFolder);
			filesView = new Mock<IFilesView>();
			toolBarView = new Mock<IToolBarView>();
			addressToolBar = new Mock<IAddressToolBar>();
			environmentHelper = new Mock<IEnvironmentHelper>();
			messageHelper = new Mock<IMessageHelper>();
			commandsContainer = new Mock<ICommandsContainer>();
			options = new Options();
		}

		[TearDown]
		public void TearDown() {
			DeleteTestFolder();
		}

		[Test]
		public void OpenDefaultDirectory_CheckRefreshOptionsInitialDirectory() {
			var path = Path.Combine(testFolder, "documents");
			Directory.CreateDirectory(path);

			environmentHelper.Setup(x => x.GetMyDocumentsPath())
				.Returns(path);

			var directoryEntity = new Mock<IDirectoryEntity>();

			directoryEntity.Setup(x => x.GetDirectories()).Returns(new List<FileSystemEntity>());

			directoryEntity.Setup(x => x.GetFiles()).Returns(new List<FileSystemEntity>());

			directoryEntity.Setup(x => x.FullName).Returns(path);

			filesViewPresenter = CreateFilesViewPresenter();

			filesViewPresenter.OpenDefaultDirectory();

			Assert.AreEqual(Path.GetFullPath(path), options.InitialDirectory);
		}

		[Test]
		public void RefreshDirectory_CheckRefreshOptionsInitialDirectory() {
			var path = Path.Combine(testFolder, "documents");
			Directory.CreateDirectory(path);

			environmentHelper.Setup(x => x.GetMyDocumentsPath())
				.Returns(path);

			var directoryEntity = new Mock<IDirectoryEntity>();

			directoryEntity.Setup(x => x.GetDirectories()).Returns(new List<FileSystemEntity>());

			directoryEntity.Setup(x => x.GetFiles()).Returns(new List<FileSystemEntity>());

			directoryEntity.Setup(x => x.FullName).Returns(path);

			filesViewPresenter = CreateFilesViewPresenter();

			filesViewPresenter.RefreshDirectory();

			Assert.AreEqual(Path.GetFullPath(path), options.InitialDirectory);
		}

		[Test]
		public void ToUpperFolder_CheckRefreshOptionsInitialDirectory() {
			var path = Path.Combine(testFolder, "documents");
			Directory.CreateDirectory(path);

			environmentHelper.Setup(x => x.GetMyDocumentsPath())
				.Returns(path);

			var directoryEntity = new Mock<IDirectoryEntity>();

			directoryEntity.Setup(x => x.GetDirectories()).Returns(new List<FileSystemEntity>());

			directoryEntity.Setup(x => x.GetFiles()).Returns(new List<FileSystemEntity>());

			directoryEntity.Setup(x => x.FullName).Returns(path);

			var parentDirectory = new Mock<IDirectoryEntity>();

			parentDirectory.Setup(x => x.GetDirectories()).Returns(new List<FileSystemEntity>());

			parentDirectory.Setup(x => x.GetFiles()).Returns(new List<FileSystemEntity>());

			parentDirectory.Setup(x => x.FullName).Returns(testFolder);

			directoryEntity.Setup(x => x.GetParentDirectory()).Returns(parentDirectory.Object);

			filesViewPresenter = CreateFilesViewPresenter();

			filesViewPresenter.ToUpperFolder();

			Assert.AreEqual(Path.GetFullPath(testFolder), options.InitialDirectory);
		}

		[Test]
		public void RefreshDirectory_OptionsInitialDirectoryIsNotNull() {
			options.InitialDirectory = testFolder;

			var directoryEntity = new Mock<IDirectoryEntity>();

			directoryEntity.Setup(x => x.GetDirectories()).Returns(new List<FileSystemEntity>());

			directoryEntity.Setup(x => x.GetFiles()).Returns(new List<FileSystemEntity>());

			directoryEntity.Setup(x => x.FullName).Returns(testFolder);

			filesViewPresenter = CreateFilesViewPresenter();

			filesViewPresenter.RefreshDirectory();

			Assert.AreEqual(Path.GetFullPath(testFolder), options.InitialDirectory);
		}

		[Test]
		public void SortByLength_CheckInitialSortColumn() {
			var path = Path.Combine(testFolder, "documents");
			Directory.CreateDirectory(path);

			environmentHelper.Setup(x => x.GetMyDocumentsPath())
				.Returns(path);

			var directoryEntity = new Mock<IDirectoryEntity>();

			directoryEntity.Setup(x => x.GetDirectories()).Returns(new List<FileSystemEntity>());

			directoryEntity.Setup(x => x.GetFiles()).Returns(new List<FileSystemEntity>());

			directoryEntity.Setup(x => x.FullName).Returns(path);

			filesViewPresenter = CreateFilesViewPresenter();

			filesView.Raise(x => x.SortByLength += null, EventArgs.Empty);

			Assert.AreEqual(SortColumn.Length, options.InitialSortColumn);
		}

		[Test]
		public void DoubleSortByLength_CheckInitialSortDirection() {
			var path = Path.Combine(testFolder, "documents");
			Directory.CreateDirectory(path);

			environmentHelper.Setup(x => x.GetMyDocumentsPath())
				.Returns(path);

			var directoryEntity = new Mock<IDirectoryEntity>();

			directoryEntity.Setup(x => x.GetDirectories()).Returns(new List<FileSystemEntity>());

			directoryEntity.Setup(x => x.GetFiles()).Returns(new List<FileSystemEntity>());

			directoryEntity.Setup(x => x.FullName).Returns(path);

			filesViewPresenter = CreateFilesViewPresenter();

			filesView.Raise(x => x.SortByLength += null, EventArgs.Empty);
			filesView.Raise(x => x.SortByLength += null, EventArgs.Empty);

			Assert.AreEqual(SortDirection.Descending, options.InitialSortDirection);
		}

		[Test]
		public void RefreshDirectory_CheckSortByLength() {
			var path = Path.Combine(testFolder, "documents");
			Directory.CreateDirectory(path);

			options.InitialSortColumn = SortColumn.Length;
			options.InitialSortDirection = SortDirection.Descending;

			environmentHelper.Setup(x => x.GetMyDocumentsPath())
				.Returns(path);

			var firstFileName = Path.Combine(path, "first.txt");
			File.WriteAllBytes(firstFileName, new byte[50]);

			var secondFileName = Path.Combine(path, "second.txt");
			File.WriteAllBytes(secondFileName, new byte[100]);

			filesViewPresenter = CreateFilesViewPresenter();

			filesViewPresenter.RefreshDirectory();

			filesView.Verify(
				x =>
				x.SetFileSystemEntities(
					It.Is<IEnumerable<FileSystemEntity>>(files =>
					                                     files.ElementAt(0).Length == 100 &&
					                                     files.ElementAt(1).Length == 50)));
		}

		[Test]
		public void RefreshDirectory_CheckSetRsaFileColor() {
			var path = Path.Combine(testFolder, "documents");
			Directory.CreateDirectory(path);

			var color = Color.FromArgb(0, 0, 255);

			options.RsaFileColor = new ColorXml(color);

			environmentHelper.Setup(x => x.GetMyDocumentsPath())
				.Returns(path);

			var directoryEntity = new Mock<IDirectoryEntity>();

			directoryEntity.Setup(x => x.GetDirectories()).Returns(new List<FileSystemEntity>());

			directoryEntity.Setup(x => x.GetFiles()).Returns(new List<FileSystemEntity>());

			directoryEntity.Setup(x => x.FullName).Returns(path);

			filesView.SetupProperty(x => x.RsaFileColor, Color.Black);

			filesViewPresenter = CreateFilesViewPresenter();

			filesViewPresenter.RefreshDirectory();

			Assert.AreEqual(color, filesView.Object.RsaFileColor);
		}

		[Test]
		public void SelectedEntityChanged_CheckCipherEnabledIfSelectedEntitiesHasDirectory() {
			filesView.SetupProperty(x => x.CipherEnabled, false);

			var directoryInfo = new Mock<IDirectoryInfo>();
			directoryInfo.Setup(x => x.Exists).Returns(true);

			var directoryEntity = new DirectoryEntity(directoryInfo.Object);
			var fileEntities = new ReadOnlyCollection<FileSystemEntity>(new[] { directoryEntity });
			filesView.Setup(x => x.SelectedEntities).Returns(fileEntities);
			var refreshCryptoViewsCommand = new RefreshCryptoViewsCommand(filesView.Object, new[] { filesView.Object });
			commandsContainer.Setup(x => x.RefreshCryptoViewsCommand).Returns(refreshCryptoViewsCommand);

			CreateFilesViewPresenter();

			filesView.Raise(x => x.SelectedEntityChanged += null, EventArgs.Empty);

			Assert.IsTrue(filesView.Object.CipherEnabled);
		}

		[Test]
		public void SelectedEntityChanged_CheckCipherEnabledIfSelectedEntitiesHasRsaFile() {
			filesView.SetupProperty(x => x.CipherEnabled);

			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.Extension).Returns(".rsa");

			var fileEntity = new FileEntity(fileInfo.Object);
			var fileEntities = new ReadOnlyCollection<FileSystemEntity>(new[] { fileEntity });
			filesView.Setup(x => x.SelectedEntities).Returns(fileEntities);
			var refreshCryptoViewsCommand = new RefreshCryptoViewsCommand(filesView.Object, new[] { filesView.Object });
			commandsContainer.Setup(x => x.RefreshCryptoViewsCommand).Returns(refreshCryptoViewsCommand);

			CreateFilesViewPresenter();

			filesView.Raise(x => x.SelectedEntityChanged += null, EventArgs.Empty);

			Assert.IsFalse(filesView.Object.CipherEnabled);
		}

		[Test]
		public void SelectedEntityChanged_CheckCipherEnabledIfSelectedEntitiesHasTwoRsaFiles() {
			filesView.SetupProperty(x => x.CipherEnabled);

			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.Extension).Returns(".rsa");

			var firstFileEntity = new FileEntity(fileInfo.Object);
			var secondFileEntity = new FileEntity(fileInfo.Object);
			var fileEntities = new ReadOnlyCollection<FileSystemEntity>(new[] { firstFileEntity, secondFileEntity });
			filesView.Setup(x => x.SelectedEntities).Returns(fileEntities);
			var refreshCryptoViewsCommand = new RefreshCryptoViewsCommand(filesView.Object, new[] { filesView.Object });
			commandsContainer.Setup(x => x.RefreshCryptoViewsCommand).Returns(refreshCryptoViewsCommand);

			CreateFilesViewPresenter();

			filesView.Raise(x => x.SelectedEntityChanged += null, EventArgs.Empty);

			Assert.IsTrue(filesView.Object.CipherEnabled);
		}

		private FilesViewPresenter CreateFilesViewPresenter() {
			return new FilesViewPresenter(filesView.Object,
			                              toolBarView.Object,
			                              addressToolBar.Object,
			                              commandsContainer.Object,
			                              environmentHelper.Object,
			                              options,
			                              messageHelper.Object);
		}

		private static void DeleteTestFolder() {
			if (Directory.Exists(testFolder))
				Directory.Delete(testFolder, true);
		}
	}
}