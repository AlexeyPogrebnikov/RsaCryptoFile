using System.IO;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Environment;
using CryptoFile.IO.Exceptions;
using CryptoFile.IO.Unification;
using CryptoFile.Library.Keys;

namespace CryptoFile.Client.Management {
	public class DecipherManager {
		private readonly IEnvironmentHelper environmentHelper;
		private readonly IRsaFileDecipher rsaFileDecipher;
		private readonly IMessageHelper messageHelper;
		private readonly IFileUnifier fileUnifier;

		public DecipherManager(IEnvironmentHelper environmentHelper,
		                       IRsaFileDecipher rsaFileDecipher,
		                       IMessageHelper messageHelper,
		                       IFileUnifier fileUnifier) {
			this.environmentHelper = environmentHelper;
			this.rsaFileDecipher = rsaFileDecipher;
			this.messageHelper = messageHelper;
			this.fileUnifier = fileUnifier;
		}

		public void Decipher(PrivateKey privateKey, string inputFileName, string outputDirectoryPath) {
			var temporaryFileName = environmentHelper.GetTempFileName();
			try {
				if (!Directory.Exists(outputDirectoryPath))
					Directory.CreateDirectory(outputDirectoryPath);
				rsaFileDecipher.Decipher(privateKey, inputFileName, temporaryFileName);
				if (rsaFileDecipher.Status == ProcessStatus.Stopped) {
					messageHelper.Show("Process was stopped.", "Процесс был остановлен.");
					rsaFileDecipher.Restart();
					return;
				}
				fileUnifier.Split(temporaryFileName, outputDirectoryPath);
				if (rsaFileDecipher.Status == ProcessStatus.Complete) {
					messageHelper.Show("Process complete.", "Процесс завершен.");
				}
			} catch (IncorrectPrivateKeyException) {
				messageHelper.Show("Incorrect private key.", "Неправильный закрытый ключ.");
			} catch (TooHighVersionException) {
				messageHelper.Show("To decrypt a file needs a new program.", "Для расшифровки файла нужна более новая программа.");
			} catch (UnauthorizedAccessDestinationFileException) {
				messageHelper.Show(@"Error writing destination file.", "Ошибка при записи файла.");
			} catch {
				messageHelper.Show("Error decipher RSA file.", "Ошибка при расшифровке RSA файла.");
			} finally {
				environmentHelper.DeleteFile(temporaryFileName);
			}
		}
	}
}