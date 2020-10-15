using System;
using System.Collections.Generic;
using System.IO;
using CryptoFile.IO.Reading;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.IO.Tests.Reading
{
	[TestFixture]
	public class FileReader_Test
	{
		private const string testFolder = "test";

		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			Directory.CreateDirectory(testFolder);
		}

		[TearDown]
		public void TearDown()
		{
			Directory.Delete(testFolder, true);
		}

		#endregion

		[Test]
		public void Constructor_FileDoesntExist()
		{
			Assert.Throws(typeof(FileNotFoundException), () => new FileReader("hello", 1));
		}

		[Test]
		public void Constructor_FileNameIsEmpty()
		{
			Assert.Throws(typeof(ArgumentException), () => new FileReader("", 1));
		}

		[Test]
		public void Constructor_FileNameIsNull()
		{
			Assert.Throws(typeof(ArgumentNullException), () => new FileReader(null, 1));
		}

		[Test]
		public void Constructor_LengthIsZero()
		{
			Assert.Throws(typeof(ArgumentException), () => new FileReader("hello", 0));
		}

		[Test]
		public void Constructor_CheckHashCode()
		{
			string fileName = Path.Combine(testFolder, "source.txt");
			File.WriteAllBytes(fileName, new byte[] { 104, 101, 108, 108, 111 });

			using (var reader = new FileReader(fileName, 5))
			{
				var expectedBytes = new byte[]
				{
					93, 65, 64, 42,
					188, 75, 42, 118,
					185, 113, 157, 145,
					16, 23, 197, 146
				};
				TestHelper.CheckArray(expectedBytes, reader.HashCode);
			}
		}

		[Test]
		public void IsDone_FileIsEmpty()
		{
			CreateFile(testFolder + "\\file.bin");
			using (var reader = new FileReader(testFolder + "\\file.bin", 1))
			{
				Assert.IsTrue(reader.IsDone);
			}
		}

		[Test]
		public void IsDone_FileIsntEmpty()
		{
			CreateFile(testFolder + "\\file.bin", 1, 2);
			using (var reader = new FileReader(testFolder + "\\file.bin", 1))
			{
				Assert.IsFalse(reader.IsDone);
			}
		}

		[Test]
		public void ReadNextBlock_FileIsEmpty()
		{
			CreateFile(testFolder + "\\file.bin");
			using (var reader = new FileReader(testFolder + "\\file.bin", 1))
			{
				Assert.Throws(typeof(EndOfStreamException), () => reader.ReadNextBlock());
			}
		}

		[Test]
		public void ReadNextBlock1()
		{
			CreateFile(testFolder + "\\file.bin", 1, 0, 0, 0);
			using (var reader = new FileReader(testFolder + "\\file.bin", 2))
			{
				// Check first block
				BigNumber number = reader.ReadNextBlock();
				Assert.AreEqual(1, number.Digit);
				IList<int> numbers = number.Numbers;
				Assert.AreEqual(1, numbers[0]);
				Assert.IsFalse(reader.IsDone);
				// Check second block
				number = reader.ReadNextBlock();
				Assert.AreEqual(1, number.Digit);
				numbers = number.Numbers;
				Assert.AreEqual(0, numbers[0]);
				Assert.IsTrue(reader.IsDone);
			}
		}

		[Test]
		public void ReadNextBlock2()
		{
			CreateFile(testFolder + "\\file.bin", 1, 4, 7);
			using (var reader = new FileReader(testFolder + "\\file.bin", 2))
			{
				// Check first block
				BigNumber number = reader.ReadNextBlock();
				Assert.AreEqual(2, number.Digit);
				IList<int> numbers = number.Numbers;
				Assert.AreEqual(1, numbers[0]);
				Assert.AreEqual(4, numbers[1]);
				Assert.IsFalse(reader.IsDone);
				// Check second block
				number = reader.ReadNextBlock();
				Assert.AreEqual(1, number.Digit);
				numbers = number.Numbers;
				Assert.AreEqual(7, numbers[0]);
				Assert.IsTrue(reader.IsDone);
			}
		}

		private static void CreateFile(string fileName, params byte[] bytes)
		{
			TestHelper.CreateFile(fileName, bytes);
		}
	}
}