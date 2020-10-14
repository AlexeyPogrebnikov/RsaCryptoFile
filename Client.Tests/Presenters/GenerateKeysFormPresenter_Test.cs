using System;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Forms;
using CryptoFile.Client.Presenters;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Presenters {
	[TestFixture]
	public class GenerateKeysFormPresenter_Test {
		private Mock<IGenerateKeysForm> generateKeysForm;
		private Mock<IKeyGenerator> keyGenerator;
		private Options options;
		private Mock<IMessageHelper> messageHelper;
		private Mock<IFormFactory> formFactory;

		[SetUp]
		public void SetUp() {
			generateKeysForm = new Mock<IGenerateKeysForm>();
			generateKeysForm.SetupProperty(x => x.PublicExponent, 0);
			keyGenerator = new Mock<IKeyGenerator>();
			options = new Options();
			messageHelper = new Mock<IMessageHelper>();
			formFactory = new Mock<IFormFactory>();
		}

		[Test]
		public void Constructor_CheckSetPublicExponent() {
			options.PublicExponent = 100;
			CreatePresenter();

			Assert.AreEqual(100, generateKeysForm.Object.PublicExponent);
		}

		[Test]
		public void Generate_CheckSetRsaKeyLength() {
			var e = BigNumber.FromInt(0);
			var n = BigNumber.FromInt(0);
			var d = BigNumber.FromInt(0);
			var rsaKey = new RsaKey(new PublicKey(e, n), new PrivateKey(d, n));
			keyGenerator.Setup(x => x.Generate(It.IsAny<int>(), It.IsAny<BigNumber>())).Returns(rsaKey);
			CreatePresenter();

			generateKeysForm.SetupProperty(x => x.RsaKeyLength, 100);

			generateKeysForm.Raise(x => x.Generate += null, EventArgs.Empty);

			Assert.AreEqual(100, generateKeysForm.Object.RsaKeyLength);
		}

		[Test]
		public void ChangePublicExponent() {
			var publicExponentForm = new Mock<IPublicExponentForm>();
			formFactory.Setup(x => x.CreatePublicExponentForm()).Returns(publicExponentForm.Object);
			CreatePresenter();
			generateKeysForm.Raise(x => x.ChangePublicExponent += null, EventArgs.Empty);

			formFactory.Verify(x => x.CreatePublicExponentForm());
		}

		private void CreatePresenter() {
			new GenerateKeysFormPresenter(generateKeysForm.Object, keyGenerator.Object, options, messageHelper.Object,
			                              formFactory.Object);
		}
	}
}