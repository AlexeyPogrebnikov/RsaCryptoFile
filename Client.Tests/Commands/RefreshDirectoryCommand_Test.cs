using CryptoFile.Client.Commands;
using CryptoFile.Client.Presenters;
using CryptoFile.IO.Entities;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Commands
{
	[TestFixture]
	public class RefreshDirectoryCommand_Test
	{
		private RefreshDirectoryCommand command;
		private Mock<IFilesViewPresenter> filesViewPresenter;
		private Mock<IMessageHelper> messageHelper;

		[SetUp]
		public void SetUp()
		{
			filesViewPresenter = new Mock<IFilesViewPresenter>();
			messageHelper = new Mock<IMessageHelper>();
			command = new RefreshDirectoryCommand(messageHelper.Object);
			command.FilesViewPresenter = filesViewPresenter.Object;
		}

		[Test]
		public void Execute_ErrorWhenRefreshDirectory()
		{
			filesViewPresenter.Setup(x => x.RefreshDirectory()).Throws(new FileEntityNotFoundException("hello"));
			command.Execute();
			messageHelper.Verify(x => x.Show("Failed to refresh folder.", "Ошибка при обновлении папки."));
		}
	}
}