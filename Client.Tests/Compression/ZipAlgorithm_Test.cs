using System;
using System.IO;
using CryptoFile.Client.Compression;
using CryptoFile.Client.Environment;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Compression
{
	[TestFixture]
	public class ZipAlgorithm_Test
	{
		private ZipAlgorithm zipAlgorithm;
		private const string testFolder = "testFolder";
		private Mock<IEnvironmentHelper> environmentHelper;

		[SetUp]
		public void SetUp()
		{
			DeleteTestFolder();
			Directory.CreateDirectory(testFolder);
			environmentHelper = new Mock<IEnvironmentHelper>();
			zipAlgorithm = new ZipAlgorithm(environmentHelper.Object);
		}

		[TearDown]
		public void TearDown()
		{
			DeleteTestFolder();
		}

		[Test]
		public void CompressFile_SourceFileNameIsNull()
		{
			string destinationFileName = Path.Combine(testFolder, "destination.rsa");
			Assert.Throws(typeof(ArgumentNullException), () => zipAlgorithm.CompressFile(null, destinationFileName));
		}

		[Test]
		public void CompressFile_DestinationFileNameIsNull()
		{
			string sourceFileName = Path.Combine(testFolder, "source.txt");
			Assert.Throws(typeof(ArgumentNullException), () => zipAlgorithm.CompressFile(sourceFileName, null));
		}

		[Test]
		public void CompressFile_SourceFileDoesNotExist()
		{
			string sourceFileName = Path.Combine(testFolder, "source.txt");
			string destinationFileName = Path.Combine(testFolder, "destination.rsa");
			Assert.Throws(typeof(CompressionException), () => zipAlgorithm.CompressFile(sourceFileName, destinationFileName));
		}

		[Test]
		public void CompressFileTest()
		{
			string sourceFileName = Path.Combine(testFolder, "source.txt");
			File.WriteAllText(sourceFileName, @"hello");
			string destinationFileName = Path.Combine(testFolder, "destination.rsa");
			zipAlgorithm.CompressFile(sourceFileName, destinationFileName);

			Assert.IsTrue(File.Exists(destinationFileName));
		}

		[Test]
		public void DecompressFile_SourceFileNameIsNull()
		{
			string destinationFileName = Path.Combine(testFolder, "destination.txt");
			Assert.Throws(typeof(ArgumentNullException), () => zipAlgorithm.DecompressFile(null, destinationFileName));
		}

		[Test]
		public void DecompressFile_DestinationFileNameIsNull()
		{
			string sourceFileName = Path.Combine(testFolder, "source.txt");
			Assert.Throws(typeof(ArgumentNullException), () => zipAlgorithm.DecompressFile(sourceFileName, null));
		}

		[Test]
		public void DecompressFile_SourceFileHasErros()
		{
			string temporaryPath = Path.Combine(testFolder, "temporary");
			string sourceFileName = Path.Combine(testFolder, "source.txt");
			File.WriteAllText(sourceFileName, @"hello");
			string temporaryFileName = Path.Combine(testFolder, "temporary.rsa");
			string destinationFileName = Path.Combine(testFolder, "destination.txt");
			environmentHelper.Setup(x => x.GetTempPath()).Returns(temporaryPath);

			File.WriteAllText(sourceFileName, @"hi!");
			Assert.Throws(typeof(CompressionException), () => zipAlgorithm.DecompressFile(temporaryFileName, destinationFileName));
		}

		[Test]
		public void DecompressFileTest()
		{
			string temporaryPath = Path.Combine(testFolder, "���������_�����");
			string sourceFileName = Path.Combine(testFolder, "��������.txt");
			File.WriteAllText(sourceFileName, @"hello");
			string temporaryFileName = Path.Combine(testFolder, "���������.rsa");
			string destinationFileName = Path.Combine(testFolder, "����������.txt");
			environmentHelper.Setup(x => x.GetTempPath()).Returns(temporaryPath);
			environmentHelper.Setup(x => x.CopyFile(It.IsAny<string>(), It.IsAny<string>())).Callback(
				(string s, string d) => File.Copy(s, d));

			zipAlgorithm.CompressFile(sourceFileName, temporaryFileName);
			zipAlgorithm.DecompressFile(temporaryFileName, destinationFileName);

			Assert.IsTrue(File.Exists(destinationFileName));
			Assert.AreEqual("hello", File.ReadAllText(destinationFileName));
			environmentHelper.Verify(x => x.GetTempPath());
			environmentHelper.Verify(x => x.DeleteDirectory(temporaryPath));
		}

		private static void DeleteTestFolder()
		{
			if (Directory.Exists(testFolder))
				Directory.Delete(testFolder, true);
		}
	}
}