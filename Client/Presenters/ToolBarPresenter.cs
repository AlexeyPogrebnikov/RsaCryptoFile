using System;
using CryptoFile.Client.Commands;
using CryptoFile.Client.Controls;

namespace CryptoFile.Client.Presenters
{
	internal class ToolBarPresenter
	{
		private readonly ICommandsContainer commandsContainer;

		public ToolBarPresenter(IToolBarView toolBarView, ICommandsContainer commandsContainer)
		{
			this.commandsContainer = commandsContainer;
			toolBarView.ToUpperDirectory += toolBarView_ToUpperDirectory;
			toolBarView.RefreshDirectory += toolBarView_RefreshDirectory;
			toolBarView.Cipher += toolBarView_Cipher;
			toolBarView.Decipher += toolBarView_Decipher;
			toolBarView.GenerateKeys += toolBarView_GenerateKeys;
		}

		private void toolBarView_ToUpperDirectory(object sender, EventArgs e)
		{
			commandsContainer.ToUpperFolderCommand.Execute();
		}

		private void toolBarView_RefreshDirectory(object sender, EventArgs e)
		{
			commandsContainer.RefreshDirectoryCommand.Execute();
		}

		private void toolBarView_Cipher(object sender, EventArgs e)
		{
			commandsContainer.CipherCommand.Execute();
		}

		private void toolBarView_Decipher(object sender, EventArgs e)
		{
			commandsContainer.DecipherCommand.Execute();
		}

		private void toolBarView_GenerateKeys(object sender, EventArgs e)
		{
			commandsContainer.GenerateKeysCommand.Execute();
		}
	}
}