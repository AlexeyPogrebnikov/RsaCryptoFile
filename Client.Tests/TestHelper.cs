using System.Collections.Generic;
using System.IO;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Client.Tests {
	static class TestHelper {
		public static void CheckNumber(BigNumber number, params int[] expectedNumbers) {
			CheckArray(expectedNumbers, number.Numbers);
		}

		public static void CheckFile(string fileName, params byte[] expectedBytes) {
			var actualBytes = File.ReadAllBytes(fileName);
			CheckArray(expectedBytes, actualBytes);
		}

		public static void CheckArray(IList<int> expected, IList<int> actual) {
			if (expected.Count != actual.Count) {
				Assert.AreEqual(expected.Count, actual.Count, "Длины не совпадают:\n" + GetMessage(expected, actual));
			}
			for (var i = 0; i < expected.Count; ++i) {
				if (expected[i] != actual[i]) {
					Assert.AreEqual(expected[i], actual[i], "Ошибка в разряде: " + i + "\n" + GetMessage(expected, actual));
				}
			}
		}

		public static void CheckArray(IList<byte> expected, IList<byte> actual) {
			if (expected.Count != actual.Count) {
				Assert.AreEqual(expected.Count, actual.Count, "Длины не совпадают:\n" + GetMessage(expected, actual));
			}
			for (var i = 0; i < expected.Count; ++i) {
				if (expected[i] != actual[i]) {
					Assert.AreEqual(expected[i], actual[i], "Ошибка в разряде: " + i + "\n" + GetMessage(expected, actual));
				}
			}
		}

		private static string GetMessage(IList<int> expectedNumber, IList<int> actualNumber) {
			var message = string.Format("\nExpectedNumber[{0}]: ", expectedNumber.Count);
			for (var i = 0; i < expectedNumber.Count; ++i) {
				message += expectedNumber[i];
				if (i < expectedNumber.Count - 1) {
					message += ", ";
				}
			}
			message += "\n";
			message += string.Format("ActualNumber[{0}]: ", actualNumber.Count);
			for (var i = 0; i < actualNumber.Count; ++i) {
				message += actualNumber[i];
				if (i < actualNumber.Count - 1) {
					message += ", ";
				}
			}
			message += "\n";
			return message;
		}

		private static string GetMessage(IList<byte> expectedNumber, IList<byte> actualNumber) {
			var message = string.Format("\nExpectedNumber[{0}]: ", expectedNumber.Count);
			for (var i = 0; i < expectedNumber.Count; ++i) {
				message += expectedNumber[i];
				if (i < expectedNumber.Count - 1) {
					message += ", ";
				}
			}
			message += "\n";
			message += string.Format("ActualNumber[{0}]: ", actualNumber.Count);
			for (var i = 0; i < actualNumber.Count; ++i) {
				message += actualNumber[i];
				if (i < actualNumber.Count - 1) {
					message += ", ";
				}
			}
			message += "\n";
			return message;
		}
	}
}