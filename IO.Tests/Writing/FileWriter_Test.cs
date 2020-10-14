using System;
using System.IO;
using CryptoFile.IO.Writing;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.IO.Tests.Writing {
	[TestFixture]
	public class FileWriter_Test {
		private const string testFolder = "test";

		[SetUp]
		public void SetUp() {
			Directory.CreateDirectory(testFolder);
		}

		[TearDown]
		public void TearDown() {
			Directory.Delete(testFolder, true);
		}

		#region Constructor

		[Test]
		public void Constructor_FileNameIsNull() {
			Assert.Throws(typeof(ArgumentNullException), () => new FileWriter(null, 1, 5));
		}

		[Test]
		public void Constructor_FileNameIsEmpty() {
			Assert.Throws(typeof(ArgumentException), () => new FileWriter("", 1, 5));
		}

		[Test]
		public void Constructor_BlockLengthLessZero() {
			Assert.Throws(typeof(ArgumentException), () => new FileWriter("hello", -1, 5));
		}

		[Test]
		public void Constructor_BlockLengthEqualZero() {
			Assert.Throws(typeof(ArgumentException), () => new FileWriter("hello", 0, 5));
		}

		#endregion

		#region WriteNextBlock

		[Test]
		public void WriteNextBlock1() {
			const string fileName = testFolder + "\\file.exe";
			using (var writer = new FileWriter(fileName, 8, 4)) {
				var number = BigNumber.FromBytes(new[] { 5, 2, 7, 2 });
				writer.WriteNextBlock(number);
			}
			TestHelper.CheckFile(fileName, new byte[] { 5, 2, 7, 2 });
		}

		[Test]
		public void WriteNextBlock2() {
			const string fileName = testFolder + "\\file.exe";
			using (var writer = new FileWriter(fileName, 8, 11)) {
				var number = BigNumber.FromBytes(new[] { 45, 23, 129, 243, 4, 12, 56, 90 });
				writer.WriteNextBlock(number);
				number = BigNumber.FromBytes(new[] { 5, 2, 7 });
				writer.WriteNextBlock(number);
			}
			TestHelper.CheckFile(fileName, new byte[] { 45, 23, 129, 243, 4, 12, 56, 90, 5, 2, 7 });
		}

		[Test]
		public void WriteNextBlock3() {
			const string fileName = testFolder + "\\file.exe";
			using (var writer = new FileWriter(fileName, 5, 10)) {
				var number = BigNumber.FromBytes(new[] { 87, 32, 107, 42, 58 });
				writer.WriteNextBlock(number);
				number = BigNumber.FromBytes(new[] { 31, 178, 241, 255, 43 });
				writer.WriteNextBlock(number);
			}
			TestHelper.CheckFile(fileName, new byte[] { 87, 32, 107, 42, 58, 31, 178, 241, 255, 43 });
		}

		#endregion
	}
}