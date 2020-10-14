using CryptoFile.IO.Sorting.Comparers;

namespace CryptoFile.IO.Sorting {
	public class FileSorterByLength : FileSorter {
		public FileSorterByLength() {}

		public FileSorterByLength(SortDirection direction) : base(direction) {}

		protected override SortColumn GetSortColumn() {
			return SortColumn.Length;
		}

		protected override FileSystemEntityComparer CreateComparer(SortDirection direction) {
			return new LengthComparer(direction);
		}
	}
}