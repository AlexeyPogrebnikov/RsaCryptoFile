using System;
using System.IO;
using CryptoFile.Client.Compression;
using CryptoFile.Client.Environment;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Compression {
	[TestFixture]
	public class ZipAlgorithm_Test {
		private ZipAlgorithm zipAlgorithm;
		private const string testFolder = "testFolder";
		private Mock<IEnvironmentHelper> environmentHelper;

		[SetUp]
		public void SetUp() {
			DeleteTestFolder();
			Directory.CreateDirectory(testFolder);
			environmentHelper = new Mock<IEnvironmentHelper>();
			zipAlgorithm = new ZipAlgorithm(environmentHelper.Object);
		}

		[TearDown]
		public void TearDown() {
			DeleteTestFolder();
		}

		[Test]
		public void CompressFile_SourceFileNameIsNull() {
			var destinationFileName = Path.Combine(testFolder, "destination.rsa");
			Assert.Throws(typeof(ArgumentNullException), () => zipAlgorithm.CompressFile(null, destinationFileName));
		}

		[Test]
		public void CompressFile_DestinationFileNameIsNull() {
			var sourceFileName = Path.Combine(testFolder, "source.txt");
			Assert.Throws(typeof(ArgumentNullException), () => zipAlgorithm.CompressFile(sourceFileName, null));
		}

		[Test]
		public void CompressFile_SourceFileDoesNotExist() {
			var sourceFileName = Path.Combine(testFolder, "source.txt");
			var destinationFileName = Path.Combine(testFolder, "destination.rsa");
			Assert.Throws(typeof(CompressionException), () => zipAlgorithm.CompressFile(sourceFileName, destinationFileName));
		}

		[Test]
		public void CompressFileTest() {
			var sourceFileName = Path.Combine(testFolder, "source.txt");
			File.WriteAllText(sourceFileName, @"hello");
			var destinationFileName = Path.Combine(testFolder, "destination.rsa");
			zipAlgorithm.CompressFile(sourceFileName, destinationFileName);

			Assert.IsTrue(File.Exists(destinationFileName));
		}

		[Test]
		public void DecompressFile_SourceFileNameIsNull() {
			var destinationFileName = Path.Combine(testFolder, "destination.txt");
			Assert.Throws(typeof(ArgumentNullException), () => zipAlgorithm.DecompressFile(null, destinationFileName));
		}

		[Test]
		public void DecompressFile_DestinationFileNameIsNull() {
			var sourceFileName = Path.Combine(testFolder, "source.txt");
			Assert.Throws(typeof(ArgumentNullException), () => zipAlgorithm.DecompressFile(sourceFileName, null));
		}

		[Test]
		public void DecompressFile_SourceFileHasErros() {
			var temporaryPath = Path.Combine(testFolder, "temporary");
			var sourceFileName = Path.Combine(testFolder, "source.txt");
			File.WriteAllText(sourceFileName, @"hello");
			var temporaryFileName = Path.Combine(testFolder, "temporary.rsa");
			var destinationFileName = Path.Combine(testFolder, "destination.txt");
			environmentHelper.Setup(x => x.GetTempPath()).Returns(temporaryPath);

			File.WriteAllText(sourceFileName, @"hi!");
			Assert.Throws(typeof(CompressionException), () => zipAlgorithm.DecompressFile(temporaryFileName, destinationFileName));
		}

		[Test]
		public void DecompressFileTest() {
			var temporaryPath = Path.Combine(testFolder, "временная_папка");
			var sourceFileName = Path.Combine(testFolder, "источник.txt");
			File.WriteAllText(sourceFileName, @"hello");
			var temporaryFileName = Path.Combine(testFolder, "временный.rsa");
			var destinationFileName = Path.Combine(testFolder, "назначение.txt");
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

		private static void DeleteTestFolder() {
			if (Directory.Exists(testFolder))
				Directory.Delete(testFolder, true);
		}
	}
}