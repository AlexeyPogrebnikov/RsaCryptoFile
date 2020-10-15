using System;
using System.IO;
using System.Security.Cryptography;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.IO.Reading
{
	/// <summary>
	/// Осуществляет чтение из любых файлов
	/// </summary>
	public class FileReader : IFileReader
	{
		private readonly short blockLength;
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
		public FileReader(string fileName, short blockLength)
		{
			if (blockLength <= 0)
			{
				throw new ArgumentException("blockLength <= 0");
			}

			this.blockLength = blockLength;
			stream = new FileStream(fileName, FileMode.Open);
			stream.Position = 0;
			var md5 = new MD5CryptoServiceProvider();
			HashCode = md5.ComputeHash(stream);
			stream.Seek(0, SeekOrigin.Begin);
		}

		#region IFileReader Members

		public BigNumber ReadNextBlock()
		{
			if (IsDone)
			{
				throw new EndOfStreamException("IsDone is true");
			}

			var bytes = new byte[blockLength];
			int index = stream.Read(bytes, 0, blockLength) - 1;
			while (index > 0 && bytes[index] == 0)
			{
				--index;
			}

			var numbers = new int[index + 1];
			for (var i = 0; i < numbers.Length; ++i)
			{
				numbers[i] = bytes[i];
			}

			return BigNumber.FromBytes(numbers);
		}

		public void Dispose()
		{
			stream.Dispose();
		}

		public long Length => stream.Length;

		public byte[] HashCode { get; private set; }

		public bool IsDone => stream.Length <= stream.Position;

		#endregion
	}
}