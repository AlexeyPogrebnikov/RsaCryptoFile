using System.Collections.Generic;
using System.ComponentModel;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Sorting.Comparers;

namespace CryptoFile.IO.Sorting {
	public abstract class FileSorter {
		private SortDirection direction = SortDirection.Ascending;

		protected FileSorter() {}

		protected FileSorter(SortDirection direction) {
			this.direction = direction;
		}

		public void Sort(List<FileSystemEntity> entities) {
			var comparer = CreateComparer(direction);
			entities.Sort(comparer);
		}

		public void ChangeDirection() {
			direction = direction == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
		}

		public SortingInfo GetSortingInfo() {
			var sortingColumn = GetSortColumn();
			return new SortingInfo(sortingColumn, direction);
		}

		protected abstract SortColumn GetSortColumn();

		protected abstract FileSystemEntityComparer CreateComparer(SortDirection direction);

		public static FileSorter CreateSorter(SortColumn sortColumn, SortDirection direction) {
			switch (sortColumn) {
				case SortColumn.Name :
					return new FileSorterByName(direction);
				case SortColumn.Length :
					return new FileSorterByLength(direction);
				case SortColumn.Type :
					return new FileSorterByType(direction);
				case SortColumn.ModifiedDate :
					return new FileSorterByModifiedDate(direction);
			}
			throw new InvalidEnumArgumentException();
		}
	}
}