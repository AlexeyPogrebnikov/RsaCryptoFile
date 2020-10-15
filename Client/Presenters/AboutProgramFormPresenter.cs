using System;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Forms;

namespace CryptoFile.Client.Presenters
{
	internal class AboutProgramFormPresenter
	{
		private readonly IAboutProgramForm aboutProgramForm;
		private readonly IEnvironmentHelper environmentHelper;

		public AboutProgramFormPresenter(IAboutProgramForm aboutProgramForm, IEnvironmentHelper environmentHelper)
		{
			this.aboutProgramForm = aboutProgramForm;
			this.environmentHelper = environmentHelper;
			aboutProgramForm.EmailClick += form_EmailClick;
		}

		private void form_EmailClick(object sender, EventArgs e)
		{
			string process = string.Format("mailto:{0}", aboutProgramForm.Email);
			environmentHelper.StartProcess(process);
		}
	}
}