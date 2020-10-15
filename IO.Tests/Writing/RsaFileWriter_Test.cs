using System;
using System.IO;
using CryptoFile.IO.Writing;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.IO.Tests.Writing
{
	[TestFixture]
	public class RsaFileWriter_Test
	{
		private RsaFileWriter writer;
		private const string testFolder = "testFolder";

		[SetUp]
		public void SetUp()
		{
			DeleteTestFolder();
			Directory.CreateDirectory(testFolder);
		}

		[TearDown]
		public void TearDown()
		{
			DeleteTestFolder();
		}

		[Test]
		public void WriteHeaderTest()
		{
			string firstName = Path.Combine(testFolder, "file.rsa");
			using (writer = new RsaFileWriter(15, firstName, 10))
			{
				var hashCode = new byte[]
				{
					1, 2, 3, 4,
					5, 6, 7, 8,
					9, 10, 11, 12,
					13, 14, 15, 16
				};
				writer.WriteHeader(1000, hashCode);
			}

			var expectedBytes = new byte[]
			{
				15, // версия
				10, 0, // размер блока
				232, 3, 0, 0, // размер исходного файла
				0, 0, 0, 0, // размер исходного файла
				1, 2, 3, 4, // хэш
				5, 6, 7, 8, // хэш
				9, 10, 11, 12, // хэш
				13, 14, 15, 16 // хэш
			};
			TestHelper.CheckFile(firstName, expectedBytes);
		}

		[Test]
		public void DeleteTest()
		{
			string firstName = Path.Combine(testFolder, "file.rsa");
			using (writer = new RsaFileWriter(15, firstName, 10))
			{
				writer.Delete();
			}

			Assert.IsFalse(File.Exists(firstName));
		}

		[Test]
		public void WriteNextBlock_DigitLessThanBlockLength()
		{
			string firstName = Path.Combine(testFolder, "file.rsa");
			using (writer = new RsaFileWriter(3, firstName, 5))
			{
				BigNumber number = BigNumber.FromBytes(new[] { 1, 67, 32, 150 });
				writer.WriteNextBlock(number);
			}

			var expectedBytes = new byte[]
			{
				1, 67, 32, 150, 0
			};
			TestHelper.CheckFile(firstName, expectedBytes);
		}

		[Test]
		public void WriteNextBlock_DigitGreaterThanBlockLength()
		{
			string firstName = Path.Combine(testFolder, "file.rsa");
			using (writer = new RsaFileWriter(3, firstName, 2))
			{
				BigNumber number = BigNumber.FromBytes(new[] { 1, 67, 32, 150 });
				Assert.Throws(typeof(ArgumentException), () => writer.WriteNextBlock(number));
			}
		}

		private static void DeleteTestFolder()
		{
			if (Directory.Exists(testFolder))
				Directory.Delete(testFolder, true);
		}
	}
}