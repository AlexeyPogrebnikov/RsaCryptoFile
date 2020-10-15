using System;
using System.Collections.Generic;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Environment;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Exceptions;
using CryptoFile.IO.Unification;
using CryptoFile.Library.Keys;

namespace CryptoFile.Client.Management
{
	internal class CipherManager
	{
		private readonly IRsaFileCipher rsaFileCipher;
		private readonly IFileUnifier fileUnifier;
		private readonly IEnvironmentHelper environmentHelper;
		private readonly IMessageHelper messageHelper;

		public CipherManager(IRsaFileCipher rsaFileCipher,
			IFileUnifier fileUnifier,
			IEnvironmentHelper environmentHelper,
			IMessageHelper messageHelper)
		{
			this.rsaFileCipher = rsaFileCipher;
			this.fileUnifier = fileUnifier;
			this.environmentHelper = environmentHelper;
			this.messageHelper = messageHelper;
		}

		public void Cipher(PublicKey publicKey, IList<FileSystemEntity> inputFileEntities, string outputFileName)
		{
			if (inputFileEntities.Count == 0)
				throw new ArgumentException(@"inputFileEntities is empty", "inputFileEntities");
			string temporaryFileName = environmentHelper.GetTempFileName();
			try
			{
				fileUnifier.Union(inputFileEntities, temporaryFileName);
				rsaFileCipher.Cipher(publicKey, temporaryFileName, outputFileName);
				if (rsaFileCipher.Status == ProcessStatus.Stopped)
				{
					messageHelper.Show("Process was stopped.", "Процесс был остановлен.");
					rsaFileCipher.Restart();
				}

				if (rsaFileCipher.Status == ProcessStatus.Complete)
				{
					messageHelper.Show("Process complete.", "Процесс завершен.");
				}
			}
			catch (SourceFileNotFoundException)
			{
				messageHelper.Show("Source file not found.", "Исходный файл не найден.");
			}
			catch (UnauthorizedAccessSourceFileException)
			{
				messageHelper.Show("Error when accessing the source file.", "Ошибка доступа к исходному файлу.");
			}
			catch (DestinationFileException)
			{
				messageHelper.Show("Error writing RSA file.", "Ошибка при записи RSA файла");
			}
			finally
			{
				environmentHelper.DeleteFile(temporaryFileName);
			}
		}
	}
}