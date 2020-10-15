using System;
using System.Threading;
using System.Windows.Forms;
using CryptoFile.Client.Commands;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Forms;
using CryptoFile.Client.Management;
using CryptoFile.Client.Serialization;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Unification;
using CryptoFile.Library;
using CryptoFile.Library.Keys;

namespace CryptoFile.Client.Presenters
{
	internal class DecipherFormPresenter
	{
		private readonly IDecipherForm form;
		private readonly IRsaFactory rsaFactory;
		private IRsaFileDecipher rsaFileDecipher;
		private readonly KeySerializer keySerializer;
		private readonly ICommandsContainer commandsContainer;
		private readonly IEnvironmentHelper environmentHelper;
		private readonly IMessageHelper messageHelper;
		private readonly IFileUnifier fileUnifier;
		private PrivateKey privateKey;
		private Thread thread;

		/// <exception cref="ArgumentNullException">any argument is null</exception>
		public DecipherFormPresenter(IDecipherForm form,
			IRsaFactory rsaFactory,
			KeySerializer keySerializer,
			ICommandsContainer commandsContainer,
			FileSystemEntity initialFile,
			IEnvironmentHelper environmentHelper,
			IMessageHelper messageHelper,
			IFileUnifier fileUnifier)
		{
			Checker.CheckNull(form, rsaFactory, keySerializer, commandsContainer, initialFile);
			this.form = form;
			this.rsaFactory = rsaFactory;
			this.keySerializer = keySerializer;
			this.commandsContainer = commandsContainer;
			this.environmentHelper = environmentHelper;
			this.messageHelper = messageHelper;
			this.fileUnifier = fileUnifier;
			form.OutputDirectoryPath = GetOutputDirectoryPath(initialFile);
			form.Decipher += form_Decipher;
			form.CancelDecipher += form_CancelDecipher;
			form.OutputFileNameChanged += form_OutputFileNameChanged;
			form.PrivateKeyChanged += form_PrivateKeyChanged;
		}

		private static string GetOutputDirectoryPath(FileSystemEntity initialFile)
		{
			string fullName = initialFile.FullName;
			return fullName.Remove(fullName.Length - initialFile.Extension.Length);
		}

		private void form_CancelDecipher(object sender, EventArgs e)
		{
			if (rsaFileDecipher != null && rsaFileDecipher.Status == ProcessStatus.Processing)
			{
				rsaFileDecipher.Stop();
				RefreshThread();
			}
			else
			{
				form.DialogResult = DialogResult.Cancel;
			}
		}

		private void decipher_BlockCompleted(object sender, EventArgs e)
		{
			double percent = 100 * rsaFileDecipher.CurrentBlock;
			percent /= rsaFileDecipher.TotalBlocks;
			form.ProgressPercent = Convert.ToInt32(percent);
		}

		private void form_Decipher(object sender, EventArgs e)
		{
			if (environmentHelper.DirectoryExists(form.OutputDirectoryPath))
			{
				DialogResult dialogResult = messageHelper.Show("Directory already exists. Would you like to overwrite it?",
					"Папка уже существует. Вы хотите перезаписать ее?", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.No)
					return;
			}

			try
			{
				privateKey = keySerializer.DeserializePrivateKey(form.PrivateKey);
			}
			catch (KeySerializationException)
			{
				messageHelper.Show("Private key has errors.", "Секретный ключ содержит ошибки.");
				return;
			}

			rsaFileDecipher = rsaFactory.CreateRsaFileDecipher(form.InputFileName);
			rsaFileDecipher.BlockCompleted += decipher_BlockCompleted;
			form.DecipherEnabled = false;
			RefreshThread();
			thread.Start();
		}

		private void StartDecipher()
		{
			var manager = new DecipherManager(environmentHelper, rsaFileDecipher, messageHelper, fileUnifier);
			manager.Decipher(privateKey, form.InputFileName, form.OutputDirectoryPath);
			if (rsaFileDecipher.Status == ProcessStatus.Complete)
			{
				if (form.CloseWindowAfterComlete)
				{
					form.DialogResult = DialogResult.Cancel;
				}
			}

			commandsContainer.RefreshDirectoryCommand.Execute();
			form.ProgressPercent = 0;
			form.DecipherEnabled = true;
		}

		private void form_OutputFileNameChanged(object sender, EventArgs e)
		{
			UpdateForm();
		}

		private void form_PrivateKeyChanged(object sender, EventArgs e)
		{
			UpdateForm();
		}

		private void UpdateForm()
		{
			if (string.IsNullOrEmpty(form.InputFileName))
			{
				form.DecipherEnabled = false;
				return;
			}

			if (string.IsNullOrEmpty(form.InputFileName))
			{
				form.DecipherEnabled = false;
				return;
			}

			if (string.IsNullOrEmpty(form.PrivateKey))
			{
				form.DecipherEnabled = false;
				return;
			}

			form.DecipherEnabled = true;
		}

		private void RefreshThread()
		{
			thread = new Thread(StartDecipher);
		}
	}
}