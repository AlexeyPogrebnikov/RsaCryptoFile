using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CryptoFile.Client.Configuration;
using CryptoFile.IO.Entities;

namespace CryptoFile.Client.Forms {
	partial class CipherForm : Form, ICipherForm {
		private IEnumerable<FileSystemEntity> fileEntities;
		private Language language;
		private long totalLength;

		public CipherForm() {
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;
		}

		#region ICipherForm Members

		public event EventHandler Cipher;

		public event EventHandler CancelCipher;

		public event EventHandler OutputFileNameChanged;

		public event EventHandler PublicKeyChanged;

		public IEnumerable<FileSystemEntity> InputFileEntities {
			get { return fileEntities; }
			set {
				fileEntities = value;
				foreach (var fileEntity in value) {
					var images = selectedFileEntitiesListView.SmallImageList.Images;
					images.Add(fileEntity.Icon);
					var item = new ListViewItem(fileEntity.Name);
					item.ImageIndex = images.Count - 1;
					item.SubItems.Add(fileEntity.Length.ToString());
					selectedFileEntitiesListView.Items.Add(item);
				}
			}
		}

		public long TotalLength {
			get { return totalLength; }
			set {
				totalLength = value;
				RefreshTotalLength();
			}
		}

		public string OutputFileName {
			get { return outputFileTextBox.Text; }
			set { outputFileTextBox.Text = value; }
		}

		public string PublicKey {
			get { return publicKeyTextBox.Text; }
			set { publicKeyTextBox.Text = value; }
		}

		public bool CipherEnabled {
			get { return cipherButton.Enabled; }
			set { cipherButton.Enabled = value; }
		}

		public int ProgressPercent {
			get { return progressBar.Value; }
			set { progressBar.Value = value; }
		}

		public bool CloseWindowAfterComlete {
			get { return closeWindowCheckBox.Checked; }
			set { closeWindowCheckBox.Checked = value; }
		}

		public bool ZipСompression {
			get { return zipСompressionСheckBox.Checked; }
			set { zipСompressionСheckBox.Checked = value; }
		}

		#endregion

		public Language Language {
			get { return language; }
			set {
				language = value;
				if (language == Language.English) {
					Text = @"Encryption";
					selectedFileEntitiesLabel.Text = @"Selected files and folders:";
					nameColumnHeader.Text = @"Name";
					lengthColumnHeader.Text = @"Length";
					RefreshTotalLength();
					outputFileLabel.Text = @"Output file:";
					publicKeyLabel.Text = @"Public key:";
					progressLabel.Text = @"Progress:";
					closeWindowCheckBox.Text = @"Close window after complete";
					zipСompressionСheckBox.Text = @"Zip compression";
					cipherButton.Text = @"Cipher";
					cancelButton.Text = @"Cancel";
				}
				if (language == Language.Russian) {
					Text = @"Шифрование";
					selectedFileEntitiesLabel.Text = @"Выбранные файлы и папки:";
					nameColumnHeader.Text = @"Имя";
					lengthColumnHeader.Text = @"Размер";
					RefreshTotalLength();
					outputFileLabel.Text = @"Выходной файл:";
					publicKeyLabel.Text = @"Публичный ключ:";
					progressLabel.Text = @"Прогресс:";
					closeWindowCheckBox.Text = @"Закрыть окно после завершения";
					zipСompressionСheckBox.Text = @"Zip сжатие";
					cipherButton.Text = @"Шифровать";
					cancelButton.Text = @"Отмена";
				}
			}
		}

		private void cipherButton_Click(object sender, EventArgs e) {
			if (Cipher != null) {
				Cipher(this, e);
			}
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			if (CancelCipher != null) {
				CancelCipher(this, e);
			}
		}

		private void outputFileTextBox_TextChanged(object sender, EventArgs e) {
			if (OutputFileNameChanged != null) {
				OutputFileNameChanged(this, e);
			}
		}

		private void publicKeyTextBox_TextChanged(object sender, EventArgs e) {
			if (PublicKeyChanged != null) {
				PublicKeyChanged(this, e);
			}
		}

		private void outputFileButton_Click(object sender, EventArgs e) {
			if (saveFileDialog.ShowDialog() == DialogResult.OK) {
				outputFileTextBox.Text = saveFileDialog.FileName;
			}
		}

		private void RefreshTotalLength() {
			if (Language == Language.English) {
				totalLengthLabel.Text = string.Format("Total length: {0} bytes", totalLength);
			}
			if (Language == Language.Russian) {
				totalLengthLabel.Text = string.Format("Общий размер: {0} байт", totalLength);
			}
		}
	}
}