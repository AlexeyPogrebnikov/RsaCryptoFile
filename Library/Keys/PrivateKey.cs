using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Library.Keys {
	public class PrivateKey {
		public PrivateKey(BigNumber d, BigNumber n) {
			D = d;
			N = n;
		}

		public BigNumber D { get; private set; }
		public BigNumber N { get; private set; }
	}
}