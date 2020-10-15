using System.Collections.Generic;
using CryptoFile.Client.Controls;

namespace CryptoFile.Client.Commands
{
	public class RefreshCryptoViewsCommand : ICommand
	{
		private readonly IFilesView filesView;
		private readonly IEnumerable<ICryptoView> cryptoViews;

		public RefreshCryptoViewsCommand(IFilesView filesView, IEnumerable<ICryptoView> cryptoViews)
		{
			this.filesView = filesView;
			this.cryptoViews = cryptoViews;
		}

		public void Execute()
		{
			bool cipherEnabled = GetCipherEnabled();
			bool decipherEnabled = GetDecipherEnabled();
			foreach (ICryptoView cryptoView in cryptoViews)
			{
				cryptoView.CipherEnabled = cipherEnabled;
				cryptoView.DecipherEnabled = decipherEnabled;
			}
		}

		private bool GetCipherEnabled()
		{
			if (filesView.SelectedEntities.Count == 1)
				return !filesView.SelectedEntities[0].IsCryptoFile;
			return filesView.SelectedEntities.Count > 1;
		}

		private bool GetDecipherEnabled()
		{
			if (filesView.SelectedEntities.Count == 0)
				return false;
			return !GetCipherEnabled();
		}
	}
}