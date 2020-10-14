using System;
using System.IO;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Entities.Wrappers;
using Moq;
using NUnit.Framework;

namespace CryptoFile.IO.Tests.Entities {
	[TestFixture]
	public class FileEntity_Test {
		private const string testFolder = "testFolder";

		[SetUp]
		public void SetUp() {
			Directory.CreateDirectory(testFolder);
		}

		[TearDown]
		public void TearDown() {
			Directory.Delete(testFolder, true);
		}

		#region Constructor

		[Test]
		public void Constructor_InfoIsNull() {
			Assert.Throws(typeof(ArgumentNullException), () => new FileEntity(null));
		}

		[Test]
		public void Constructor_FileDoesntExist() {
			var fileInfo = new FileInfo("hello");
			var directoryInfo = new Mock<IDirectoryInfo>();
			var info = new FileInfoWrapper(fileInfo, directoryInfo.Object);
			Assert.Throws(typeof(FileEntityNotFoundException), () => new FileEntity(info));
		}

		[Test]
		public void Constructor_FileIsEmpty() {
			const string fileName = testFolder + "\\file.emp";
			File.WriteAllText(fileName, null);
			File.SetLastWriteTime(fileName, new DateTime(2009, 2, 20));
			var fileInfoWrapper = CreateFileInfoWrapper(fileName);
			var entity = new FileEntity(fileInfoWrapper);
			Assert.IsFalse(entity.IsCryptoFile);
			Assert.AreEqual(0, entity.Length);
			Assert.AreEqual("file.emp", entity.Name);
			Assert.AreEqual(new DateTime(2009, 2, 20), entity.ModifiedDate);
		}

		[Test]
		public void Constructor_FileIsNotEmpty() {
			const string fileName = testFolder + "\\file.bmp";
			File.WriteAllBytes(fileName, new byte[] { 1, 2, 3 });
			var fileInfoWrapper = CreateFileInfoWrapper(fileName);
			var entity = new FileEntity(fileInfoWrapper);
			Assert.IsFalse(entity.IsCryptoFile);
			Assert.AreEqual(3, entity.Length);
			Assert.AreEqual("file.bmp", entity.Name);
		}

		[Test]
		public void Constructor_FileIsRsa() {
			const string fileName = testFolder + "\\file.rsa";
			File.WriteAllBytes(fileName, new byte[] { 1, 2, 3 });
			var fileInfoWrapper = CreateFileInfoWrapper(fileName);
			var entity = new FileEntity(fileInfoWrapper);
			Assert.IsTrue(entity.IsCryptoFile);
			Assert.AreEqual(3, entity.Length);
			Assert.AreEqual("file.rsa", entity.Name);
		}

		[Test]
		public void Constructor_CheckIsCryptoFile() {
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.Extension).Returns(".RsA");

			var entity = new FileEntity(fileInfo.Object);
			Assert.IsTrue(entity.IsCryptoFile);
		}

		#endregion

		[Test]
		public void GetDataTest() {
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.GetData()).Returns(new byte[] { 4, 56, 168 });
			var entity = new FileEntity(fileInfo.Object);
			var data = entity.GetData();

			TestHelper.CheckArray(new byte[] { 4, 56, 168 }, data);
		}

		[Test]
		public void ExtensionTest() {
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.Extension).Returns(".txt");
			var fileEntity = new FileEntity(fileInfo.Object);

			Assert.AreEqual(".txt", fileEntity.Extension);
		}

		[Test]
		public void GetFilesTest() {
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.Extension).Returns(".txt");
			var fileEntity = new FileEntity(fileInfo.Object);

			Assert.Throws(typeof(NotSupportedException), () => fileEntity.GetFiles());
		}

		[Test]
		public void GetDirectoriesTest() {
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.Extension).Returns(".txt");
			var fileEntity = new FileEntity(fileInfo.Object);

			Assert.Throws(typeof(NotSupportedException), () => fileEntity.GetDirectories());
		}

		private static FileInfoWrapper CreateFileInfoWrapper(string fileName) {
			var fileInfo = new FileInfo(fileName);
			var directoryInfo = new Mock<IDirectoryInfo>();
			return new FileInfoWrapper(fileInfo, directoryInfo.Object);
		}
	}
}