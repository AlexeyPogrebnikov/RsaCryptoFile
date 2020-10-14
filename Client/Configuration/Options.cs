using System.Drawing;
using CryptoFile.IO.Sorting;

namespace CryptoFile.Client.Configuration {
	public class Options {
		public Options() {
			MinRsaKeyLength = 64;
			MaxRsaKeyLength = 4096;
			RsaKeyLength = 64;
			InitialSortDirection = SortDirection.Ascending;
			InitialSortColumn = SortColumn.Name;
			Language = Language.English;
			RsaFileColor = new ColorXml(Color.White);
			PublicExponent = 65537;
			ZipСompression = true;
		}

		public int MinRsaKeyLength { get; set; }
		public int MaxRsaKeyLength { get; set; }
		public int RsaKeyLength { get; set; }
		public string InitialDirectory { get; set; }
		public SortDirection InitialSortDirection { get; set; }
		public SortColumn InitialSortColumn { get; set; }
		public Language Language { get; set; }
		public ColorXml RsaFileColor { get; set; }
		public int PublicExponent { get; set; }
		public bool ZipСompression { get; set; }
	}
}