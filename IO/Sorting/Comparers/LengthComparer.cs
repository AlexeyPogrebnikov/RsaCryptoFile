using CryptoFile.IO.Entities;

namespace CryptoFile.IO.Sorting.Comparers
{
	internal class LengthComparer : FileSystemEntityComparer
	{
		public LengthComparer(SortDirection direction) : base(direction)
		{
		}

		protected override int DoCompare(FileSystemEntity first, FileSystemEntity second)
		{
			return !first.IsFile ? 0 : first.Length.CompareTo(second.Length);
		}
	}
}