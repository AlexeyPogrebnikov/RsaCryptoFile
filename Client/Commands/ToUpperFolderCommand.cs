using CryptoFile.Client.Presenters;
using CryptoFile.IO.Entities;

namespace CryptoFile.Client.Commands
{
	internal class ToUpperFolderCommand : ICommand
	{
		private readonly IMessageHelper messageHelper;
		private IFilesViewPresenter presenter;

		public ToUpperFolderCommand(IMessageHelper messageHelper)
		{
			this.messageHelper = messageHelper;
		}

		#region ICommand Members

		public void Execute()
		{
			try
			{
				presenter.ToUpperFolder();
			}
			catch (FileEntityNotFoundException)
			{
				messageHelper.Show("The parent folder is not found.", "Родительская папка не найдена.");
				presenter.OpenDefaultDirectory();
			}
		}

		#endregion

// ReSharper disable ParameterHidesMember
		public void SetFilesViewPresenter(IFilesViewPresenter presenter)
		{
// ReSharper restore ParameterHidesMember
			this.presenter = presenter;
		}
	}
}