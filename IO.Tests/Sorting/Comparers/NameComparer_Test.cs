using CryptoFile.IO.Entities;
using CryptoFile.IO.Entities.Wrappers;
using CryptoFile.IO.Sorting;
using CryptoFile.IO.Sorting.Comparers;
using Moq;
using NUnit.Framework;

namespace CryptoFile.IO.Tests.Sorting.Comparers
{
	[TestFixture]
	public class NameComparer_Test
	{
		private NameComparer comparer;

		[SetUp]
		public void SetUp()
		{
			comparer = new NameComparer(SortDirection.Ascending);
		}

		[Test]
		public void CompareFileAndDirectory()
		{
			var fileInfo = new Mock<IFileInfo>();
			fileInfo.Setup(x => x.Name).Returns("a");
			fileInfo.Setup(x => x.Exists).Returns(true);
			var fileEntity = new FileEntity(fileInfo.Object);

			var directoryInfo = new Mock<IDirectoryInfo>();
			directoryInfo.Setup(x => x.Name).Returns("b");
			directoryInfo.Setup(x => x.Exists).Returns(true);
			var directoryEntity = new DirectoryEntity(directoryInfo.Object);

			int compare = comparer.Compare(fileEntity, directoryEntity);
			Assert.AreEqual(1, compare);
		}
	}
}