using CryptoFile.Library.Keys;

namespace CryptoFile.Client.Crypto {
	public interface IRsaFileCipher : IRsaCryptoFile {
		void Cipher(PublicKey key, string sourceFileName, string destinationFileName);
	}
}