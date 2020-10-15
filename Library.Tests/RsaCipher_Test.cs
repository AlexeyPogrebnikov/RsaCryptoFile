using System;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Library.Tests
{
	[TestFixture]
	public class RsaCipher_Test
	{
		#region Constructor

		[Test]
		public void Constructor_KeyIsNull()
		{
			Assert.Throws(typeof(ArgumentNullException), () => new RsaCipher(null));
		}

		#endregion

		#region Cipher

		[Test]
		public void Cipher1()
		{
			// p = 17
			// q = 23
			// n = p * q = 391
			// e = 3
			BigNumber data = BigNumber.FromBytes(new[] { 4 });
			BigNumber n = BigNumber.FromBytes(new[] { 135, 1 });
			BigNumber e = BigNumber.FromBytes(new[] { 3 });
			var key = new PublicKey(e, n);
			var rsa = new RsaCipher(key);
			BigNumber result = rsa.Cipher(data);
			CheckNumber(result, 64);
		}

		[Test]
		public void Cipher2()
		{
			// p = 17
			// q = 23
			// n = p * q = 391
			// e = 17
			BigNumber data = BigNumber.FromBytes(new[] { 2 });
			BigNumber n = BigNumber.FromBytes(new[] { 135, 1 });
			BigNumber e = BigNumber.FromBytes(new[] { 17 });
			var key = new PublicKey(e, n);
			var rsa = new RsaCipher(key);
			BigNumber result = rsa.Cipher(data);
			CheckNumber(result, 87);
		}

		[Test]
		public void Cipher3()
		{
			// p = 17
			// q = 23
			// n = p * q = 391
			// e = 3
			BigNumber data = BigNumber.FromBytes(new[] { 14, 1 });
			BigNumber n = BigNumber.FromBytes(new[] { 135, 1 });
			BigNumber e = BigNumber.FromBytes(new[] { 3 });
			var key = new PublicKey(e, n);
			var rsa = new RsaCipher(key);
			BigNumber result = rsa.Cipher(data);
			CheckNumber(result, 60);
		}

		#endregion

		private static void CheckNumber(BigNumber number, params int[] expectedNumbers)
		{
			TestHelper.CheckNumber(number, expectedNumbers);
		}
	}
}