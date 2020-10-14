using System;

namespace CryptoFile.Client.Controls {
	public interface IToolBarView : ICommonMenu {
		event EventHandler ToUpperDirectory;
		event EventHandler RefreshDirectory;
		bool ToUpperDirectoryEnabled { get; set; }
	}
}