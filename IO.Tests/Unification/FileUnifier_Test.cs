using System;
using System.IO;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Entities.Wrappers;
using CryptoFile.IO.Unification;
using Moq;
using NUnit.Framework;

namespace CryptoFile.IO.Tests.Unification {
	[TestFixture]
	public class FileUnifier_Test {
		private FileUnifier unifier;
		private const string testFolder = "testFolder";

		[SetUp]
		public void SetUp() {
			DeleteTestFolder();
			Directory.CreateDirectory(testFolder);
			unifier = new FileUnifier();
		}

		[TearDown]
		public void TearDown() {
			DeleteTestFolder();
		}

		[Test]
		public void Union_FileSystemEntitiesIsEmpty() {
			var destinationFileName = Path.Combine(testFolder, "destination.bin");

			Assert.Throws(typeof(ArgumentException), () => unifier.Union(new FileSystemEntity[0], destinationFileName));
		}

		[Test]
		public void Union_FileEntityDoesNotExist() {
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			var fileName = Path.Combine(testFolder, "file.exe");
			fileInfo.Setup(x => x.FullName).Returns(fileName);

			var fileEntity = new FileEntity(fileInfo.Object);
			fileInfo.Setup(x => x.Exists).Returns(false);
			var fileSystemEntities = new[] { fileEntity };

			var destinationFileName = Path.Combine(testFolder, "destination.zip");

			Assert.Throws(typeof(FileNotFoundException), () => unifier.Union(fileSystemEntities, destinationFileName));
		}

		[Test]
		public void Union_DirectoryEntityDoesNotExist() {
			var directoryInfo = new Mock<IDirectoryInfo>();
			directoryInfo.Setup(x => x.Exists).Returns(true);
			var path = Path.Combine(testFolder, "directory");
			directoryInfo.Setup(x => x.FullName).Returns(path);

			var directoryEntity = new DirectoryEntity(directoryInfo.Object);
			directoryInfo.Setup(x => x.Exists).Returns(false);
			var fileSystemEntities = new[] { directoryEntity };

			var destinationFileName = Path.Combine(testFolder, "destination.zip");

			Assert.Throws(typeof(DirectoryNotFoundException), () => unifier.Union(fileSystemEntities, destinationFileName));
		}

		[Test]
		public void Union_Split_FileSystemEntitiesHaveOneFile() {
			var fileName = Path.Combine(testFolder, "test.bin");
			var bytes = new byte[] { 190, 34, 56 };
			File.WriteAllBytes(fileName, bytes);
			var fileInfo = new FileInfo(fileName);
			var directoryInfo = new Mock<IDirectoryInfo>();
			var fileInfoWrapper = new FileInfoWrapper(fileInfo, directoryInfo.Object);
			var fileEntity = new FileEntity(fileInfoWrapper);
			var destinationFileName = Path.Combine(testFolder, "union.bin");

			unifier.Union(new[] { fileEntity }, destinationFileName);

			Assert.IsTrue(File.Exists(destinationFileName));

			var destinationPath = Path.Combine(testFolder, "destinationPath");

			unifier.Split(destinationFileName, destinationPath);

			Assert.IsTrue(Directory.Exists(destinationPath));

			var innerFileName = Path.Combine(destinationPath, "test.bin");

			Assert.IsTrue(File.Exists(innerFileName));

			TestHelper.CheckFile(innerFileName, bytes);
		}

		[Test]
		public void Union_Split_FileSystemEntitiesHaveOneFileAndOneDirectory() {
			const string fileName = "hello - файл";
			var fileFullName = Path.Combine(testFolder, fileName);
			var bytes = new byte[] { 89, 50 };
			File.WriteAllBytes(fileFullName, bytes);
			const string directoryName = "dir - папка";
			var directoryFullPath = Path.Combine(testFolder, directoryName);
			Directory.CreateDirectory(directoryFullPath);
			var directoryInfo = new Mock<IDirectoryInfo>();
			var fileInfo = new FileInfo(fileFullName);
			var fileInfoWrapper = new FileInfoWrapper(fileInfo, directoryInfo.Object);
			var fileEntity = new FileEntity(fileInfoWrapper);
			var directoryInfoWrapper = new DirectoryInfoWrapper(directoryFullPath);
			var directoryEntity = new DirectoryEntity(directoryInfoWrapper);
			var destinationFileName = Path.Combine(testFolder, "union.bin");

			unifier.Union(new FileSystemEntity[] { fileEntity, directoryEntity }, destinationFileName);

			Assert.IsTrue(File.Exists(destinationFileName));

			var destinationPath = Path.Combine(testFolder, "destinationPath");

			unifier.Split(destinationFileName, destinationPath);

			Assert.IsTrue(Directory.Exists(destinationPath));

			var innerFileName = Path.Combine(destinationPath, fileName);

			Assert.IsTrue(File.Exists(innerFileName));

			TestHelper.CheckFile(innerFileName, bytes);

			var innerDirectoryName = Path.Combine(destinationPath, directoryName);

			Assert.IsTrue(Directory.Exists(innerDirectoryName));
		}

