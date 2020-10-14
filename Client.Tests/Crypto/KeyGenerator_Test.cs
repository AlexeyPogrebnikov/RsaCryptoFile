using CryptoFile.Client.Crypto;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;
using CryptoFile.Library.Prime;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Crypto {
	[TestFixture]
	public class KeyGenerator_Test {
		private KeyGenerator keyGenerator;
		private RsaKeyGenerator rsaKeyGenerator;
		private PrimeGenerator primeGenerator;

		[SetUp]
		public void SetUp() {
			rsaKeyGenerator = new RsaKeyGenerator();
			var rabinMillerTest = new RabinMillerTest(20);
			primeGenerator = new PrimeGenerator(rabinMillerTest);
			keyGenerator = new KeyGenerator(rsaKeyGenerator, primeGenerator);
		}

		[Test]
		public void Constructor_CheckStatus() {
			Assert.AreEqual(ProcessStatus.NotBeginning, keyGenerator.Status);
		}

		[Test]
		public void CheckStatusAfterGenerate() {
			keyGenerator.Generate(2, BigNumber.FromInt(3));

			Assert.AreEqual(ProcessStatus.Complete, keyGenerator.Status);
		}

		[Test]
		public void CheckStatusAfterStop() {
			keyGenerator.Stop();

			Assert.AreEqual(ProcessStatus.Stopped, keyGenerator.Status);
		}
	}
}