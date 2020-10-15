using System;
using System.Collections.Generic;
using System.IO;
using CryptoFile.Library;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.IO.Writing
{
	public class FileWriter : IFileWriter
	{
		private readonly int blockLength;
		private long remainder;
		private readonly FileStream stream;

		/// <exception cref="ArgumentNullException">fileName is null</exception>
		/// <exception cref="ArgumentException">
		/// 1. fileName is empty
		/// 2. blockLength less or equal zero
		/// </exception>
		/// <exception cref="UnauthorizedAccessException">
		/// Запрета доступа операционной системой из-за ошибки ввода-вывода или
		/// особого типа ошибка безопасности.
		/// </exception>
		public FileWriter(string fileName, int blockLength, long sourceFileLength)
		{
			if (blockLength <= 0)
			{
				throw new ArgumentException("blockLength <= 0");
			}

			this.blockLength = blockLength;
			remainder = sourceFileLength;
			stream = new FileStream(fileName, FileMode.Create);
		}

		#region IFileWriter Members

		public void WriteNextBlock(BigNumber number)
		{
			Checker.CheckNull(number);
			if (number.Digit > blockLength)
			{
				throw new ArgumentException("number.Digit > length");
			}

			byte[] bytes = remainder < blockLength ? new byte[remainder] : new byte[blockLength];
			remainder -= blockLength;
			IList<int> numbers = number.Numbers;
			for (var i = 0; i < numbers.Count; ++i)
			{
				bytes[i] = (byte) numbers[i];
			}

			stream.Write(bytes, 0, bytes.Length);
		}

		public void Delete()
		{
			Dispose();
			File.Delete(stream.Name);
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			stream.Dispose();
		}

		#endregion
	}
}