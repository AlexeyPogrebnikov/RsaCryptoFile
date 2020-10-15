using CryptoFile.IO.Sorting.Comparers;

namespace CryptoFile.IO.Sorting
{
	public class FileSorterByName : FileSorter
	{
		public FileSorterByName()
		{
		}

		public FileSorterByName(SortDirection direction) : base(direction)
		{
		}

		protected override SortColumn GetSortColumn()
		{
			return SortColumn.Name;
		}

		protected override FileSystemEntityComparer CreateComparer(SortDirection direction)
		{
			return new NameComparer(direction);
		}
	}
}