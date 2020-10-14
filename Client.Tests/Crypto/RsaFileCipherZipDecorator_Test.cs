using CryptoFile.Client.Compression;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Environment;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Crypto {
	[TestFixture]
	public class RsaFileCipherZipDecorator_Test {
		private RsaFileCipherZipDecorator decorator;
		private Mock<IRsaFileCipher> rsaFileCipher;
		private Mock<IEnvironmentHelper> environmentHelper;
		private Mock<IZipAlgorithm> zipAlgorithm;

		[SetUp]
		public void SetUp() {
			rsaFileCipher = new Mock<IRsaFileCipher>();
			environmentHelper = new Mock<IEnvironmentHelper>();
			zipAlgorithm = new Mock<IZipAlgorithm>();
			decorator = new RsaFileCipherZipDecorator(rsaFileCipher.Object, environmentHelper.Object, zipAlgorithm.Object);
		}

		[Test]
		public void CipherTest() {
			const string temporaryFileName = "hello.bin";
			environmentHelper.Setup(x => x.GetTempFileName()).Returns(temporaryFileName);
			var e = BigNumber.FromInt(3);
			var n = BigNumber.FromInt(36);
			var publicKey = new PublicKey(e, n);
			const string sourceFileName = "source.txt";
			const string destinationFileName = "dest.dat";
			decorator.Cipher(publicKey, sourceFileName, destinationFileName);

			rsaFileCipher.Verify(x => x.Cipher(It.IsAny<PublicKey>(), It.IsAny<string>(), temporaryFileName));
			environmentHelper.Verify(x => x.DeleteFile(temporaryFileName));
			zipAlgorithm.Verify(x => x.CompressFile(temporaryFileName, destinationFileName));
		}
	}
}