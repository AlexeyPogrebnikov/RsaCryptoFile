using System;
using System.IO;
using CryptoFile.Client.Compression;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Environment;
using CryptoFile.IO;
using CryptoFile.IO.Exceptions;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Crypto
{
	[TestFixture]
	public class RsaFactory_Test
	{
		private RsaFactory rsaFactory;
		private Mock<IFileFactory> fileFactory;
		private Options options;
		private Mock<IEnvironmentHelper> environmentHelper;
		private const string testFolder = "testFolder";
		private Mock<IZipAlgorithm> zipAlgorithm;

		[SetUp]
		public void SetUp()
		{
			DeleteTestFolder();
			Directory.CreateDirectory(testFolder);
			fileFactory = new Mock<IFileFactory>();
			options = new Options();
			environmentHelper = new Mock<IEnvironmentHelper>();
			zipAlgorithm = new Mock<IZipAlgorithm>();
			rsaFactory = new RsaFactory(fileFactory.Object, options, environmentHelper.Object, zipAlgorithm.Object);
		}

		[TearDown]
		public void TearDown()
		{
			DeleteTestFolder();
		}

		[Test]
		public void CreateRsaFileDecipher_FileNameIsNull()
		{
			Assert.Throws(typeof(ArgumentNullException), () => rsaFactory.CreateRsaFileDecipher(null));
		}

		[Test]
		public void CreateRsaFileDecipher_FileNameIsEmpty()
		{
			Assert.Throws(typeof(ArgumentException), () => rsaFactory.CreateRsaFileDecipher(string.Empty));
		}

		[Test]
		public void CreateRsaFileDecipher_FileDoesNotExist()
		{
			Assert.Throws(typeof(SourceFileNotFoundException), () => rsaFactory.CreateRsaFileDecipher("hello"));
		}

		[Test]
		public void CreateRsaFileDecipher_FirstByteIs80()
		{
			string fileName = Path.Combine(testFolder, "file.bin");
			File.WriteAllBytes(fileName, new byte[] { 80 });
			IRsaFileDecipher rsaFileDecipher = rsaFactory.CreateRsaFileDecipher(fileName);
			Assert.IsTrue(rsaFileDecipher is RsaFileDecipherZipDecorator);
		}

		[Test]
		public void CreateRsaFileDecipher_FirstByteIsNot80()
		{
			string fileName = Path.Combine(testFolder, "file.bin");
			File.WriteAllBytes(fileName, new byte[] { 0 });
			IRsaFileDecipher rsaFileDecipher = rsaFactory.CreateRsaFileDecipher(fileName);
			Assert.IsTrue(rsaFileDecipher is RsaFileDecipher);
		}

		[Test]
		public void CreateRsaFileDecipher_FileIsEmpty()
		{
			string fileName = Path.Combine(testFolder, "file.bin");
			File.WriteAllBytes(fileName, new byte[0]);
			Assert.Throws(typeof(SourceFileException), () => rsaFactory.CreateRsaFileDecipher(fileName));
		}

		private static void DeleteTestFolder()
		{
			if (Directory.Exists(testFolder))
				Directory.Delete(testFolder, true);
		}
	}
}