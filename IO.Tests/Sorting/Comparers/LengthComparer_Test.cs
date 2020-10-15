using CryptoFile.IO.Entities;
using CryptoFile.IO.Entities.Wrappers;
using CryptoFile.IO.Sorting;
using CryptoFile.IO.Sorting.Comparers;
using Moq;
using NUnit.Framework;

namespace CryptoFile.IO.Tests.Sorting.Comparers
{
	[TestFixture]
	public class LengthComparer_Test
	{
		private LengthComparer comparer;

		[SetUp]
		public void SetUp()
		{
			comparer = new LengthComparer(SortDirection.Ascending);
		}

		[Test]
		public void CompareTwoDirectories()
		{
			var firstDirectoryInfo = new Mock<IDirectoryInfo>();
			firstDirectoryInfo.Setup(x => x.Exists).Returns(true);
			firstDirectoryInfo.Setup(x => x.Length).Returns(200);
			var firstDirectory = new DirectoryEntity(firstDirectoryInfo.Object);

			var secondDirectoryInfo = new Mock<IDirectoryInfo>();
			secondDirectoryInfo.Setup(x => x.Exists).Returns(true);
			secondDirectoryInfo.Setup(x => x.Length).Returns(100);
			var secondDirectory = new DirectoryEntity(secondDirectoryInfo.Object);

			int compareResult = comparer.Compare(firstDirectory, secondDirectory);

			Assert.AreEqual(0, compareResult);
		}

		[Test]
		public void CompareTwoFiles()
		{
			var firstFileInfo = new Mock<IFileInfo>();
			firstFileInfo.Setup(x => x.Exists).Returns(true);
			firstFileInfo.Setup(x => x.Length).Returns(100);
			var firstFile = new FileEntity(firstFileInfo.Object);

			var secondFileInfo = new Mock<IFileInfo>();
			secondFileInfo.Setup(x => x.Exists).Returns(true);
			secondFileInfo.Setup(x => x.Length).Returns(50);
			var secondFile = new FileEntity(secondFileInfo.Object);

			int compareResult = comparer.Compare(firstFile, secondFile);

			Assert.AreEqual(1, compareResult);
		}
	}
}