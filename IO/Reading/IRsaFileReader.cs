namespace CryptoFile.IO.Reading {
	public interface IRsaFileReader : IFileReader {
		void ReadHeader();
		byte Version { get; }
		short BlockLength { get; }
		long SourceFileLength { get; }
	}
}