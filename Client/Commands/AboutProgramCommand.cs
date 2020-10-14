using CryptoFile.Client.Environment;
using CryptoFile.Client.Forms;
using CryptoFile.Client.Presenters;

namespace CryptoFile.Client.Commands {
	class AboutProgramCommand : ICommand {
		private readonly IFormFactory formFactory;
		private readonly IEnvironmentHelper environmentHelper;

		public AboutProgramCommand(IFormFactory formFactory, IEnvironmentHelper environmentHelper) {
			this.formFactory = formFactory;
			this.environmentHelper = environmentHelper;
		}

		#region ICommand Members

		public void Execute() {
			using (var form = formFactory.CreateAboutProgramForm()) {
				new AboutProgramFormPresenter(form, environmentHelper);
				form.ShowDialog();
			}
		}

		#endregion
	}
}