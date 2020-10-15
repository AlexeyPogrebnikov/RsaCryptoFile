using System;
using System.Collections.Generic;
using System.IO;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Entities.Wrappers;
using Moq;
using NUnit.Framework;

namespace CryptoFile.IO.Tests.Entities
{
	[TestFixture]
	public class DirectoryEntity_Test
	{
		private const string testFolder = "testFolder";

		[SetUp]
		public void SetUp()
		{
			Directory.CreateDirectory(testFolder);
		}

		[TearDown]
		public void TearDown()
		{
			if (Directory.Exists(testFolder))
			{
				Directory.Delete(testFolder, true);
			}
		}

		#region Constructor

		[Test]
		public void Constructor_DirectoryDoesntExist()
		{
			var info = new DirectoryInfoWrapper("hello");
			Assert.Throws(typeof(FileEntityNotFoundException), () => new DirectoryEntity(info));
		}

		[Test]
		public void Constructor()
		{
			var info = new Mock<IDirectoryInfo>();
			info.Setup(x => x.Exists).Returns(true);
			info.Setup(x => x.Name).Returns("hello");
			info.Setup(x => x.ModifiedDate).Returns(new DateTime(2008, 10, 5));
			var entity = new DirectoryEntity(info.Object);

			Assert.AreEqual("hello", entity.Name);
			Assert.AreEqual(new DateTime(2008, 10, 5), entity.ModifiedDate);
		}

		#endregion

		#region GetFiles

		[Test]
		public void GetFiles_DirectoryDoesntExist()
		{
			var info = new DirectoryInfoWrapper(testFolder);
			var entity = new DirectoryEntity(info);
			Directory.Delete(testFolder, true);
			Assert.Throws(typeof(FileEntityNotFoundException), () => entity.GetFiles());
		}

		[Test]
		public void GetFiles()
		{
			File.WriteAllText(testFolder + "\\first", null);
			var info = new DirectoryInfoWrapper(testFolder);
			var entity = new DirectoryEntity(info);
			var files = new List<FileSystemEntity>(entity.GetFiles());
			Assert.AreEqual(1, files.Count);
			Assert.AreEqual("first", files[0].Name);
		}

		#endregion

		#region GetDirectories

		[Test]
		public void GetDirectories_DirectoryDoesntExist()
		{
			var info = new DirectoryInfoWrapper(testFolder);
			var entity = new DirectoryEntity(info);
			Directory.Delete(testFolder, true);
			Assert.Throws(typeof(FileEntityNotFoundException), () => entity.GetDirectories());
		}

		[Test]
		public void GetDirectories()
		{
			Directory.CreateDirectory(testFolder + "\\dir");
			var info = new DirectoryInfoWrapper(testFolder);
			var entity = new DirectoryEntity(info);
			var dirs = new List<FileSystemEntity>(entity.GetDirectories());
			Assert.AreEqual(1, dirs.Count);
			Assert.AreEqual("dir", dirs[0].Name);
		}

		#endregion

		#region GetParentDirectory

		[Test]
		public void GetParentDirectory_ParentFolderDoesntExist()
		{
			const string parentFolderName = testFolder + "\\parent";
			Directory.CreateDirectory(parentFolderName);
			const string childFolderName = parentFolderName + "\\child";
			Directory.CreateDirectory(childFolderName);
			var info = new DirectoryInfoWrapper(childFolderName);
			var entity = new DirectoryEntity(info);
			Directory.Delete(parentFolderName, true);
			Assert.Throws(typeof(FileEntityNotFoundException), () => entity.GetParentDirectory());
		}

		[Test]
		public void GetParentDirectoryTest()
		{
			const string dirName = testFolder + "\\hello";
			Directory.CreateDirectory(dirName);
			var info = new DirectoryInfoWrapper(dirName);
			var entity = new DirectoryEntity(info);
			Assert.AreEqual("testFolder", entity.GetParentDirectory().Name);
		}

		#endregion

		[Test]
		public void LengthTest()
		{
			var directoryInfo = new Mock<IDirectoryInfo>();
			directoryInfo.Setup(x => x.Exists).Returns(true);
			directoryInfo.Setup(x => x.Length).Returns(10);

			var entity = new DirectoryEntity(directoryInfo.Object);
			Assert.AreEqual(10, entity.Length);
		}

		[Test]
		public void GetDataTest()
		{
			var directoryInfo = new Mock<IDirectoryInfo>();
			directoryInfo.Setup(x => x.Exists).Returns(true);
			directoryInfo.Setup(x => x.Length).Returns(10);
			var entity = new DirectoryEntity(directoryInfo.Object);
			Assert.Throws(typeof(NotSupportedException), () => entity.GetData());
		}
	}
}