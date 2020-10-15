using System;
using CryptoFile.Client.Controls;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Forms;
using CryptoFile.Client.Presenters;
using CryptoFile.Client.Serialization;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Unification;
using CryptoFile.Library;

namespace CryptoFile.Client.Commands
{
	internal class DecipherCommand : ICommand
	{
		private readonly ICommandsContainer commandsContainer;
		private readonly IFilesView filesView;
		private readonly IRsaFactory rsaFactory;
		private readonly IEnvironmentHelper environmentHelper;
		private readonly IFormFactory formFactory;
		private readonly IMessageHelper messageHelper;
		private readonly IFileUnifier fileUnifier;

		/// <exception cref="ArgumentNullException">commandsContainer is null</exception>
		public DecipherCommand(ICommandsContainer commandsContainer,
			IFilesView filesView,
			IRsaFactory rsaFactory,
			IEnvironmentHelper environmentHelper,
			IFormFactory formFactory,
			IMessageHelper messageHelper,
			IFileUnifier fileUnifier)
		{
			Checker.CheckNull(commandsContainer);
			this.commandsContainer = commandsContainer;
			this.filesView = filesView;
			this.rsaFactory = rsaFactory;
			this.environmentHelper = environmentHelper;
			this.formFactory = formFactory;
			this.messageHelper = messageHelper;
			this.fileUnifier = fileUnifier;
		}

		#region ICommand Members

		public void Execute()
		{
			FileEntity file = filesView.SelectedFile;
			if (!CheckFile(file))
			{
				throw new InvalidOperationException("Selected file is null or is not rsa.");
			}

			using (IDecipherForm form = formFactory.CreateDecipherForm())
			{
				form.InputFileName = file.FullName;
				var serializer = new KeySerializer(new BigNumberHexSerializer());
				new DecipherFormPresenter(form, rsaFactory, serializer, commandsContainer, file, environmentHelper, messageHelper,
					fileUnifier);
				form.ShowDialog();
			}
		}

		#endregion

		private static bool CheckFile(FileSystemEntity file)
		{
			return file != null && file.IsCryptoFile;
		}
	}
}