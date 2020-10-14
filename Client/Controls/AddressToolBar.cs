using System;
using System.Windows.Forms;
using CryptoFile.Client.Configuration;

namespace CryptoFile.Client.Controls {
	public partial class AddressToolBar : UserControl, IAddressToolBar {
		private string path;
		private Language language;

		public AddressToolBar() {
			InitializeComponent();
		}

		#region IAddressToolBar Members

		public event EventHandler PathChanged;

		public string Path {
			get { return path; }
			set {
				path = value;
				RefreshAddress();
			}
		}

		public Language Language {
			get { return language; }
			set {
				language = value;
				RefreshAddress();
				if (language == Language.English) {
					addressToolStripButton.ToolTipText = @"Browse";
				}
				if (language == Language.Russian) {
					addressToolStripButton.ToolTipText = @"Обзор";
				}
			}
		}

		#endregion

		private void addressToolStripButton_Click(object sender, EventArgs e) {
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
				Path = folderBrowserDialog.SelectedPath;
				if (PathChanged != null)
					PathChanged(this, e);
			}
		}

		private void RefreshAddress() {
			var address = "Address";
			if (language == Language.Russian) {
				address = "Адрес";
			}
			addressToolStripLabel.Text = string.Format("{0}: {1}", address, path);
		}
	}
}