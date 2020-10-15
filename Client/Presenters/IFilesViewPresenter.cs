using CryptoFile.IO.Entities;

namespace CryptoFile.Client.Presenters
{
	public interface IFilesViewPresenter
	{
		/// <exception cref="FileEntityNotFoundException">Ошибка при перемещении вверх</exception>
		void ToUpperFolder();

		/// <exception cref="FileEntityNotFoundException">Ошибка при открытии папки</exception>
		void OpenDefaultDirectory();

		/// <exception cref="FileEntityNotFoundException">Ошибка при обновлении папки</exception>
		void RefreshDirectory();
	}
}