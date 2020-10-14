using System;
using System.IO;
using CryptoFile.IO.Reading;
using CryptoFile.IO.Writing;

namespace CryptoFile.IO {
	public interface IFileFactory {
		/// <exception cref="ArgumentNullException">fileName is null</exception>
		/// <exception cref="ArgumentException">
		/// 1. fileName is empty
		/// 2. length less or equal zero
		/// </exception>
		/// <exception cref="FileNotFoundException">file not found</exception>
		/// <exception cref="UnauthorizedAccessException">Запрета доступа операционной системой из-за ошибки ввода-вывода или особого типа ошибка безопасности.</exception>
		IFileReader CreateFileReader(string fileName, short blockLength);

		IRsaFileReader CreateRsaFileReader(string fileName);

		/// <exception cref="ArgumentNullException">fileName is null</exception>
		/// <exception cref="ArgumentException">
		/// 1. fileName is empty
		/// 2. blockLength less or equal zero
		/// </exception>
		/// <exception cref="UnauthorizedAccessException">Запрета доступа операционной системой из-за ошибки ввода-вывода или особого типа ошибка безопасности.</exception>
		IFileWriter CreateFileWriter(string fileName, short blockLength, long sourceFileLength);

		IRsaFileWriter CreateRsaFileWriter(string fileName, short blockLength);

		byte RsaVersion { get; }
	}
}