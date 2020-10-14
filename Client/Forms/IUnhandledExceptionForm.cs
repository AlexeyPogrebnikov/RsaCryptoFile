using System;

namespace CryptoFile.Client.Forms {
	interface IUnhandledExceptionForm : IForm {
		event EventHandler Send;
		string Email { get; set; }
		string Message { get; set; }
	}
}