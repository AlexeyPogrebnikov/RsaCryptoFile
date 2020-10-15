namespace CryptoFile.Client.Forms
{
	public interface IFormFactory
	{
		ICipherForm CreateCipherForm();
		IDecipherForm CreateDecipherForm();
		IGenerateKeysForm CreateGenerateKeysForm();
		IAboutProgramForm CreateAboutProgramForm();
		IPublicExponentForm CreatePublicExponentForm();
		IPropertiesForm CreatePropertiesForm();
	}
}