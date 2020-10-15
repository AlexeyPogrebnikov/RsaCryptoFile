using System;
using CryptoFile.IO;
using CryptoFile.IO.Exceptions;
using CryptoFile.IO.Reading;
using CryptoFile.IO.Writing;
using CryptoFile.Library;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Client.Crypto
{
	internal class RsaFileDecipher : RsaCryptoFile, IRsaFileDecipher
	{
		/// <exception cref="ArgumentNullException">factory is null</exception>
		public RsaFileDecipher(IFileFactory factory) : base(factory)
		{
		}

		/// <exception cref="ArgumentNullException">any argument is null</exception>
		/// <exception cref="ArgumentException">destinationFileName is empty</exception>
		/// <exception cref="UnauthorizedAccessDestinationFileException">Ошибка при доступе к файлу назначения.</exception>
		/// <exception cref="DestinationFileException">Ошибка при сохранении расшифрованного файла.</exception>
		/// <exception cref="TooHighVersionException">Слишком высокая версия</exception>
		public void Decipher(PrivateKey key, string sourceFileName, string destinationFileName)
		{
			Checker.CheckNull(key, sourceFileName, destinationFileName);
			CurrentBlock = 0;
			var rsa = new RsaDecipher(key);
			byte[] expectedHash;
			Status = ProcessStatus.Processing;
			try
			{
				using (IRsaFileReader reader = factory.CreateRsaFileReader(sourceFileName))
				{
					reader.ReadHeader();
					expectedHash = reader.HashCode;
					if (reader.Version > factory.RsaVersion)
						throw new TooHighVersionException("Version is too high.");
					SetTotalBlocks(key, reader);
					using (IFileWriter writer = CreateFileWriter(destinationFileName, reader))
					{
						while (!reader.IsDone && Status != ProcessStatus.Stopped)
						{
							BigNumber number = reader.ReadNextBlock();
							writer.WriteNextBlock(rsa.Decipher(number));
							++CurrentBlock;
							OnBlockCompleted();
						}
					}
				}
			}
			catch
			{
				Status = ProcessStatus.Stopped;
				throw;
			}

			if (Status == ProcessStatus.Processing)
			{
				Status = ProcessStatus.Complete;
			}

			if (Status == ProcessStatus.Stopped)
				return;
			using (IFileReader reader = factory.CreateFileReader(destinationFileName, 1))
			{
				for (var i = 0; i < expectedHash.Length; ++i)
				{
					if (expectedHash[i] != reader.HashCode[i])
					{
						Status = ProcessStatus.Stopped;
						throw new IncorrectPrivateKeyException("Private key is incorrect: hash codes do not match.");
					}
				}
			}
		}

		private void SetTotalBlocks(PrivateKey key, IRsaFileReader reader)
		{
			TotalBlocks = (int) Math.Ceiling((double) reader.SourceFileLength / (key.N.Digit - 1));
		}

		/// <exception cref="ArgumentNullException">fileName is null</exception>
		/// <exception cref="ArgumentException">
		/// 1. fileName is empty
		/// 2. blockLength less or equal zero
		/// </exception>
		/// <exception cref="UnauthorizedAccessDestinationFileException">Ошибка при доступе к файлу назначения.</exception>
		private IFileWriter CreateFileWriter(string fileName, IRsaFileReader reader)
		{
			try
			{
				return factory.CreateFileWriter(fileName, (short) (reader.BlockLength - 1), reader.SourceFileLength);
			}
			catch (UnauthorizedAccessException e)
			{
				Stop();
				throw new UnauthorizedAccessDestinationFileException("Ошибка при доступе к файлу назначения.", e);
			}
		}
	}
}