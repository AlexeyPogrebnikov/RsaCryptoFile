using System;
using System.IO;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Management;
using CryptoFile.IO;
using CryptoFile.IO.Entities.Wrappers;
using CryptoFile.IO.Unification;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Management
{
	[TestFixture]
	public class DecipherManager_Test
	{
		private Mock<IEnvironmentHelper> environmentHelper;
		private Mock<IMessageHelper> messageHelper;
		private FileUnifier fileUnifier;
		private const string testFolder = "testFolder";

		[SetUp]
		public void SetUp()
		{
			DeleteTestFolder();
			Directory.CreateDirectory(testFolder);
			environmentHelper = new Mock<IEnvironmentHelper>();
			messageHelper = new Mock<IMessageHelper>();
			fileUnifier = new FileUnifier();
		}

		[TearDown]
		public void TearDown()
		{
			DeleteTestFolder();
		}

		[Test]
		public void Decipher_PrivateKeyIsIncorrect()
		{
			var rsaFileDecipher = new RsaFileDecipher(new FileFactory(4));
			var manager = new DecipherManager(environmentHelper.Object, rsaFileDecipher, messageHelper.Object, fileUnifier);
			var bytes = new byte[]
			{
				4, 7, 0, 54, 0, 0, 0, 0, 0, 0, 0, // rsa header
				6, 53, 47, 52, 49, 87, 159, 197, 184, 234, 6, 229, 57, 107, 51, 226, 235, 36, 89,
				167, 102, 203, 58, 114, 235, 109, 203, 136, 37, 136, 233, 169, 128, 211, 14, 226,
				139, 74, 33, 212, 6, 28, 102, 27, 32, 141, 244, 197, 74, 241, 8, 73, 220, 55, 193,
				12, 0, 0, 240, 100, 185, 0, 0
			};
			// public key: 05#2039A8720B186BDD
			// private key: 0671E7E40596528D#2039A8720B186BDD
			BigNumber d = BigNumber.FromBytes(new[] { 113, 4, 172, 254, 73, 26, 110 });
			BigNumber n = BigNumber.FromBytes(new[] { 173, 144, 32, 44, /*38,*/ 129, 183 });
			var privateKey = new PrivateKey(d, n);
			string inputFileName = Path.Combine(testFolder, "file.rsa");
			File.WriteAllBytes(inputFileName, bytes);
			string outputDirectoryPath = Path.Combine(testFolder, "output");
			string temporaryFileName = Path.Combine(testFolder, "temporary.bin");
			environmentHelper.Setup(x => x.GetTempFileName()).Returns(temporaryFileName);

			manager.Decipher(privateKey, inputFileName, outputDirectoryPath);

			messageHelper.Verify(x => x.Show("Incorrect private key.", "Неправильный закрытый ключ."));
		}

		[Test]
		public void Decipher_ErrorWhenDecipheringFile()
		{
			var rsaFileDecipher = new Mock<IRsaFileDecipher>();
			rsaFileDecipher.Setup(x => x.Decipher(It.IsAny<PrivateKey>(), It.IsAny<string>(), It.IsAny<string>())).Throws(
				new Exception());
			var manager = new DecipherManager(environmentHelper.Object, rsaFileDecipher.Object, messageHelper.Object, fileUnifier);
			BigNumber d = BigNumber.FromBytes(new[] { 113 });
			BigNumber n = BigNumber.FromBytes(new[] { 173 });
			var privateKey = new PrivateKey(d, n);
			string sourceFileName = Path.Combine(testFolder, "sourceFile.rsa");
			File.WriteAllText(sourceFileName, @"hello");
			string outputDirectoryPath = Path.Combine(testFolder, "path");
			manager.Decipher(privateKey, sourceFileName, outputDirectoryPath);

			messageHelper.Verify(x => x.Show("Error decipher RSA file.", "Ошибка при расшифровке RSA файла."));
		}

		[Test]
		public void Decipher_ProcessWasStopped()
		{
			var rsaFileDecipher = new Mock<IRsaFileDecipher>();
			rsaFileDecipher.Setup(x => x.Status).Returns(ProcessStatus.Stopped);
			var manager = new DecipherManager(environmentHelper.Object, rsaFileDecipher.Object, messageHelper.Object, fileUnifier);
			BigNumber d = BigNumber.FromInt(113);
			BigNumber n = BigNumber.FromInt(173);
			var privateKey = new PrivateKey(d, n);

			string outputDirectoryPath = Path.Combine(testFolder, "output");

			manager.Decipher(privateKey, "input.rsa", outputDirectoryPath);

			messageHelper.Verify(x => x.Show("Process was stopped.", "Процесс был остановлен."));
		}

		[Test]
		public void Decipher_VersionIsTooHigh()
		{
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			fileInfo.Setup(x => x.FullName).Returns("hello.rsa");
			fileInfo.Setup(x => x.Extension).Returns(".rsa");

			var rsaFileDecipher = new Mock<IRsaFileDecipher>();
			rsaFileDecipher.Setup(x => x.Decipher(It.IsAny<PrivateKey>(), It.IsAny<string>(), It.IsAny<string>())).Throws(
				new TooHighVersionException("hello"));

			var manager = new DecipherManager(environmentHelper.Object, rsaFileDecipher.Object, messageHelper.Object, fileUnifier);
			BigNumber d = BigNumber.FromInt(113);
			BigNumber n = BigNumber.FromInt(173);
			var privateKey = new PrivateKey(d, n);

			string outputDirectoryPath = Path.Combine(testFolder, "output");

			manager.Decipher(privateKey, "input.rsa", outputDirectoryPath);

			messageHelper.Verify(
				x => x.Show("To decrypt a file needs a new program.", "Для расшифровки файла нужна более новая программа."));
		}

		private static void DeleteTestFolder()
		{
			if (Directory.Exists(testFolder))
				Directory.Delete(testFolder, true);
		}
	}
}