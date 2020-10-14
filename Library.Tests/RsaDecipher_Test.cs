using System;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Library.Tests {
	[TestFixture]
	public class RsaDecipher_Test {
		#region Constructor

		[Test]
		public void Constructor_KeyIsNull() {
			Assert.Throws(typeof(ArgumentNullException), () => new RsaDecipher(null));
		}

		#endregion

		#region Decipher

		[Test]
		public void Decipher1() {
			// p = 17
			// q = 23
			// n = p * q = 391
			// (p - 1) * (q - 1) = 352
			// e = 17
			// d = 145
			var data = BigNumber.FromBytes(new[] { 87 });
			var n = BigNumber.FromBytes(new[] { 135, 1 });
			var d = BigNumber.FromBytes(new[] { 145 });
			var key = new PrivateKey(d, n);
			var rsa = new RsaDecipher(key);
			var result = rsa.Decipher(data);
			// Check Up
			CheckNumber(result, 2);
		}

		[Test]
		public void Decipher2() {
			// p = 17
			// q = 23
			// n = p * q = 391
			// (p - 1) * (q - 1) = 352
			// e = 3
			// d = 235
			var data = BigNumber.FromBytes(new[] { 60 });
			var n = BigNumber.FromBytes(new[] { 135, 1 });
			var d = BigNumber.FromBytes(new[] { 235 });
			var key = new PrivateKey(d, n);
			var rsa = new RsaDecipher(key);
			var result = rsa.Decipher(data);
			// Check Up
			CheckNumber(result, 14, 1);
		}

		#endregion

		private static void CheckNumber(BigNumber number, params int[] expectedNumbers) {
			TestHelper.CheckNumber(number, expectedNumbers);
		}
	}
}