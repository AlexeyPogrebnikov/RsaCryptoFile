using System;
using System.IO;
using CryptoFile.Client.Crypto;
using CryptoFile.IO;
using CryptoFile.IO.Exceptions;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Crypto {
	[TestFixture]
	public class RsaFileCipher_Test {
		private RsaFileCipher cipher;
		private const string testFolder = "testFolder";

		[SetUp]
		public void SetUp() {
			DeleteTestFolder();
			Directory.CreateDirectory(testFolder);
		}

		[TearDown]
		public void TearDown() {
			DeleteTestFolder();
		}

		#region Cipher

		[Test]
		public void Cipher_KeyIsNull() {
			cipher = new RsaFileCipher(new FileFactory(56));
			Assert.Throws(typeof(ArgumentNullException), () => cipher.Cipher(null, "hello", "world"));
			Assert.AreEqual(ProcessStatus.Stopped, cipher.Status);
		}

		[Test]
		public void Cipher_SourceFileDoesntExist() {
			var e = BigNumber.FromBytes(new[] { 17 });
			var n = BigNumber.FromBytes(new[] { 19, 241, 93, 252, 193, 101, 145, 14 });
			var key = new PublicKey(e, n);
			cipher = new RsaFileCipher(new FileFactory(56));
			Assert.Throws(typeof(SourceFileNotFoundException), () => cipher.Cipher(key, "hello", "world"));
			Assert.AreEqual(ProcessStatus.Stopped, cipher.Status);
		}

		[Test]
		public void Cipher_FileContainsFiveBytes() {
			var e = BigNumber.FromBytes(new[] { 7 });
			var n = BigNumber.FromBytes(new[] { 217, 118, 184, 189, 114, 147, 142, 61 });
			var key = new PublicKey(e, n);
			var sourceFile = Path.Combine(testFolder, "source.txt");
			File.WriteAllBytes(sourceFile, new byte[] { 104, 101, 108, 108, 111 });
			var destinationFile = Path.Combine(testFolder, "destination.rsa");
			cipher = new RsaFileCipher(new FileFactory(10));
			cipher.Cipher(key, sourceFile, destinationFile);

			var expectedBytes = new byte[] {
			                               	10, // версия
			                               	8, 0, // размер блока
			                               	5, 0, 0, 0, // размер исходного файла
			                               	0, 0, 0, 0, // размер исходного файла
			                               	93, 65, 64, 42, // хэш
			                               	188, 75, 42, 118, // хэш
			                               	185, 113, 157, 145, // хэш
			                               	16, 23, 197, 146, // хэш
			                               	18, 85, 184, 86, 212, 72, 244, 4
			                               };

			TestHelper.CheckFile(destinationFile, expectedBytes);
			Assert.AreEqual(ProcessStatus.Complete, cipher.Status);
		}

		[Test]
		public void Cipher_FileContainsThreeBytes() {
			var e = BigNumber.FromBytes(new[] { 17 });
			var n = BigNumber.FromBytes(new[] { 3, 81, 217, 6, 4, 255, 114, 123, 36 });
			var key = new PublicKey(e, n);
			var sourceFile = Path.Combine(testFolder, "source.txt");
			File.WriteAllBytes(sourceFile, new byte[] { 49, 50, 51 });
			var destinationFile = Path.Combine(testFolder, "destination.rsa");
			cipher = new RsaFileCipher(new FileFactory(5));
			cipher.Cipher(key, sourceFile, destinationFile);

			var expectedBytes = new byte[] {
			                               	5, // версия
			                               	9, 0, // размер блока
			                               	3, 0, 0, 0, // размер исходного файла
			                               	0, 0, 0, 0, // размер исходного файла
			                               	32, 44, 185, 98, // хэш
			                               	172, 89, 7, 91, // хэш
			                               	150, 75, 7, 21, // хэш
			                               	45, 35, 75, 112, // хэш
			                               	51, 136, 212, 124, 2, 205, 81, 39, 34
			                               };

			TestHelper.CheckFile(destinationFile, expectedBytes);
			Assert.AreEqual(ProcessStatus.Complete, cipher.Status);
		}

		[Test]
		public void Cipher_CheckStatusIfErrorOccured() {
			var e = BigNumber.FromBytes(new[] { 7 });
			var n = BigNumber.FromBytes(new[] { 217, 118, 184, 189, 114, 147, 142, 61 });
			var key = new PublicKey(e, n);
			var sourceFile = Path.Combine(testFolder, "source.txt");
			File.WriteAllBytes(sourceFile, new byte[] { 104, 101, 108, 108, 111 });
			var destinationFile = Path.Combine(testFolder, "destination.rsa");
			var fileFactory = new Mock<IFileFactory>();
			fileFactory.Setup(x => x.CreateFileReader(It.IsAny<string>(), It.IsAny<short>())).Throws(new Exception());
			cipher = new RsaFileCipher(fileFactory.Object);
			Assert.Throws(typeof(Exception), () => cipher.Cipher(key, sourceFile, destinationFile));
			Assert.AreEqual(ProcessStatus.Stopped, cipher.Status);
		}

		#endregion

		private static void DeleteTestFolder() {
			if (Directory.Exists(testFolder)) {
				Directory.Delete(testFolder, true);
			}
		}
	}
}