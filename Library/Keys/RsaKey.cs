namespace CryptoFile.Library.Keys {
	public class RsaKey {
		public RsaKey(PublicKey publicKey, PrivateKey privateKey) {
			PublicKey = publicKey;
			PrivateKey = privateKey;
		}

		public PublicKey PublicKey { get; private set; }
		public PrivateKey PrivateKey { get; private set; }
	}
}