using System.Collections.Generic;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Entities.Wrappers;
using CryptoFile.IO.Sorting;
using Moq;
using NUnit.Framework;

namespace CryptoFile.IO.Tests.Sorting
{
	[TestFixture]
	public class FileSorterByName_Test
	{
		private FileSorterByName sorter;

		[SetUp]
		public void SetUp()
		{
			sorter = new FileSorterByName();
		}

		[Test]
		public void SortTest()
		{
			var entities = new List<FileSystemEntity>();

			var fileInfoForFirst = new Mock<IFileInfo>();
			fileInfoForFirst.Setup(x => x.Exists).Returns(true);
			fileInfoForFirst.Setup(x => x.Name).Returns("b");
			var firstFileEntity = new FileEntity(fileInfoForFirst.Object);
			entities.Add(firstFileEntity);

			var fileInfoForSecond = new Mock<IFileInfo>();
			fileInfoForSecond.Setup(x => x.Exists).Returns(true);
			fileInfoForSecond.Setup(x => x.Name).Returns("a");
			var secondFileEntity = new FileEntity(fileInfoForSecond.Object);
			entities.Add(secondFileEntity);

			sorter.Sort(entities);
			Assert.AreEqual("a", entities[0].Name);
			Assert.AreEqual("b", entities[1].Name);
		}
	}
}