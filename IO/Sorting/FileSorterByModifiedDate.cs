using CryptoFile.IO.Sorting.Comparers;

namespace CryptoFile.IO.Sorting {
	public class FileSorterByModifiedDate : FileSorter {
		public FileSorterByModifiedDate() {}

		public FileSorterByModifiedDate(SortDirection direction) : base(direction) {}


		protected override SortColumn GetSortColumn() {
			return SortColumn.ModifiedDate;
		}

		protected override FileSystemEntityComparer CreateComparer(SortDirection direction) {
			return new ModifiedDateComparer(direction);
		}
	}
}