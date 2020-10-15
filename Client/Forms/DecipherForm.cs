using System;
using System.Windows.Forms;
using CryptoFile.Client.Configuration;

namespace CryptoFile.Client.Forms
{
	internal partial class DecipherForm : Form, IDecipherForm
	{
		private Language language;

		public DecipherForm()
		{
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;
		}

		#region IDecipherForm Members

		public event EventHandler Decipher;

		public event EventHandler CancelDecipher;

		public event EventHandler OutputFileNameChanged;

		public event EventHandler PrivateKeyChanged;

		public string InputFileName
		{
			get => inputFileTextBox.Text;
			set => inputFileTextBox.Text = value;
		}

		public string OutputDirectoryPath
		{
			get => outputDirectoryPathTextBox.Text;
			set => outputDirectoryPathTextBox.Text = value;
		}

		public string PrivateKey
		{
			get => privateKeyTextBox.Text;
			set => privateKeyTextBox.Text = value;
		}

		public bool DecipherEnabled
		{
			get => decryptButton.Enabled;
			set => decryptButton.Enabled = value;
		}

		public int ProgressPercent
		{
			get => progressBar.Value;
			set => progressBar.Value = value;
		}

		public bool CloseWindowAfterComlete
		{
			get => closeWindowCheckBox.Checked;
			set => closeWindowCheckBox.Checked = value;
		}

		public Language Language
		{
			get => language;
			set
			{
				language = value;
				if (language == Language.English)
				{
					Text = @"Decipher";
					inputFileLabel.Text = @"Input file:";
					outputDirectoryPathLabel.Text = @"Output path:";
					privateKeyLabel.Text = @"Private key:";
					progressLabel.Text = @"Progress:";
					closeWindowCheckBox.Text = @"Close window after complete";
					decryptButton.Text = @"Decrypt";
					cancelButton.Text = @"Cancel";
				}

				if (language == Language.Russian)
				{
					Text = @"Расшифрование";
					inputFileLabel.Text = @"Входной файл:";
					outputDirectoryPathLabel.Text = @"Выходная папка:";
					privateKeyLabel.Text = @"Секретный ключ:";
					progressLabel.Text = @"Прогресс:";
					closeWindowCheckBox.Text = @"Закрыть окно после завершения";
					decryptButton.Text = @"Расшифровать";
					cancelButton.Text = @"Отмена";
				}
			}
		}

		#endregion

		private void outputFileButton_Click(object sender, EventArgs e)
		{
			folderBrowserDialog.SelectedPath = OutputDirectoryPath;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				OutputDirectoryPath = folderBrowserDialog.SelectedPath;
			}
		}

		private void decipherButton_Click(object sender, EventArgs e)
		{
			if (Decipher != null)
			{
				Decipher(this, e);
			}
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			if (CancelDecipher != null)
			{
				CancelDecipher(this, e);
			}
		}

		private void outputFileTextBox_TextChanged(object sender, EventArgs e)
		{
			if (OutputFileNameChanged != null)
			{
				OutputFileNameChanged(this, e);
			}
		}

		private void privateKeyTextBox_TextChanged(object sender, EventArgs e)
		{
			if (PrivateKeyChanged != null)
			{
				PrivateKeyChanged(this, e);
			}
		}
	}
}