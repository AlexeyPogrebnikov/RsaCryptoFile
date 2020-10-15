using CryptoFile.IO.Entities;

namespace CryptoFile.IO.Sorting.Comparers
{
	internal class NameComparer : FileSystemEntityComparer
	{
		public NameComparer(SortDirection direction) : base(direction)
		{
		}

		protected override int DoCompare(FileSystemEntity first, FileSystemEntity second)
		{
			return first.Name.CompareTo(second.Name);
		}
	}
}