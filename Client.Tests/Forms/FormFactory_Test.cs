using CryptoFile.Client.Configuration;
using CryptoFile.Client.Forms;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Forms {
	[TestFixture]
	public class FormFactory_Test {
		private FormFactory formFactory;
		private Options options;

		[SetUp]
		public void SetUp() {
			options = new Options();
			formFactory = new FormFactory(options);
		}

		[Test]
		public void CreateCipherForm_CheckSetLanguage() {
			options.Language = Language.Russian;
			using (var form = formFactory.CreateCipherForm()) {
				Assert.AreEqual(Language.Russian, form.Language);
			}
		}

		[Test]
		public void CreateGenerateKeysForm_CheckSetLanguage() {
			options.Language = Language.Russian;
			using (var form = formFactory.CreateGenerateKeysForm()) {
				Assert.AreEqual(Language.Russian, form.Language);
			}
		}

		[Test]
		public void CreateDecipherForm_CheckSetLanguage() {
			options.Language = Language.Russian;
			using (var form = formFactory.CreateDecipherForm()) {
				Assert.AreEqual(Language.Russian, form.Language);
			}
		}

		[Test]
		public void CreateAboutProgramForm_CheckSetLanguage() {
			options.Language = Language.Russian;
			using (var form = formFactory.CreateAboutProgramForm()) {
				Assert.AreEqual(Language.Russian, form.Language);
			}
		}

		[Test]
		public void CreatePublicExponentForm_CheckSetLanguage() {
			options.Language = Language.Russian;
			using (var form = formFactory.CreatePublicExponentForm()) {
				Assert.AreEqual(Language.Russian, form.Language);
			}
		}

		[Test]
		public void CreatePropertiesForm_CheckSetLanguage() {
			options.Language = Language.Russian;
			using (var form = formFactory.CreatePropertiesForm()) {
				Assert.AreEqual(Language.Russian, form.Language);
			}
		}
	}
}