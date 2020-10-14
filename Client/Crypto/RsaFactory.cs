using System.IO;
using CryptoFile.Client.Compression;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Environment;
using CryptoFile.IO;
using CryptoFile.IO.Exceptions;
using CryptoFile.Library;

namespace CryptoFile.Client.Crypto {
	public class RsaFactory : IRsaFactory {
		private readonly IFileFactory fileFactory;
		private readonly Options options;
		private readonly IEnvironmentHelper environmentHelper;
		private readonly IZipAlgorithm zipAlgorithm;

		public RsaFactory(IFileFactory fileFactory,
		                  Options options,
		                  IEnvironmentHelper environmentHelper,
		                  IZipAlgorithm zipAlgorithm) {
			this.fileFactory = fileFactory;
			this.options = options;
			this.environmentHelper = environmentHelper;
			this.zipAlgorithm = zipAlgorithm;
		}

		public IRsaFileCipher CreateRsaFileCipher() {
			var cipher = new RsaFileCipher(fileFactory);
			return options.Zip—ompression ? CreateRsaFileCipherZipDecorator(cipher) : cipher;
		}

		public IRsaFileDecipher CreateRsaFileDecipher(string fileName) {
			Checker.CheckString(fileName);
			var fileInfo = new FileInfo(fileName);
			if (!fileInfo.Exists)
				throw new SourceFileNotFoundException(fileName);
			if (fileInfo.Length == 0)
				throw new SourceFileException("file is empty.");
			var rsaFileDecipher = new RsaFileDecipher(fileFactory);
			using (var reader = fileInfo.OpenRead()) {
				if (reader.ReadByte() == 80)
					return new RsaFileDecipherZipDecorator(rsaFileDecipher, environmentHelper, zipAlgorithm);
			}
			return rsaFileDecipher;
		}

		private IRsaFileCipher CreateRsaFileCipherZipDecorator(IRsaFileCipher cipher) {
			return new RsaFileCipherZipDecorator(cipher, environmentHelper, zipAlgorithm);
		}
	}
}