using System;

namespace CryptoFile.Client.Controls
{
	public interface IAddressToolBar : IControl
	{
		event EventHandler PathChanged;
		string Path { get; set; }
	}
}