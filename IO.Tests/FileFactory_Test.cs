using System;
using System.IO;
using NUnit.Framework;

namespace CryptoFile.IO.Tests
{
	[TestFixture]
	public class FileFactory_Test
	{
		private FileFactory factory;
		private const string testFolder = "test";

		[SetUp]
		public void SetUp()
		{
			factory = new FileFactory(6);
			Directory.CreateDirectory(testFolder);
		}

		[TearDown]
		public void TearDown()
		{
			Directory.Delete(testFolder, true);
		}

		#region CreateFileReader

		[Test]
		public void CreateFileReader_FileNameIsNull()
		{
			Assert.Throws(typeof(ArgumentNullException), () => factory.CreateFileReader(null, 1));
		}

		[Test]
		public void CreateFileReader_FileNameIsEmpty()
		{
			Assert.Throws(typeof(ArgumentException), () => factory.CreateFileReader("", 1));
		}

		[Test]
		public void CreateFileReader_FileDoesntExist()
		{
			Assert.Throws(typeof(FileNotFoundException), () => factory.CreateFileReader("hello", 1));
		}

		[Test]
		public void CreateFileReader_LengthIsZero()
		{
			const string fileName = testFolder + "\\file.txt";
			File.WriteAllText(fileName, @"hello");
			Assert.Throws(typeof(ArgumentException), () => factory.CreateFileReader(fileName, 0));
		}

		[Test]
		public void CreateFileReader_LengthLessZero()
		{
			const string fileName = testFolder + "\\file.txt";
			File.WriteAllText(fileName, @"hello");
			Assert.Throws(typeof(ArgumentException), () => factory.CreateFileReader(fileName, -1));
		}

		#endregion
	}
}