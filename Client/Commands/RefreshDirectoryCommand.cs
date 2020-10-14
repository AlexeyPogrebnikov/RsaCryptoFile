using CryptoFile.Client.Presenters;
using CryptoFile.IO.Entities;

namespace CryptoFile.Client.Commands {
	class RefreshDirectoryCommand : ICommand {
		private readonly IMessageHelper messageHelper;

		public RefreshDirectoryCommand(IMessageHelper messageHelper) {
			this.messageHelper = messageHelper;
		}

		#region ICommand Members

		public void Execute() {
			try {
				FilesViewPresenter.RefreshDirectory();
			} catch (FileEntityNotFoundException) {
				messageHelper.Show("Failed to refresh folder.", "Ошибка при обновлении папки.");
				FilesViewPresenter.OpenDefaultDirectory();
			}
		}

		#endregion

		public IFilesViewPresenter FilesViewPresenter { get; set; }
	}
}