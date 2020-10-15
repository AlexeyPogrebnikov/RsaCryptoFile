using CryptoFile.Client.Configuration;
using CryptoFile.Client.Properties;

namespace CryptoFile.Client.Forms
{
	internal class FormFactory : IFormFactory
	{
		private readonly Options options;

		public FormFactory(Options options)
		{
			this.options = options;
		}

		public ICipherForm CreateCipherForm()
		{
			return new CipherForm { Language = GetLanguage() };
		}

		public IDecipherForm CreateDecipherForm()
		{
			return new DecipherForm { Language = GetLanguage() };
		}

		public IGenerateKeysForm CreateGenerateKeysForm()
		{
			return new GenerateKeysForm { Language = GetLanguage() };
		}

		public IAboutProgramForm CreateAboutProgramForm()
		{
			return new AboutProgramForm
			{
				Language = GetLanguage(),
				Email = Settings.Default.Email
			};
		}

		public IPublicExponentForm CreatePublicExponentForm()
		{
			return new PublicExponentForm { Language = GetLanguage() };
		}

		public IPropertiesForm CreatePropertiesForm()
		{
			return new PropertiesForm { Language = GetLanguage() };
		}

		private Language GetLanguage()
		{
			return options.Language;
		}
	}
}