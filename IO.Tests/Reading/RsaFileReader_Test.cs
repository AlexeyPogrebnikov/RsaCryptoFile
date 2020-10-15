using System;
using System.IO;
using CryptoFile.IO.Exceptions;
using CryptoFile.IO.Reading;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.IO.Tests.Reading
{
	[TestFixture]
	public class RsaFileReader_Test
	{
		private RsaFileReader reader;
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
		public void ConstructorTest()
		{
			string fileName = Path.Combine(testFolder, "file.zip");
			File.WriteAllBytes(fileName, new byte[20]);

			using (reader = new RsaFileReader(fileName))
			{
				Assert.AreEqual(0, reader.Version);
				Assert.AreEqual(0, reader.BlockLength);
				Assert.AreEqual(0, reader.SourceFileLength);
			}
		}

		[Test]
		public void Length_LengthIs3()
		{
			string fileName = Path.Combine(testFolder, "file.txt");
			File.WriteAllBytes(fileName, new byte[] { 34, 89, 140 });
			using (reader = new RsaFileReader(fileName))
			{
				Assert.AreEqual(3, reader.Length);
			}
		}

		[Test]
		public void Length_LengthIs5()
		{
			string fileName = Path.Combine(testFolder, "file.txt");
			File.WriteAllBytes(fileName, new byte[] { 23, 67, 34, 89, 140 });
			using (reader = new RsaFileReader(fileName))
			{
				Assert.AreEqual(5, reader.Length);
			}
		}

		[Test]
		public void IsDone_FileIsEmpty()
		{
			string fileName = Path.Combine(testFolder, "file.bin");
			File.Create(fileName).Dispose();
			using (reader = new RsaFileReader(fileName))
			{
				Assert.IsTrue(reader.IsDone);
			}
		}

		[Test]
		public void IsDone_FileIsNotEmpty()
		{
			string fileName = Path.Combine(testFolder, "file.exe");
			File.WriteAllText(fileName, "hello");
			using (reader = new RsaFileReader(fileName))
			{
				Assert.IsFalse(reader.IsDone);
			}
		}

		[Test]
		public void ReadHeader_FileTooSmall()
		{
			string fileName = Path.Combine(testFolder, "file.h");
			var bytes = new byte[26];
			File.WriteAllBytes(fileName, bytes);
			using (reader = new RsaFileReader(fileName))
			{
				Assert.Throws(typeof(SourceFileException), () => reader.ReadHeader());
			}
		}

		[Test]
		public void ReadHeader_CheckVersion()
		{
			string fileName = Path.Combine(testFolder, "file.bin");
			var bytes = new byte[27];
			bytes[0] = 12;
			File.WriteAllBytes(fileName, bytes);
			using (reader = new RsaFileReader(fileName))
			{
				reader.ReadHeader();
				Assert.AreEqual(12, reader.Version);
			}
		}

		[Test]
		public void ReadHeader_CheckBlockLength()
		{
			string fileName = Path.Combine(testFolder, "file.bin");
			var bytes = new byte[27];
			bytes[1] = 23;
			bytes[2] = 1;
			File.WriteAllBytes(fileName, bytes);
			using (reader = new RsaFileReader(fileName))
			{
				reader.ReadHeader();
				Assert.AreEqual(279, reader.BlockLength);
			}
		}

		[Test]
		public void ReadHeader_CheckSourceFileLength()
		{
			string fileName = Path.Combine(testFolder, "file.bin");
			var bytes = new byte[27];
			// 10 FF FF FF FF FF FF FF
			bytes[3] = 255;
			bytes[4] = 255;
			bytes[5] = 255;
			bytes[6] = 255;

			bytes[7] = 255;
			bytes[8] = 255;
			bytes[9] = 255;
			bytes[10] = 16;
			File.WriteAllBytes(fileName, bytes);
			using (reader = new RsaFileReader(fileName))
			{
				reader.ReadHeader();
				Assert.AreEqual(1224979098644774911, reader.SourceFileLength);
			}
		}

		[Test]
		public void ReadHeader_CheckHashCode()
		{
			string fileName = Path.Combine(testFolder, "file.bin");
			var bytes = new byte[]
			{
				0, // version
				4, 0, // length of blocks
				1, 0, 0, 0, // length of source file
				0, 0, 0, 0, // length of source file
				1, 2, 3, 4, // hash code
				5, 6, 7, 8, // hash code
				9, 10, 11, 12, // hash code
				13, 14, 15, 16, // hash code
				0, 0, 0, 0 // first block
			};
			File.WriteAllBytes(fileName, bytes);
			using (reader = new RsaFileReader(fileName))
			{
				reader.ReadHeader();
				var expected = new byte[]
				{
					1, 2, 3, 4,
					5, 6, 7, 8,
					9, 10, 11, 12,
					13, 14, 15, 16
				};
				TestHelper.CheckArray(expected, reader.HashCode);
			}
		}

		[Test]
		public void ReadNextBlock_BlockContainsOnlyZeros()
		{
			string fileName = Path.Combine(testFolder, "file.bin");
			var bytes = new byte[]
			{
				0, // version
				4, 0, // length of blocks
				1, 0, 0, 0, // length of source file
				0, 0, 0, 0, // length of source file
				0, 0, 0, 0, // hash code
				0, 0, 0, 0, // hash code
				0, 0, 0, 0, // hash code
				0, 0, 0, 0, // hash code
				0, 0, 0, 0 // first block
			};
			File.WriteAllBytes(fileName, bytes);
			using (reader = new RsaFileReader(fileName))
			{
				reader.ReadHeader();
				BigNumber number = reader.ReadNextBlock();
				Assert.IsTrue(number.IsZero);
			}
		}

		[Test]
		public void ReadNextBlockBeforeReadHeader()
		{
			string fileName = Path.Combine(testFolder, "file.bin");
			File.WriteAllBytes(fileName, new byte[20]);
			using (reader = new RsaFileReader(fileName))
			{
				Assert.Throws(typeof(InvalidOperationException), () => reader.ReadNextBlock());
			}
		}

		[Test]
		public void ReadNextBlock_BlockLengthIs5()
		{
			string fileName = Path.Combine(testFolder, "file.bin");
			var bytes = new byte[32];
			bytes[1] = 5;
			bytes[27] = 23;
			bytes[28] = 67;
			bytes[29] = 12;
			bytes[30] = 149;
			bytes[31] = 253;
			File.WriteAllBytes(fileName, bytes);
			using (reader = new RsaFileReader(fileName))
			{
				reader.ReadHeader();
				BigNumber number = reader.ReadNextBlock();
				BigNumber expectedNumber = BigNumber.FromBytes(new[] { 23, 67, 12, 149, 253 });
				Assert.AreEqual(CompareResult.Equal, expectedNumber.Compare(number));
			}
		}

		[Test]
		public void ReadNextBlock_ZeroBeforeNumber()
		{
			string fileName = Path.Combine(testFolder, "file.bin");
			var bytes = new byte[29];
			bytes[1] = 2;
			bytes[27] = 45;
			bytes[28] = 0;
			File.WriteAllBytes(fileName, bytes);
			using (reader = new RsaFileReader(fileName))
			{
				reader.ReadHeader();
				BigNumber number = reader.ReadNextBlock();
				BigNumber expectedNumber = BigNumber.FromBytes(new[] { 45 });
				Assert.AreEqual(CompareResult.Equal, expectedNumber.Compare(number));
			}
		}

		private static void DeleteTestFolder()
		{
			if (Directory.Exists(testFolder))
				Directory.CreateDirectory(testFolder);
		}
	}
}