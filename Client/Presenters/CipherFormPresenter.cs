using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using CryptoFile.Client.Commands;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Forms;
using CryptoFile.Client.Management;
using CryptoFile.Client.Serialization;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Unification;
using CryptoFile.Library;
using CryptoFile.Library.Keys;

namespace CryptoFile.Client.Presenters {
	class CipherFormPresenter {
		private IRsaFileCipher rsaFileCipher;
		private readonly ICipherForm cipherForm;
		private readonly IList<FileSystemEntity> inputFileEntities;
		private readonly IRsaFactory rsaFactory;
		private readonly KeySerializer keySerializer;
		private readonly ICommandsContainer commandsContainer;
		private readonly IFileUnifier fileUnifier;
		private readonly IEnvironmentHelper environmentHelper;
		private readonly IMessageHelper messageHelper;
		private readonly Options options;
		private Thread thread;
		private PublicKey publicKey;

		/// <exception cref="ArgumentNullException">any argument is null</exception>
		public CipherFormPresenter(ICipherForm cipherForm,
		                           IList<FileSystemEntity> inputFileEntities,
		                           IRsaFactory rsaFactory,
		                           KeySerializer keySerializer,
		                           ICommandsContainer commandsContainer,
		                           IFileUnifier fileUnifier,
		                           IEnvironmentHelper environmentHelper,
		                           IMessageHelper messageHelper,
		                           Options options) {
			Checker.CheckNull(cipherForm, keySerializer, commandsContainer);
			this.cipherForm = cipherForm;
			this.inputFileEntities = inputFileEntities;
			this.rsaFactory = rsaFactory;
			this.keySerializer = keySerializer;
			this.commandsContainer = commandsContainer;
			this.fileUnifier = fileUnifier;
			this.environmentHelper = environmentHelper;
			this.messageHelper = messageHelper;
			this.options = options;
			cipherForm.InputFileEntities = inputFileEntities;
			cipherForm.TotalLength = GetTotalLength();
			cipherForm.OutputFileName = GetOutputFileName();
			cipherForm.ZipСompression = options.ZipСompression;
			cipherForm.Cipher += cipherForm_Cipher;
			cipherForm.CancelCipher += cipherForm_CancelCipher;
			cipherForm.OutputFileNameChanged += cipherForm_OutputFileNameChanged;
			cipherForm.PublicKeyChanged += cipherForm_PublicKeyChanged;
		}

		public void ShowDialog() {
			cipherForm.ShowDialog();
		}

		private long GetTotalLength() {
			long length = 0;
			foreach (var entity in inputFileEntities) {
				length += entity.Length;
			}
			return length;
		}

		private string GetOutputFileName() {
			string fullName;
			if (inputFileEntities.Count > 1) {
				var directory = inputFileEntities[0].GetParentDirectory();
				fullName = directory.FullName;
				fullName = string.Format("{0}\\{1}", fullName, directory.Name);
			} else {
				var entity = inputFileEntities[0];
				fullName = entity.FullName;
				if (entity.IsFile) {
					fullName = fullName.Remove(entity.FullName.Length - entity.Extension.Length);
				}
			}
			return string.Format("{0}.rsa", fullName);
		}

		private void cipherForm_CancelCipher(object sender, EventArgs e) {
			if (rsaFileCipher != null && rsaFileCipher.Status == ProcessStatus.Processing) {
				rsaFileCipher.Stop();
				RefreshThread();
			} else {
				cipherForm.DialogResult = DialogResult.Cancel;
			}
		}

		private void cipher_BlockCompleted(object sender, EventArgs e) {
			double percent = 100*rsaFileCipher.CurrentBlock;
			percent /= rsaFileCipher.TotalBlocks;
			cipherForm.ProgressPercent = Convert.ToInt32(percent);
		}

		private void cipherForm_Cipher(object sender, EventArgs e) {
			if (environmentHelper.FileExists(cipherForm.OutputFileName)) {
				var dialogResult = messageHelper.Show("RSA file already exists. Would you like to overwrite it?",
				                                      "RSA файл уже существует. Вы действительно хотите перезаписать его?",
				                                      MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.No)
					return;
			}
			cipherForm.CipherEnabled = false;
			try {
				publicKey = keySerializer.DeserializePublicKey(cipherForm.PublicKey);
			} catch (KeySerializationException) {
				messageHelper.Show("Public key has errors.", "Открытый ключ имеет ошибки.");
				return;
			}
			options.ZipСompression = cipherForm.ZipСompression;
			if (rsaFileCipher != null) {
				rsaFileCipher.BlockCompleted -= cipher_BlockCompleted;
			}
			rsaFileCipher = rsaFactory.CreateRsaFileCipher();
			rsaFileCipher.BlockCompleted += cipher_BlockCompleted;
			RefreshThread();
			thread.Start();
		}

		private void StartCipher() {
			var manager = new CipherManager(rsaFileCipher, fileUnifier, environmentHelper, messageHelper);
			manager.Cipher(publicKey, inputFileEntities, cipherForm.OutputFileName);
			if (rsaFileCipher.Status == ProcessStatus.Complete) {
				if (cipherForm.CloseWindowAfterComlete) {
					cipherForm.DialogResult = DialogResult.Cancel;
				}
			}
			cipherForm.ProgressPercent = 0;
			cipherForm.CipherEnabled = true;
			commandsContainer.RefreshDirectoryCommand.Execute();
		}

		private void cipherForm_OutputFileNameChanged(object sender, EventArgs e) {
			UpdateForm();
		}

		private void cipherForm_PublicKeyChanged(object sender, EventArgs e) {
			UpdateForm();
		}

		private void UpdateForm() {
			if (string.IsNullOrEmpty(cipherForm.PublicKey)) {
				cipherForm.CipherEnabled = false;
				return;
			}
			cipherForm.CipherEnabled = true;
		}

		private void RefreshThread() {
			thread = new Thread(StartCipher);
		}
	}
}