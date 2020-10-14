using System;
using System.Windows.Forms;
using CryptoFile.Client.Configuration;

namespace CryptoFile.Client.Controls {
	public partial class ToolBarView : UserControl, IToolBarView {
		private Language language;

		public ToolBarView() {
			InitializeComponent();
		}

		#region IToolBarView Members

		public event EventHandler ToUpperDirectory;

		public event EventHandler RefreshDirectory;

		public bool ToUpperDirectoryEnabled {
			get { return toUpperDirectoryToolStripButton.Enabled; }
			set { toUpperDirectoryToolStripButton.Enabled = value; }
		}

		#endregion

		#region ICommonMenu Members

		public event EventHandler Cipher;

		public event EventHandler Decipher;

		public event EventHandler GenerateKeys;

		#endregion

		#region ICryptoView Members

		public bool CipherEnabled {
			get { return cipherToolStripButton.Enabled; }
			set { cipherToolStripButton.Enabled = value; }
		}

		public bool DecipherEnabled {
			get { return decipherToolStripButton.Enabled; }
			set { decipherToolStripButton.Enabled = value; }
		}

		public Language Language {
			get { return language; }
			set {
				language = value;
				if (language == Language.English) {
					toUpperDirectoryToolStripButton.ToolTipText = @"Up";
					refreshToolStripButton.ToolTipText = @"Refresh";
					cipherToolStripButton.ToolTipText = @"Cipher";
					decipherToolStripButton.ToolTipText = @"Decipher";
					generateKeysToolStripButton.ToolTipText = @"Generate keys";
				}
				if (language == Language.Russian) {
					toUpperDirectoryToolStripButton.ToolTipText = @"Вверх";
					refreshToolStripButton.ToolTipText = @"Обновить";
					cipherToolStripButton.ToolTipText = @"Шифровать";
					decipherToolStripButton.ToolTipText = @"Расшифровать";
					generateKeysToolStripButton.ToolTipText = @"Генерация ключей";
				}
			}
		}

		#endregion

		private void toUpperDirectoryToolStripButton_Click(object sender, EventArgs e) {
			if (ToUpperDirectory != null) {
				ToUpperDirectory(this, e);
			}
		}

		private void refreshToolStripButton_Click(object sender, EventArgs e) {
			if (RefreshDirectory != null) {
				RefreshDirectory(this, e);
			}
		}

		private void cipherToolStripButton_Click(object sender, EventArgs e) {
			if (Cipher != null) {
				Cipher(this, e);
			}
		}

		private void decipherToolStripButton_Click(object sender, EventArgs e) {
			if (Decipher != null) {
				Decipher(this, e);
			}
		}

		private void generateKeysToolStripButton_Click(object sender, EventArgs e) {
			if (GenerateKeys != null) {
				GenerateKeys(this, e);
			}
		}
	}
}