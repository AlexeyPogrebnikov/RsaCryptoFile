using System;
using System.IO;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.IO.Reading
{
	public interface IFileReader : IDisposable
	{
		/// <exception cref="EndOfStreamException">IsDone is true</exception>
		BigNumber ReadNextBlock();

		bool IsDone { get; }
		long Length { get; }
		byte[] HashCode { get; }
	}
}