using System.Collections.Generic;
using CryptoFile.IO.Entities;

namespace CryptoFile.IO.Sorting.Comparers {
	public abstract class FileSystemEntityComparer : IComparer<FileSystemEntity> {
		private readonly SortDirection direction;

		protected FileSystemEntityComparer(SortDirection direction) {
			this.direction = direction;
		}

		public int Compare(FileSystemEntity first, FileSystemEntity second) {
			if (first.IsFile != second.IsFile)
				return first.IsFile.CompareTo(second.IsFile);

			var compare = DoCompare(first, second);
			if (direction == SortDirection.Descending)
				compare *= -1;
			return compare;
		}

		protected abstract int DoCompare(FileSystemEntity first, FileSystemEntity second);
	}
}