using System.Windows.Forms;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Controls;

namespace CryptoFile.Client.Forms
{
	internal partial class MainForm : Form, IMainForm
	{
		private Language language;

		public MainForm()
		{
			InitializeComponent();
		}

		#region IMainForm Members

		public IMainMenuView MainMenu => mainMenuView;

		public IFilesView FilesView => filesView;

		public IToolBarView ToolBar => toolBarView;

		public IAddressToolBar AddressToolBar => addressToolBar;

		#endregion

		public Language Language
		{
			get => language;
			set
			{
				language = value;
				MainMenu.Language = language;
				ToolBar.Language = language;
				AddressToolBar.Language = language;
				FilesView.Language = language;
			}
		}
	}
}