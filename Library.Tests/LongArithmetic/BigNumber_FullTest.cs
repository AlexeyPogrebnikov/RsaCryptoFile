using System;
using System.Collections.Generic;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Library.Tests.LongArithmetic {
	[TestFixture]
	public class BigNumber_FullTest {
		private static void CheckNumber(BigNumber expectedNumber, BigNumber actualNumber, int divisible, int divisor) {
			var expectedNumbers = expectedNumber.Numbers;
			var actualNumbers = actualNumber.Numbers;
			Assert.AreEqual(expectedNumber.Digit, actualNumber.Digit,
			                "Разрядность чисел не совпадает\n" + GetMessage(expectedNumbers, actualNumbers, divisible, divisor));
			for (var i = 0; i < expectedNumber.Digit; ++i) {
				if (expectedNumbers[i] != actualNumbers[i]) {
					Assert.AreEqual(expectedNumbers[i], actualNumbers[i],
					                "Ошибка в разряде :" + i + "\n" + GetMessage(expectedNumbers, actualNumbers, divisible, divisor));
				}
			}
		}

		private static string GetMessage(IList<int> expectedNumber, IList<int> actualNumber, int divisible, int divisor) {
			var message = divisible + " / " + divisor + "\n";
			message += string.Format("\nExpectedNumber[{0}]: ", expectedNumber.Count);
			for (var i = 0; i < expectedNumber.Count; ++i) {
				message += expectedNumber[i] + ", ";
			}
			message += "\n";
			message += string.Format("ActualNumber[{0}]: ", actualNumber.Count);
			for (var i = 0; i < actualNumber.Count; ++i) {
				message += actualNumber[i] + ", ";
			}
			message += "\n";
			return message;
		}

		[Test]
		public void Division() {
			for (var i = 1; i < 1000; ++i) {
				for (var j = 1; j < 1000; ++j) {
					var divisible = BigNumber.FromInt(i);
					var divisor = BigNumber.FromInt(j);
					var actual = divisible.Division(divisor);
					var expected = BigNumber.FromInt(i/j);
					CheckNumber(expected, actual, i, j);
				}
				if (i%100 == 0) {
					Console.Out.WriteLine(i.ToString());
				}
			}
		}
	}
}