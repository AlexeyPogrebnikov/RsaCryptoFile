using System;
using System.Collections.Generic;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Entities.Wrappers;
using CryptoFile.IO.Sorting;
using Moq;
using NUnit.Framework;

namespace CryptoFile.IO.Tests.Sorting
{
	public class FileSorterByModifiedDate_Test
	{
		private FileSorterByModifiedDate sorter;

		[SetUp]
		public void SetUp()
		{
			sorter = new FileSorterByModifiedDate();
		}

		[Test]
		public void SortTest()
		{
			var entities = new List<FileSystemEntity>();

			var fileInfoForFirst = new Mock<IFileInfo>();
			fileInfoForFirst.Setup(x => x.Exists).Returns(true);
			fileInfoForFirst.Setup(x => x.ModifiedDate).Returns(new DateTime(2010, 12, 1));
			var firstFileEntity = new FileEntity(fileInfoForFirst.Object);
			entities.Add(firstFileEntity);

			var fileInfoForSecond = new Mock<IFileInfo>();
			fileInfoForSecond.Setup(x => x.Exists).Returns(true);
			fileInfoForSecond.Setup(x => x.ModifiedDate).Returns(new DateTime(2010, 6, 1));
			var secondFileEntity = new FileEntity(fileInfoForSecond.Object);
			entities.Add(secondFileEntity);

			sorter.Sort(entities);
			Assert.AreEqual(new DateTime(2010, 6, 1), entities[0].ModifiedDate);
			Assert.AreEqual(new DateTime(2010, 12, 1), entities[1].ModifiedDate);
		}

		[Test]
		public void ChangeDirection()
		{
			var entities = new List<FileSystemEntity>();

			var fileInfoForFirst = new Mock<IFileInfo>();
			fileInfoForFirst.Setup(x => x.Exists).Returns(true);
			fileInfoForFirst.Setup(x => x.ModifiedDate).Returns(new DateTime(2010, 12, 1));
			var firstFileEntity = new FileEntity(fileInfoForFirst.Object);
			entities.Add(firstFileEntity);

			var fileInfoForSecond = new Mock<IFileInfo>();
			fileInfoForSecond.Setup(x => x.Exists).Returns(true);
			fileInfoForSecond.Setup(x => x.ModifiedDate).Returns(new DateTime(2010, 6, 1));
			var secondFileEntity = new FileEntity(fileInfoForSecond.Object);
			entities.Add(secondFileEntity);

			sorter.ChangeDirection();
			sorter.Sort(entities);
			Assert.AreEqual(new DateTime(2010, 12, 1), entities[0].ModifiedDate);
			Assert.AreEqual(new DateTime(2010, 6, 1), entities[1].ModifiedDate);
		}
	}
}