using System;
using CryptoFile.Client.Environment;
using CryptoFile.Client.Forms;
using CryptoFile.Client.Presenters;
using Moq;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Presenters {
	[TestFixture]
	public class AboutProgramFormPresenter_Test {
		private Mock<IAboutProgramForm> aboutProgramForm;
		private Mock<IEnvironmentHelper> environmentHelper;

		[SetUp]
		public void SetUp() {
			aboutProgramForm = new Mock<IAboutProgramForm>();
			environmentHelper = new Mock<IEnvironmentHelper>();
			new AboutProgramFormPresenter(aboutProgramForm.Object, environmentHelper.Object);
		}

		[Test]
		public void EmailClickTest() {
			aboutProgramForm.Setup(x => x.Email).Returns("hello");

			aboutProgramForm.Raise(x => x.EmailClick += null, EventArgs.Empty);

			environmentHelper.Verify(x => x.StartProcess("mailto:hello"));
		}
	}
}