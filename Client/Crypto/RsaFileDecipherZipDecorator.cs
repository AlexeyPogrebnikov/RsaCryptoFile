using System;
using CryptoFile.Client.Compression;
using CryptoFile.Client.Environment;
using CryptoFile.Library.Keys;

namespace CryptoFile.Client.Crypto {
	class RsaFileDecipherZipDecorator : IRsaFileDecipher {
		private readonly IRsaFileDecipher rsaFileDecipher;
		private readonly IEnvironmentHelper environmentHelper;
		private readonly IZipAlgorithm zipAlgorithm;

		public RsaFileDecipherZipDecorator(IRsaFileDecipher rsaFileDecipher,
		                                   IEnvironmentHelper environmentHelper,
		                                   IZipAlgorithm zipAlgorithm) {
			this.rsaFileDecipher = rsaFileDecipher;
			this.environmentHelper = environmentHelper;
			this.zipAlgorithm = zipAlgorithm;
			rsaFileDecipher.BlockCompleted += rsaFileDecipher_BlockCompleted;
		}

		public event EventHandler BlockCompleted;

		public void Stop() {
			rsaFileDecipher.Stop();
		}

		public void Restart() {
			rsaFileDecipher.Restart();
		}

		public ProcessStatus Status {
			get { return rsaFileDecipher.Status; }
		}

		public int TotalBlocks {
			get { return rsaFileDecipher.TotalBlocks; }
		}

		public int CurrentBlock {
			get { return rsaFileDecipher.CurrentBlock; }
		}

		public void Decipher(PrivateKey key, string sourceFileName, string destinationFileName) {
			var temporaryFileName = environmentHelper.GetTempFileName();
			try {
				zipAlgorithm.DecompressFile(sourceFileName, temporaryFileName);
				rsaFileDecipher.Decipher(key, temporaryFileName, destinationFileName);
			} finally {
				environmentHelper.DeleteFile(temporaryFileName);
			}
		}

		private void rsaFileDecipher_BlockCompleted(object sender, EventArgs e) {
			if (BlockCompleted != null)
				BlockCompleted(this, e);
		}
	}
}