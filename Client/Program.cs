using System;
using System.IO;
using System.Windows.Forms;
using CryptoFile.Client.Commands;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Forms;
using CryptoFile.Client.Presenters;
using CryptoFile.Client.Properties;

namespace CryptoFile.Client {
	static class Program {
		private static Options options;
		private static OptionsSaver saver;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ApplicationExit += Application_ApplicationExit;
			try {
				Run();
			} catch (Exception e) {
				using (var form = new UnhandledExceptionForm()) {
					if (options != null)
						form.Language = options.Language;
					form.Email = Settings.Default.Email;
					form.Message = e.ToString();
					form.Send += (sender, args) => ErrorSender.Send(e);
					form.ShowDialog();
				}
			}
		}

		private static void Run() {
			InitializeOptions();
			var form = new MainForm();
			form.FilesView.DirectoryImage = Resources.Folder;
			var environmentHelper = new EnvironmentHelper();
			var messageHelper = new MessageHelper(options);
			var formFactory = new FormFactory(options);
			var commandsContainer = new CommandsContainer(options, form, environmentHelper, messageHelper, formFactory);
			commandsContainer.SetMainForm(form);
			new MainFormPresenter(form, options, environmentHelper, commandsContainer, messageHelper, formFactory);
			Application.Run(form);
		}

		private static void InitializeOptions() {
			var executablePath = Path.GetDirectoryName(Application.ExecutablePath);
			if (executablePath == null)
				return;
			var fileName = Path.Combine(executablePath, "options.xml");
			saver = new OptionsSaver(fileName);
			options = saver.LoadOptions();
		}

		private static void Application_ApplicationExit(object sender, EventArgs e) {
			saver.SaveOptions(options);
		}
	}
}