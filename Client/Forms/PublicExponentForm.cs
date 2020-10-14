using System.Collections.Generic;
using System.Windows.Forms;
using CryptoFile.Client.Configuration;

namespace CryptoFile.Client.Forms {
	public partial class PublicExponentForm : Form, IPublicExponentForm {
		private Language language;

		public PublicExponentForm() {
			InitializeComponent();
		}

		public void SetPublicExponents(IEnumerable<int> publicExponents) {
			foreach (var publicExponent in publicExponents) {
				checkedListBox.Items.Add(publicExponent);
			}
		}

		public int PublicExponent {
			get { return int.Parse(checkedListBox.SelectedItem.ToString()); }
			set {
				checkedListBox.SelectedItem = value;
				checkedListBox.SetItemChecked(checkedListBox.SelectedIndex, true);
			}
		}

		private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e) {
			if (checkedListBox.CheckedIndices.Count == 1 && e.NewValue == CheckState.Unchecked) {
				e.NewValue = CheckState.Checked;
			}
			if (checkedListBox.CheckedIndices.Count > 0) {
				checkedListBox.ItemCheck -= checkedListBox_ItemCheck;
				foreach (int index in checkedListBox.CheckedIndices) {
					if (index != e.Index) {
						checkedListBox.SetItemChecked(index, false);
					}
				}
				checkedListBox.ItemCheck += checkedListBox_ItemCheck;
			}
		}

		public Language Language {
			get { return language; }
			set {
				language = value;
				if (language == Language.English) {
					Text = @"Public exponents";
					cancelButton.Text = @"Cancel";
				}
				if (language == Language.Russian) {
					Text = @"Открытые экспоненты";
					cancelButton.Text = @"Отмена";
				}
			}
		}
	}
}