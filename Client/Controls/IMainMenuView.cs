using System;

namespace CryptoFile.Client.Controls
{
	public interface IMainMenuView : ICommonMenu
	{
		event EventHandler Exit;
		event EventHandler Properties;
		event EventHandler LanguageChanged;
		event EventHandler AboutProgram;
	}
}