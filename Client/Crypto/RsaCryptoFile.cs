using System;
using CryptoFile.IO;
using CryptoFile.Library;

namespace CryptoFile.Client.Crypto
{
	internal class RsaCryptoFile : IRsaCryptoFile
	{
		protected readonly IFileFactory factory;

		/// <exception cref="ArgumentNullException">factory is null</exception>
		public RsaCryptoFile(IFileFactory factory)
		{
			Checker.CheckNull(factory);
			this.factory = factory;
		}

		public event EventHandler BlockCompleted;

		public void Stop()
		{
			Status = ProcessStatus.Stopped;
		}

		public void Restart()
		{
			Status = ProcessStatus.NotBeginning;
		}

		public ProcessStatus Status { get; protected set; }
		public int TotalBlocks { get; protected set; }
		public int CurrentBlock { get; protected set; }

		protected void OnBlockCompleted()
		{
			if (BlockCompleted != null)
			{
				BlockCompleted(this, EventArgs.Empty);
			}
		}
	}
}