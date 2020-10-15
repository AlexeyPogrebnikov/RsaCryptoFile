using CryptoFile.IO.Reading;
using CryptoFile.IO.Writing;

namespace CryptoFile.IO
{
	public class FileFactory : IFileFactory
	{
		public FileFactory(byte rsaVersion)
		{
			RsaVersion = rsaVersion;
		}

		#region IFileFactory Members

		public IFileReader CreateFileReader(string fileName, short blockLength)
		{
			return new FileReader(fileName, blockLength);
		}

		public IRsaFileReader CreateRsaFileReader(string fileName)
		{
			return new RsaFileReader(fileName);
		}

		public IFileWriter CreateFileWriter(string fileName, short blockLength, long sourceFileLength)
		{
			return new FileWriter(fileName, blockLength, sourceFileLength);
		}

		public IRsaFileWriter CreateRsaFileWriter(string fileName, short blockLength)
		{
			return new RsaFileWriter(RsaVersion, fileName, blockLength);
		}

		public byte RsaVersion { get; private set; }

		#endregion
	}
}