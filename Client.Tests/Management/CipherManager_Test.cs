using System;
using System.Collections.Generic;
using System.IO;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Management;
using CryptoFile.IO;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Entities.Wrappers;
using CryptoFile.IO.Unification;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Management {
	[TestFixture]
	public class CipherManager_Test {
		private CipherManager cipherManager;
		private Mock<IEnvironmentHelper> environmentHelper;
		private const string testFolder = "testFolder";
		private Mock<IMessageHelper> messageHelper;

		[SetUp]
		public void SetUp() {
			DeleteTestFolder();
			Directory.CreateDirectory(testFolder);

			var rsaFileCipher = new Mock<IRsaFileCipher>();
			var fileUnifier = new Mock<IFileUnifier>();
			environmentHelper = new Mock<IEnvironmentHelper>();
			messageHelper = new Mock<IMessageHelper>();
			cipherManager = new CipherManager(rsaFileCipher.Object,
			                                  fileUnifier.Object,
			                                  environmentHelper.Object,
			                                  messageHelper.Object);
		}

		[TearDown]
		public void TearDown() {
			DeleteTestFolder();
		}

		[Test]
		public void Cipher_InputFileEntitiesIsEmpty() {
			var publicKey = CreatePublicKey(3, 20);
			Assert.Throws(typeof(ArgumentException), () => cipherManager.Cipher(publicKey, new FileSystemEntity[0], "hello"));
		}

		[Test]
		public void Cipher_CheckUseTemporaryFile() {
			var publicKey = CreatePublicKey(3, 7);
			var fileSystemEntities = new List<FileSystemEntity>();
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Exists).Returns(true);
			var fileEntity = new FileEntity(fileInfo.Object);
			fileSystemEntities.Add(fileEntity);
			var outputFileName = Path.Combine(testFolder, "outputFileName.rsa");
			var temporaryFileName = Path.Combine(testFolder, "temporary.bin");
			environmentHelper.Setup(x => x.GetTempFileName()).Returns(temporaryFileName);

			cipherManager.Cipher(publicKey, fileSystemEntities, outputFileName);
			environmentHelper.Verify(x => x.GetTempFileName());
			environmentHelper.Verify(x => x.DeleteFile(temporaryFileName));
		}

		[Test]
		public void Cipher_Decipher() {
			var outputFileName = Path.Combine(testFolder, "outputFileName.rsa");
			var temporaryFileName = Path.Combine(testFolder, "temporary.bin");
			environmentHelper.Setup(x => x.GetTempFileName()).Returns(temporaryFileName);
			var fileFactory = new FileFactory(4);
			var rsaFileCipher = new RsaFileCipher(fileFactory);
			var fileUnifier = new FileUnifier();
			var manager = new CipherManager(rsaFileCipher, fileUnifier, environmentHelper.Object, messageHelper.Object);
			// public key: 05#B781262C2090AD
			// private key: 6E1A49FEAC0471#B781262C2090AD
			var e = BigNumber.FromInt(5);
			var d = BigNumber.FromBytes(new[] { 113, 4, 172, 254, 73, 26, 110 });
			var n = BigNumber.FromBytes(new[] { 173, 144, 32, 44, 38, 129, 183 });
			var publicKey = new PublicKey(e, n);
			var privateKey = new PrivateKey(d, n);
			var sourceFileName = Path.Combine(testFolder, "file.bin");
			File.WriteAllBytes(sourceFileName, new byte[] { 45, 129, 240 });
			var directoryInfoWrapper = new DirectoryInfoWrapper(testFolder);
			var fileInfoWrapper = new FileInfoWrapper(new FileInfo(sourceFileName), directoryInfoWrapper);
			var fileEntity = new FileEntity(fileInfoWrapper);
			manager.Cipher(publicKey, new FileSystemEntity[] { fileEntity }, outputFileName);

			var decipherManager = new DecipherManager(environmentHelper.Object, new RsaFileDecipher(fileFactory),
			                                          messageHelper.Object, fileUnifier);
			var outputPath = Path.Combine(testFolder, "outputPath");
			decipherManager.Decipher(privateKey, outputFileName, outputPath);

			Assert.IsTrue(Directory.Exists(outputPath));
			var fileName = Path.Combine(outputPath, "file.bin");
			Assert.IsTrue(File.Exists(fileName));
			TestHelper.CheckFile(fileName, new byte[] { 45, 129, 240 });
		}

		private static PublicKey CreatePublicKey(int e, int n) {
			return new PublicKey(BigNumber.FromInt(e), BigNumber.FromInt(n));
		}

		private static void DeleteTestFolder() {
			if (Directory.Exists(testFolder))
				Directory.Delete(testFolder, true);
		}
	}
}