using System;
using System.IO;
using CryptoFile.Client.Environment;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Environment
{
	[TestFixture]
	public class EnvironmentHelper_Test
	{
		private EnvironmentHelper environmentHelper;
		private const string testFolder = "testFolder";

		[SetUp]
		public void SetUp()
		{
			DeleteTestFolder();
			Directory.CreateDirectory(testFolder);
			environmentHelper = new EnvironmentHelper();
		}

		[TearDown]
		public void TearDown()
		{
			DeleteTestFolder();
		}

		[Test]
		public void DeleteFile_FileNameIsNull()
		{
			Assert.Throws(typeof(ArgumentNullException), () => environmentHelper.DeleteFile(null));
		}

		[Test]
		public void DeleteFile_FileNameIsEmpty()
		{
			Assert.Throws(typeof(ArgumentException), () => environmentHelper.DeleteFile(string.Empty));
		}

		[Test]
		public void DeleteFileTest()
		{
			string fileName = Path.Combine(testFolder, "file.bin");
			File.WriteAllText(fileName, @"hello");

			environmentHelper.DeleteFile(fileName);

			Assert.IsFalse(File.Exists(fileName));
		}

		[Test]
		public void DeleteDirectory_DirectoryIsEmpty()
		{
			string path = Path.Combine(testFolder, "path");
			Directory.CreateDirectory(path);

			environmentHelper.DeleteDirectory(path);

			Assert.IsFalse(Directory.Exists(path));
		}

		[Test]
		public void DeleteDirectory_DirectoryContansFile()
		{
			string path = Path.Combine(testFolder, "path");
			Directory.CreateDirectory(path);

			string fileName = Path.Combine(path, "file.txt");
			File.WriteAllText(fileName, @"hello");

			environmentHelper.DeleteDirectory(path);

			Assert.IsFalse(Directory.Exists(path));
		}

		[Test]
		public void CopyFileTest()
		{
			string sourceFileName = Path.Combine(testFolder, "source.txt");
			File.WriteAllText(sourceFileName, @"java");
			string destinationFileName = Path.Combine(testFolder, "destination.txt");

			environmentHelper.CopyFile(sourceFileName, destinationFileName);

			Assert.IsTrue(File.Exists(destinationFileName));
			Assert.AreEqual("java", File.ReadAllText(destinationFileName));
		}

		[Test]
		public void CopyFile_IfDestinationFileAlreadyExists()
		{
			string sourceFileName = Path.Combine(testFolder, "source.txt");
			File.WriteAllText(sourceFileName, @"java");
			string destinationFileName = Path.Combine(testFolder, "destination.txt");
			File.WriteAllText(destinationFileName, @"c#");

			environmentHelper.CopyFile(sourceFileName, destinationFileName);

			Assert.IsTrue(File.Exists(destinationFileName));
			Assert.AreEqual("java", File.ReadAllText(destinationFileName));
		}

		[Test]
		public void FileExists_IfFileExists()
		{
			string fileName = Path.Combine(testFolder, "file.txt");
			File.WriteAllText(fileName, @"C++");

			Assert.IsTrue(environmentHelper.FileExists(fileName));
		}

		[Test]
		public void FileExists_IfFileDoesNotExist()
		{
			Assert.IsFalse(environmentHelper.FileExists("hello"));
		}

		[Test]
		public void DirectoryExists_IfDirectoryExists()
		{
			string path = Path.Combine(testFolder, "dir");
			Directory.CreateDirectory(path);

			Assert.IsTrue(environmentHelper.DirectoryExists(path));
		}

		[Test]
		public void DirectoryExists_IfDirectoryDoesNotExist()
		{
			string path = Path.Combine(testFolder, "dir");
			Assert.IsFalse(environmentHelper.DirectoryExists(path));
		}

		private static void DeleteTestFolder()
		{
			if (Directory.Exists(testFolder))
				Directory.Delete(testFolder, true);
		}
	}
}