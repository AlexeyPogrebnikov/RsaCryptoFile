using CryptoFile.IO.Entities;

namespace CryptoFile.IO.Sorting.Comparers {
	class ModifiedDateComparer : FileSystemEntityComparer {
		public ModifiedDateComparer(SortDirection direction) : base(direction) {}

		protected override int DoCompare(FileSystemEntity first, FileSystemEntity second) {
			return first.ModifiedDate.CompareTo(second.ModifiedDate);
		}
	}
}