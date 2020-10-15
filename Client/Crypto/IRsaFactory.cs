namespace CryptoFile.Client.Crypto
{
	public interface IRsaFactory
	{
		IRsaFileCipher CreateRsaFileCipher();
		IRsaFileDecipher CreateRsaFileDecipher(string fileName);
	}
}