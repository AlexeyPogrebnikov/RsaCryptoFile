using System;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.IO.Writing {
	public interface IFileWriter : IDisposable {
		/// <exception cref="ArgumentNullException">number is null</exception>
		/// <exception cref="ArgumentException">number.Digit > length</exception>
		void WriteNextBlock(BigNumber number);

		void Delete();
	}
}