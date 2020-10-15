using CryptoFile.Library.Keys;

namespace CryptoFile.Client.Crypto
{
	public interface IRsaFileDecipher : IRsaCryptoFile
	{
		void Decipher(PrivateKey key, string sourceFileName, string destinationFileName);
	}
}