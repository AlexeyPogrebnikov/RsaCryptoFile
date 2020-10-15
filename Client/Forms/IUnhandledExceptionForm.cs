using System;

namespace CryptoFile.Client.Forms
{
	internal interface IUnhandledExceptionForm : IForm
	{
		event EventHandler Send;
		string Email { get; set; }
		string Message { get; set; }
	}
}