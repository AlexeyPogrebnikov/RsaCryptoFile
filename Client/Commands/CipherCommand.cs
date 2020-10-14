using System;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Controls;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Forms;
using CryptoFile.Client.Presenters;
using CryptoFile.Client.Serialization;
using CryptoFile.IO.Unification;
using CryptoFile.Library;

namespace CryptoFile.Client.Commands {
	class CipherCommand : ICommand {
		private readonly ICommandsContainer commandsContainer;
		private readonly IFilesView filesView;
		private readonly IFormFactory formFactory;
		private readonly IFileUnifier fileUnifier;
		private readonly IEnvironmentHelper environmentHelper;
		private readonly Options options;
		private readonly IRsaFactory rsaFactory;
		private readonly IMessageHelper messageHelper;

		/// <exception cref="ArgumentNullException">commandsContainer is null</exception>
		public CipherCommand(ICommandsContainer commandsContainer,
		                     IFilesView filesView,
		                     IFormFactory formFactory,
		                     IFileUnifier fileUnifier,
		                     IEnvironmentHelper environmentHelper,
		                     Options options,
		                     IRsaFactory rsaFactory,
		                     IMessageHelper messageHelper) {
			Checker.CheckNull(commandsContainer);
			this.commandsContainer = commandsContainer;
			this.filesView = filesView;
			this.formFactory = formFactory;
			this.fileUnifier = fileUnifier;
			this.environmentHelper = environmentHelper;
			this.options = options;
			this.rsaFactory = rsaFactory;
			this.messageHelper = messageHelper;
		}

		#region ICommand Members

		public void Execute() {
			var entities = filesView.SelectedEntities;
			if (entities.Count == 0) {
				throw new InvalidOperationException("filesView.SelectedEntities.Count is empty");
			}
			var keySerializer = new KeySerializer(new BigNumberHexSerializer());
			using (var form = formFactory.CreateCipherForm()) {
				var presenter = new CipherFormPresenter(form,
				                                        entities,
				                                        rsaFactory,
				                                        keySerializer,
				                                        commandsContainer,
				                                        fileUnifier,
				                                        environmentHelper,
				                                        messageHelper,
				                                        options);
				presenter.ShowDialog();
			}
		}

		#endregion
	}
}