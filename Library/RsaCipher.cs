using System;
using System.Collections.Generic;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Library {
	public class RsaCipher {
		private readonly IList<bool> degree;
		private readonly BigNumber n;

		/// <exception cref="ArgumentNullException">key is null</exception>
		public RsaCipher(PublicKey key) {
			Checker.CheckNull(key);
			degree = key.E.ToBits();
			n = key.N;
		}

		public BigNumber Cipher(BigNumber data) {
			return data.Power(degree, n);
		}
	}
}