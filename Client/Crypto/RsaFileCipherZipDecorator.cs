using System;
using CryptoFile.Client.Compression;
using CryptoFile.Client.Environment;
using CryptoFile.Library.Keys;

namespace CryptoFile.Client.Crypto
{
	public class RsaFileCipherZipDecorator : IRsaFileCipher
	{
		private readonly IRsaFileCipher rsaFileCipher;
		private readonly IEnvironmentHelper environmentHelper;
		private readonly IZipAlgorithm zipAlgorithm;

		public RsaFileCipherZipDecorator(IRsaFileCipher rsaFileCipher,
			IEnvironmentHelper environmentHelper,
			IZipAlgorithm zipAlgorithm)
		{
			this.rsaFileCipher = rsaFileCipher;
			this.environmentHelper = environmentHelper;
			this.zipAlgorithm = zipAlgorithm;
			rsaFileCipher.BlockCompleted += rsaFileCipher_BlockCompleted;
		}

		public void Cipher(PublicKey key, string sourceFileName, string destinationFileName)
		{
			string temporaryFile = environmentHelper.GetTempFileName();
			try
			{
				rsaFileCipher.Cipher(key, sourceFileName, temporaryFile);
				zipAlgorithm.CompressFile(temporaryFile, destinationFileName);
			}
			finally
			{
				environmentHelper.DeleteFile(temporaryFile);
			}
		}

		public event EventHandler BlockCompleted;

		public void Stop()
		{
			rsaFileCipher.Stop();
		}

		public void Restart()
		{
			rsaFileCipher.Restart();
		}

		public ProcessStatus Status => rsaFileCipher.Status;

		public int TotalBlocks => rsaFileCipher.TotalBlocks;

		public int CurrentBlock => rsaFileCipher.CurrentBlock;

		private void rsaFileCipher_BlockCompleted(object sender, EventArgs e)
		{
			if (BlockCompleted != null)
				BlockCompleted(this, e);
		}
	}
}