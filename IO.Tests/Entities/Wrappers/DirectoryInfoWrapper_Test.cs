using System.IO;
using CryptoFile.IO.Entities.Wrappers;
using NUnit.Framework;

namespace CryptoFile.IO.Tests.Entities.Wrappers {
	[TestFixture]
	public class DirectoryInfoWrapper_Test {
		private DirectoryInfoWrapper wrapper;
		private const string testFolder = "test";

		[SetUp]
		public void SetUp() {
			Directory.CreateDirectory(testFolder);
		}

		[TearDown]
		public void TearDown() {
			Directory.Delete(testFolder, true);
		}

		[Test]
		public void Length_TestWithFile() {
			File.WriteAllBytes(testFolder + "\\first.txt", new byte[] { 1, 2, 3 });
			wrapper = new DirectoryInfoWrapper(testFolder);

			Assert.AreEqual(3, wrapper.Length);
		}

		[Test]
		public void Length_TestWithInnerFolder() {
			File.WriteAllBytes(testFolder + "\\first.txt", new byte[] { 1, 2, 3 });
			const string innerFolder = testFolder + "\\inner";
			Directory.CreateDirectory(innerFolder);
			File.WriteAllBytes(testFolder + "\\second.bin", new byte[] { 4, 5, 6, 7 });
			wrapper = new DirectoryInfoWrapper(testFolder);

			Assert.AreEqual(7, wrapper.Length);
		}
	}
}