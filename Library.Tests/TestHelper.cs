using System.Collections.Generic;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Library.Tests
{
	internal static class TestHelper
	{
		public static void CheckNumber(BigNumber number, params int[] expectedNumbers)
		{
			CheckNumber(number.Numbers, expectedNumbers);
		}

		public static void CheckNumber(BooleanBigNumber number, params byte[] expectedNumbers)
		{
			CheckNumber(number.GetBytes(), expectedNumbers);
		}

		private static void CheckNumber<T>(IList<T> actualNumbers, IList<T> expectedNumbers)
		{
			Assert.AreEqual(expectedNumbers.Count, actualNumbers.Count,
				"Разрядность чисел не совпадает\n" + GetMessage(expectedNumbers, actualNumbers));
			for (var i = 0; i < expectedNumbers.Count; ++i)
			{
				if (!expectedNumbers[i].Equals(actualNumbers[i]))
				{
					Assert.AreEqual(expectedNumbers[i], actualNumbers[i],
						"Ошибка в разряде: " + i + "\n" + GetMessage(expectedNumbers, actualNumbers));
				}
			}
		}

		private static string GetMessage<T>(IList<T> expectedNumber, IList<T> actualNumber)
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
	}
}