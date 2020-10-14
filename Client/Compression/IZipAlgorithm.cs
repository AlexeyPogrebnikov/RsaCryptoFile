namespace CryptoFile.Client.Compression {
	public interface IZipAlgorithm {
		void CompressFile(string sourceFileName, string destinationFileName);
		void DecompressFile(string sourceFileName, string destinationFileName);
	}
}