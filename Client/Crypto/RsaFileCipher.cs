using System;
using System.IO;
using CryptoFile.IO;
using CryptoFile.IO.Exceptions;
using CryptoFile.IO.Reading;
using CryptoFile.Library;
using CryptoFile.Library.Keys;

namespace CryptoFile.Client.Crypto {
	class RsaFileCipher : RsaCryptoFile, IRsaFileCipher {
		/// <exception cref="ArgumentNullException">factory is null</exception>
		public RsaFileCipher(IFileFactory factory) : base(factory) {}

		/// <exception cref="ArgumentNullException">any argument is null</exception>
		/// <exception cref="ArgumentException">sourceFileName is empty or destinationFileName is empty</exception>
		/// <exception cref="SourceFileNotFoundException">Исходный файл не найден.</exception>
		/// <exception cref="UnauthorizedAccessSourceFileException">Ошибка при доступе к исходному файлу.</exception>
		/// <exception cref="DestinationFileException">Ошибка при сохранении зашифрованного файла.</exception>
		public void Cipher(PublicKey key, string sourceFileName, string destinationFileName) {
			Status = ProcessStatus.Processing;
			CurrentBlock = 0;
			try {
				var rsa = new RsaCipher(key);
				using (var reader = CreateFileReader(sourceFileName, key)) {
					TotalBlocks = Convert.ToInt32(Math.Ceiling((double)reader.Length/(key.N.Digit - 1)));
					using (var writter = factory.CreateRsaFileWriter(destinationFileName, (short)key.N.Digit)) {
						writter.WriteHeader(reader.Length, reader.HashCode);
						while (!reader.IsDone && Status != ProcessStatus.Stopped) {
							var number = reader.ReadNextBlock();
							writter.WriteNextBlock(rsa.Cipher(number));
							++CurrentBlock;
							OnBlockCompleted();
						}
					}
				}
			} catch (Exception) {
				Status = ProcessStatus.Stopped;
				throw;
			}
			if (Status == ProcessStatus.Processing) {
				Status = ProcessStatus.Complete;
			}
		}

		/// <exception cref="ArgumentNullException">sourceFileName is null</exception>
		/// <exception cref="ArgumentException">sourceFileName is empty</exception>
		/// <exception cref="SourceFileNotFoundException">Исходный файл не найден.</exception>
		/// <exception cref="UnauthorizedAccessSourceFileException">Ошибка при доступе к исходному файлу.</exception>
		private IFileReader CreateFileReader(string sourceFileName, PublicKey key) {
			try {
				return factory.CreateFileReader(sourceFileName, (short)(key.N.Digit - 1));
			} catch (FileNotFoundException e) {
				throw new SourceFileNotFoundException("Исходный файл не найден.", e);
			} catch (UnauthorizedAccessException e) {
				throw new UnauthorizedAccessSourceFileException("Ошибка при доступе к исходному файлу.", e);
			}
		}
	}
}