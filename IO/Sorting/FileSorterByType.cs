using CryptoFile.IO.Sorting.Comparers;

namespace CryptoFile.IO.Sorting
{
	public class FileSorterByType : FileSorter
	{
		public FileSorterByType()
		{
		}

		public FileSorterByType(SortDirection direction) : base(direction)
		{
		}

		protected override SortColumn GetSortColumn()
		{
			return SortColumn.Type;
		}

		protected override FileSystemEntityComparer CreateComparer(SortDirection direction)
		{
			return new TypeComparer(direction);
		}
	}
}