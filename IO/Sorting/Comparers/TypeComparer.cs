using CryptoFile.IO.Entities;

namespace CryptoFile.IO.Sorting.Comparers {
	class TypeComparer : FileSystemEntityComparer {
		public TypeComparer(SortDirection direction) : base(direction) {}

		protected override int DoCompare(FileSystemEntity first, FileSystemEntity second) {
			return first.Type.CompareTo(second.Type);
		}
	}
}