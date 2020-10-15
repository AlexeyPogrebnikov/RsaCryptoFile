namespace CryptoFile.IO.Sorting
{
	public class SortingInfo
	{
		public SortingInfo(SortColumn sortColumn, SortDirection sortDirection)
		{
			SortColumn = sortColumn;
			SortDirection = sortDirection;
		}

		public SortColumn SortColumn { get; private set; }

		public SortDirection SortDirection { get; private set; }
	}
}