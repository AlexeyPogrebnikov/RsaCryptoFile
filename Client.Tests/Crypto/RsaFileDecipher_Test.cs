using System;
using System.IO;
using System.Threading;
using CryptoFile.Client.Crypto;
using CryptoFile.IO;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Crypto {
	[TestFixture]
	public class RsaFileDecipher_Test {
		private RsaFileDecipher decipher;
		private const string testFolder = "testFolder";
		private FileFactory fileFactory;

		[SetUp]
		public void SetUp() {
			fileFactory = new FileFactory(1);
			decipher = new RsaFileDecipher(fileFactory);
			DeleteTestFolder();
			Directory.CreateDirectory(testFolder);
		}

		[TearDown]
		public void TearDown() {
			Thread.Sleep(100);
			DeleteTestFolder();
		}

		#region Decipher

		[Test]
		public void Decipher_KeyIsNull() {
			var fileName = Path.Combine(testFolder, "file.rsa");
			File.WriteAllText(fileName, @"hello");
			Assert.Throws(typeof(ArgumentNullException), () => decipher.Decipher(null, fileName, "hello"));
			Assert.AreEqual(ProcessStatus.NotBeginning, decipher.Status);
		}

		[Test]
		public void Decipher_FileIsNull() {
			var d = BigNumber.FromBytes(new[] { 193, 223, 147, 10, 224, 136, 218, 12 });
			var n = BigNumber.FromBytes(new[] { 19, 241, 93, 252, 193, 101, 145, 14 });
			var key = new PrivateKey(d, n);
			Assert.Throws(typeof(ArgumentNullException), () => decipher.Decipher(key, null, "hello"));
			Assert.AreEqual(ProcessStatus.NotBeginning, decipher.Status);
		}

		[Test]
		public void Decipher() {
			// public key: 0101#0F433A164419165B
			// private key: 02CA8D18E051C519#0F433A164419165B
			var sourceFileName = Path.Combine(testFolder, "source.rsa");
			var bytes = new byte[] {
			                       	0, // version
			                       	8, 0, // length of block
			                       	3, 0, 0, 0, // length of source file
			                       	0, 0, 0, 0, // length of source file
			                       	247, 190, 28, 15,
			                       	142, 126, 148, 172,
			                       	109, 142, 128, 60,
			                       	33, 158, 83, 243,
			                       	86, 227, 92, 23, 120, 76, 2, 11
			                       };
			File.WriteAllBytes(sourceFileName, bytes);
			var destinationFileName = Path.Combine(testFolder, "destination.bin");
			var d = BigNumber.FromBytes(new[] { 25, 197, 81, 224, 24, 141, 202, 2 });
			var n = BigNumber.FromBytes(new[] { 91, 22, 25, 68, 22, 58, 67, 15 });
			var privateKey = new PrivateKey(d, n);
			decipher.Decipher(privateKey, sourceFileName, destinationFileName);
			var expectedBytes = new byte[] { 23, 67, 12 };
			TestHelper.CheckFile(destinationFileName, expectedBytes);
			Assert.AreEqual(ProcessStatus.Complete, decipher.Status);
		}

		[Test]
		public void Decipher_VersionIsTooHigh() {
			// public key: 0101#0F433A164419165B
			// private key: 02CA8D18E051C519#0F433A164419165B
			var sourceFileName = Path.Combine(testFolder, "source.rsa");
			var bytes = new byte[] {
			                       	2, // version
			                       	8, 0, // length of block
			                       	3, 0, 0, 0, // length of source file
			                       	0, 0, 0, 0, // length of source file
			                       	247, 190, 28, 15,
			                       	142, 126, 148, 172,
			                       	109, 142, 128, 60,
			                       	33, 158, 83, 243,
			                       	86, 227, 92, 23, 120, 76, 2, 11
			                       };
			File.WriteAllBytes(sourceFileName, bytes);
			var destinationFileName = Path.Combine(testFolder, "destination.bin");
			var d = BigNumber.FromBytes(new[] { 25, 197, 81, 224, 24, 141, 202, 2 });
			var n = BigNumber.FromBytes(new[] { 91, 22, 25, 68, 22, 58, 67, 15 });
			var privateKey = new PrivateKey(d, n);
			Assert.Throws(typeof(TooHighVersionException),
			              () => decipher.Decipher(privateKey, sourceFileName, destinationFileName));
			Assert.AreEqual(ProcessStatus.Stopped, decipher.Status);
		}

		[Test]
		public void Decipher_PrivateKeyIsIncorrect() {
			var bytes = new byte[] {
			                       	1, 7, 0, 54, 0, 0, 0, 0, 0, 0, 0, // rsa header
			                       	6, 53, 47, 52, 49, 87, 159, 197, 184, 234, 6, 229, 57, 107, 51, 226, 235, 36, 89,
			                       	167, 102, 203, 58, 114, 235, 109, 203, 136, 37, 136, 233, 169, 128, 211, 14, 226,
			                       	139, 74, 33, 212, 6, 28, 102, 27, 32, 141, 244, 197, 74, 241, 8, 73, 220, 55, 193,
			                       	12, 0, 0, 240, 100, 185, 0, 0
			                       };
			// public key: 05#2039A8720B186BDD
			// private key: 0671E7E40596528D#2039A8720B186BDD
			var d = BigNumber.FromBytes(new[] { 113, 4, 172, 254, 73, 26, 110 });
			var n = BigNumber.FromBytes(new[] { 173, 144, 32, 44, /*38,*/ 129, 183 });
			var privateKey = new PrivateKey(d, n);
			var inputFileName = Path.Combine(testFolder, "file.rsa");
			File.WriteAllBytes(inputFileName, bytes);
			var outputDirectoryPath = Path.Combine(testFolder, "output");
			Assert.Throws(typeof(IncorrectPrivateKeyException),
			              () => decipher.Decipher(privateKey, inputFileName, outputDirectoryPath));
			Assert.AreEqual(ProcessStatus.Stopped, decipher.Status);
		}

		[Test]
		public void Decipher_ProcessWasStopped() {
			// public key: 0101#0F433A164419165B
			// private key: 02CA8D18E051C519#0F433A164419165B
			var sourceFileName = Path.Combine(testFolder, "source.rsa");
			var destinationFileName = Path.Combine(testFolder, "destination.bin");
			var e = BigNumber.FromBytes(new[] { 1, 1 });
			var d = BigNumber.FromBytes(new[] { 25, 197, 81, 224, 24, 141, 202, 2 });
			var n = BigNumber.FromBytes(new[] { 91, 22, 25, 68, 22, 58, 67, 15 });
			var privateKey = new PrivateKey(d, n);
			var publicKey = new PublicKey(e, n);
			var cipher = new RsaFileCipher(fileFactory);
			var initialFileName = Path.Combine(testFolder, "initial.exe");
			File.WriteAllBytes(initialFileName, new byte[1000]);
			cipher.Cipher(publicKey, initialFileName, sourceFileName);
			decipher.BlockCompleted += (sender, args) => decipher.Stop();
			decipher.Decipher(privateKey, sourceFileName, destinationFileName);
			Assert.AreEqual(ProcessStatus.Stopped, decipher.Status);
		}

		#endregion

		private static void DeleteTestFolder() {
			if (Directory.Exists(testFolder))
				Directory.Delete(testFolder, true);
		}
	}
}