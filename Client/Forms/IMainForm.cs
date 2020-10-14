using CryptoFile.Client.Controls;

namespace CryptoFile.Client.Forms {
	public interface IMainForm : IForm {
		IMainMenuView MainMenu { get; }
		IFilesView FilesView { get; }
		IToolBarView ToolBar { get; }
		IAddressToolBar AddressToolBar { get; }
	}
}