using System.IO;
using CryptoFile.IO.Entities.Wrappers;
using Moq;
using NUnit.Framework;

namespace CryptoFile.IO.Tests.Entities.Wrappers
{
	[TestFixture]
	public class FileInfoWrapper_Test
	{
		private FileInfoWrapper wrapper;
		private const string testFolder = "testFolder";

		[SetUp]
		public void SetUp()
		{
			Directory.CreateDirectory(testFolder);
		}

		[TearDown]
		public void TearDown()
		{
			if (Directory.Exists(testFolder))
			{
				Directory.Delete(testFolder, true);
			}
		}

		[Test]
		public void GetDataTest()
		{
			string fileName = string.Format("{0}\\file.bin", testFolder);
			File.WriteAllBytes(fileName, new byte[] { 45, 190 });
			var fileInfo = new FileInfo(fileName);
			var directoryInfo = new Mock<IDirectoryInfo>();
			wrapper = new FileInfoWrapper(fileInfo, directoryInfo.Object);
			byte[] data = wrapper.GetData();

			TestHelper.CheckArray(new byte[] { 45, 190 }, data);
		}
	}
}