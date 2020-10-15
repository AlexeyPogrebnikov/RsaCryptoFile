using System;

namespace CryptoFile.Client.Forms
{
	public interface IAboutProgramForm : IForm
	{
		string Email { get; set; }
		event EventHandler EmailClick;
	}
}