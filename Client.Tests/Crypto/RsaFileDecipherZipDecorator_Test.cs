using CryptoFile.Client.Compression;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Environment;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Crypto
{
	[TestFixture]
	public class RsaFileDecipherZipDecorator_Test
	{
		private RsaFileDecipherZipDecorator decorator;
		private Mock<IRsaFileDecipher> rsaFileDecipher;
		private Mock<IEnvironmentHelper> environmentHelper;
		private Mock<IZipAlgorithm> zipAlgorithm;

		[SetUp]
		public void SetUp()
		{
			rsaFileDecipher = new Mock<IRsaFileDecipher>();
			environmentHelper = new Mock<IEnvironmentHelper>();
			zipAlgorithm = new Mock<IZipAlgorithm>();
			decorator = new RsaFileDecipherZipDecorator(rsaFileDecipher.Object, environmentHelper.Object, zipAlgorithm.Object);
		}

		[Test]
		public void DecipherTest()
		{
			BigNumber e = BigNumber.FromInt(3);
			BigNumber n = BigNumber.FromInt(5);
			var privateKey = new PrivateKey(e, n);

			const string sourceFileName = "source.rsa";
			const string temporaryFileName = "temporary.bin";
			const string destinationFileName = "destination.rsa";
			environmentHelper.Setup(x => x.GetTempFileName()).Returns(temporaryFileName);

			decorator.Decipher(privateKey, sourceFileName, destinationFileName);

			zipAlgorithm.Verify(x => x.DecompressFile(sourceFileName, temporaryFileName));
			environmentHelper.Verify(x => x.GetTempFileName());
			rsaFileDecipher.Verify(x => x.Decipher(privateKey, temporaryFileName, destinationFileName));
			environmentHelper.Verify(x => x.DeleteFile(temporaryFileName));
		}
	}
}