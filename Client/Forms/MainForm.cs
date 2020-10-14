using System.Windows.Forms;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Controls;

namespace CryptoFile.Client.Forms {
	partial class MainForm : Form, IMainForm {
		private Language language;

		public MainForm() {
			InitializeComponent();
		}

		#region IMainForm Members

		public IMainMenuView MainMenu {
			get { return mainMenuView; }
		}

		public IFilesView FilesView {
			get { return filesView; }
		}

		public IToolBarView ToolBar {
			get { return toolBarView; }
		}

		public IAddressToolBar AddressToolBar {
			get { return addressToolBar; }
		}

		#endregion

		public Language Language {
			get { return language; }
			set {
				language = value;
				MainMenu.Language = language;
				ToolBar.Language = language;
				AddressToolBar.Language = language;
				FilesView.Language = language;
			}
		}
	}
}