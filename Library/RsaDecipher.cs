using System;
using System.Collections.Generic;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Library
{
	public class RsaDecipher
	{
		private readonly IList<bool> degree;
		private readonly BigNumber n;

		/// <exception cref="ArgumentNullException">key is null</exception>
		public RsaDecipher(PrivateKey key)
		{
			Checker.CheckNull(key);
			degree = key.D.ToBits();
			n = key.N;
		}

		public BigNumber Decipher(BigNumber data)
		{
			return data.Power(degree, n);
		}
	}
}