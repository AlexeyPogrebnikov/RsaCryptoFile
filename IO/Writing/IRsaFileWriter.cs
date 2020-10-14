namespace CryptoFile.IO.Writing {
	public interface IRsaFileWriter : IFileWriter {
		void WriteHeader(long sourceFileLength, byte[] hashCode);
	}
}