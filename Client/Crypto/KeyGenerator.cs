using System;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;
using CryptoFile.Library.Prime;

namespace CryptoFile.Client.Crypto {
	class KeyGenerator : IKeyGenerator {
		private readonly IPrimeGenerator primeGener;
		private readonly RsaKeyGenerator rsaGener;

		public KeyGenerator(RsaKeyGenerator rsaGener, IPrimeGenerator primeGener) {
			this.rsaGener = rsaGener;
			this.primeGener = primeGener;
			Status = ProcessStatus.NotBeginning;
		}

		/// <summary>
		/// Генерирует ключи RSA
		/// </summary>
		/// <param name="length">длина n в битах</param>
		/// <param name="e"></param>
		public RsaKey Generate(int length, BigNumber e) {
			if (length < 1) {
				throw new ArgumentOutOfRangeException("length", length, @"length < 1");
			}
			Status = ProcessStatus.Processing;
			var pLength = GetRandomLength(length);
			var qLength = Math.Max(1, length/8 - pLength);
			var p = primeGener.Generate(pLength);
			var q = primeGener.Generate(qLength);
			var rsaKey = rsaGener.Generate(p, q, e);
			Status = ProcessStatus.Complete;
			return rsaKey;
		}

		public void Stop() {
			Status = ProcessStatus.Stopped;
		}

		public ProcessStatus Status { get; private set; }

		private static int GetRandomLength(int length) {
			var random = new Random();
			var min = Math.Max(length/3, 1);
			var max = Math.Max((2*length)/3, 1);
			return Math.Max(1, random.Next(min, max)/8);
		}
	}
}