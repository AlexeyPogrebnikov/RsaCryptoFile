using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace CryptoFile.IO.Tests
{
	internal static class TestHelper
	{
		public static void CheckFile(string fileName, params byte[] expectedBytes)
		{
			byte[] actualBytes = File.ReadAllBytes(fileName);
			CheckArray(expectedBytes, actualBytes);
		}

		public static void CheckArray(IList<byte> expected, IList<byte> actual)
		{
			if (expected.Count != actual.Count)
			{
				Assert.AreEqual(expected.Count, actual.Count, "Длины не совпадают:\n" + GetMessage(expected, actual));
			}

			for (var i = 0; i < expected.Count; ++i)
			{
				if (expected[i] != actual[i])
				{
					Assert.AreEqual(expected[i], actual[i], "Ошибка в разряде: " + i + "\n" + GetMessage(expected, actual));
				}
			}
		}

		public static void CreateFile(string fileName, params byte[] bytes)
		{
			File.WriteAllBytes(fileName, bytes);
		}

		private static string GetMessage(IList<byte> expectedNumber, IList<byte> actualNumber)
		{
			string message = string.Format("\nExpectedNumber[{0}]: ", expectedNumber.Count);
			for (var i = 0; i < expectedNumber.Count; ++i)
			{
				message += expectedNumber[i];
				if (i < expectedNumber.Count - 1)
				{
					message += ", ";
				}
			}

			message += "\n";
			message += string.Format("ActualNumber[{0}]: ", actualNumber.Count);
			for (var i = 0; i < actualNumber.Count; ++i)
			{
				message += actualNumber[i];
				if (i < actualNumber.Count - 1)
				{
					message += ", ";
				}
			}

			message += "\n";
			return message;
		}
	}
}