using CryptoFile.Client.Commands;
using CryptoFile.Client.Presenters;
using CryptoFile.IO.Entities;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Commands
{
	[TestFixture]
	public class ToUpperFolderCommand_Test
	{
		private ToUpperFolderCommand command;
		private Mock<IFilesViewPresenter> filesViewPresenter;
		private Mock<IMessageHelper> messageHelper;

		[SetUp]
		public void SetUp()
		{
			filesViewPresenter = new Mock<IFilesViewPresenter>();
			messageHelper = new Mock<IMessageHelper>();
			command = new ToUpperFolderCommand(messageHelper.Object);
		}

		[Test]
		public void Execute_ErrorToUpperFolder_CheckMessage()
		{
			filesViewPresenter.Setup(x => x.ToUpperFolder()).Throws(new FileEntityNotFoundException("hello"));
			command.SetFilesViewPresenter(filesViewPresenter.Object);
			command.Execute();

			messageHelper.Verify(x => x.Show("The parent folder is not found.", "Родительская папка не найдена."));
		}
	}
}