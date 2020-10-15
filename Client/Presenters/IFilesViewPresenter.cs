using CryptoFile.IO.Entities;

namespace CryptoFile.Client.Presenters
{
	public interface IFilesViewPresenter
	{
		/// <exception cref="FileEntityNotFoundException">������ ��� ����������� �����</exception>
		void ToUpperFolder();

		/// <exception cref="FileEntityNotFoundException">������ ��� �������� �����</exception>
		void OpenDefaultDirectory();

		/// <exception cref="FileEntityNotFoundException">������ ��� ���������� �����</exception>
		void RefreshDirectory();
	}
}