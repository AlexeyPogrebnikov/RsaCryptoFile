using System;

namespace CryptoFile.Client.Crypto
{
	public interface IRsaCryptoFile
	{
		event EventHandler BlockCompleted;
		void Stop();
		void Restart();
		ProcessStatus Status { get; }
		int TotalBlocks { get; }
		int CurrentBlock { get; }
	}
}