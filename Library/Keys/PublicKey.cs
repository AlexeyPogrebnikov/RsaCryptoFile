using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Library.Keys {
	public class PublicKey {
		public PublicKey(BigNumber e, BigNumber n) {
			N = n;
			E = e;
		}

		public BigNumber E { get; private set; }
		public BigNumber N { get; private set; }
	}
}