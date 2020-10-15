using CryptoFile.Client.Configuration;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Forms;
using CryptoFile.Client.Presenters;

namespace CryptoFile.Client.Commands
{
	internal class GenerateKeysCommand : ICommand
	{
		private readonly IKeyGenerator keyGenerator;
		private readonly Options options;
		private readonly IFormFactory formFactory;
		private readonly IMessageHelper messageHelper;

		public GenerateKeysCommand(IKeyGenerator keyGenerator, Options options, IFormFactory formFactory,
			IMessageHelper messageHelper)
		{
			this.keyGenerator = keyGenerator;
			this.options = options;
			this.formFactory = formFactory;
			this.messageHelper = messageHelper;
		}

		#region ICommand Members

		public void Execute()
		{
			using (IGenerateKeysForm form = formFactory.CreateGenerateKeysForm())
			{
				new GenerateKeysFormPresenter(form, keyGenerator, options, messageHelper, formFactory);
				form.ShowDialog();
			}
		}

		#endregion
	}
}