		[Test]
		public void Union_Split_FileSystemEntitiesHaveOneFileAndOneDirectoryIntoDirectory() {
			const string middleDirectoryName = "middle - промежуточная";
			var middleDirectoryFullName = Path.Combine(testFolder, middleDirectoryName);
			Directory.CreateDirectory(middleDirectoryFullName);
			const string fileName = "файл.txt";
			var fileFullName = Path.Combine(middleDirectoryFullName, fileName);
			var bytes = new byte[] { 89, 50 };
			File.WriteAllBytes(fileFullName, bytes);
			const string directoryName = "папка";
			var directoryFullPath = Path.Combine(middleDirectoryFullName, directoryName);
			Directory.CreateDirectory(directoryFullPath);
			var directoryInfoWrapper = new DirectoryInfoWrapper(middleDirectoryFullName);
			var directoryEntity = new DirectoryEntity(directoryInfoWrapper);
			var destinationFileFullName = Path.Combine(testFolder, "объединенный.bin");

			unifier.Union(new FileSystemEntity[] { directoryEntity }, destinationFileFullName);

			Assert.IsTrue(File.Exists(destinationFileFullName));

			var destinationPath = Path.Combine(testFolder, "destinationPath - папка назначения");

			unifier.Split(destinationFileFullName, destinationPath);

			Assert.IsTrue(Directory.Exists(destinationPath));

			var innerFileName = Path.Combine(Path.Combine(destinationPath, middleDirectoryName), fileName);

			Assert.IsTrue(File.Exists(innerFileName));

			TestHelper.CheckFile(innerFileName, bytes);

			var innerDirectoryName = Path.Combine(Path.Combine(destinationPath, middleDirectoryName), directoryName);

			Assert.IsTrue(Directory.Exists(innerDirectoryName));
		}

		[Test]
		public void Split_SourceFileDoesNotExist() {
			var destinationPath = string.Format("{0}\\qwerty", testFolder);
			Assert.Throws(typeof(FileNotFoundException), () => unifier.Split("hello", destinationPath));
			Assert.IsFalse(Directory.Exists(destinationPath));
		}

		[Test]
		public void Split_DestinationDirectoryAlreadyExistsCheckRewrite() {
			const string fileName = "файл.bin";
			var fileFullName = Path.Combine(testFolder, fileName);
			var bytes = new byte[] { 190, 34, 56 };
			File.WriteAllBytes(fileFullName, bytes);
			var fileInfo = new FileInfo(fileFullName);
			var directoryInfo = new Mock<IDirectoryInfo>();
			var fileInfoWrapper = new FileInfoWrapper(fileInfo, directoryInfo.Object);
			var fileEntity = new FileEntity(fileInfoWrapper);
			var destinationFileName = Path.Combine(testFolder, "union.bin");

			unifier.Union(new[] { fileEntity }, destinationFileName);

			Assert.IsTrue(File.Exists(destinationFileName));

			var destinationPath = Path.Combine(testFolder, "destinationPath");
			Directory.CreateDirectory(destinationPath);
			var innerFileName = Path.Combine(destinationPath, fileName);
			var secondInnerFileName = Path.Combine(destinationPath, "hi.exe");
			File.WriteAllBytes(innerFileName, new byte[] { 100, 200, 250, 50 });
			File.WriteAllText(secondInnerFileName, "hello");

			unifier.Split(destinationFileName, destinationPath);

			Assert.IsTrue(Directory.Exists(destinationPath));

			Assert.IsTrue(File.Exists(innerFileName));

			TestHelper.CheckFile(innerFileName, bytes);

			Assert.IsTrue(File.Exists(secondInnerFileName));
		}

		private static void DeleteTestFolder() {
			if (Directory.Exists(testFolder)) {
				Directory.Delete(testFolder, true);
			}
		}
	}
}