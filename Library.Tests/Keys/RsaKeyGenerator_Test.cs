using System.Collections.Generic;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Library.Tests.Keys
{
	[TestFixture]
	public class RsaKeyGenerator_Test
	{
		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			gener = new RsaKeyGenerator();
		}

		#endregion

		private RsaKeyGenerator gener;

		private static BigNumber CreateNumber(params int[] numbers)
		{
			return BigNumber.FromBytes(numbers);
		}

		private static void CheckNumber(BigNumber number, params int[] expectedNumbers)
		{
			IList<int> numbers = number.Numbers;
			Assert.AreEqual(expectedNumbers.Length, numbers.Count,
				"Разрядность чисел не совпадает\n" + GetMessage(expectedNumbers, number.Numbers));
			for (var i = 0; i < expectedNumbers.Length; ++i)
			{
				if (expectedNumbers[i] != numbers[i])
				{
					Assert.AreEqual(expectedNumbers[i], numbers[i],
						"Ошибка в разряде :" + i + "\n" + GetMessage(expectedNumbers, number.Numbers));
				}
			}
		}

		private static string GetMessage(IList<int> expectedNumber, IList<int> actualNumber)
		{
			string message = string.Format("\nExpectedNumber[{0}]: ", expectedNumber.Count);
			for (var i = 0; i < expectedNumber.Count; ++i)
			{
				message += expectedNumber[i] + ", ";
			}

			message += "\n";
			message += string.Format("ActualNumber[{0}]: ", actualNumber.Count);
			for (var i = 0; i < actualNumber.Count; ++i)
			{
				message += actualNumber[i] + ", ";
			}

			message += "\n";
			return message;
		}

		[Test]
		public void Generate1()
		{
			BigNumber p = CreateNumber(229, 13); // 3557
			BigNumber q = CreateNumber(19, 10); // 2579
			BigNumber e = CreateNumber(3);
			RsaKey key = gener.Generate(p, q, e);
			// Check Up
			CheckNumber(key.PublicKey.E, 3);
			CheckNumber(key.PublicKey.N, 255, 249, 139); // 9173503
			CheckNumber(key.PrivateKey.D, 91, 65, 93); // 6111579
			CheckNumber(key.PrivateKey.N, 255, 249, 139); // 9173503
		}

		[Test]
		public void Generate2()
		{
			BigNumber p = CreateNumber(17);
			BigNumber q = CreateNumber(23);
			BigNumber e = CreateNumber(7);
			RsaKey key = gener.Generate(p, q, e);
			// Check Up
			CheckNumber(key.PublicKey.E, 7);
			CheckNumber(key.PublicKey.N, 135, 1); // 391
			CheckNumber(key.PrivateKey.D, 151);
			CheckNumber(key.PrivateKey.N, 135, 1); // 391
		}
	}
}