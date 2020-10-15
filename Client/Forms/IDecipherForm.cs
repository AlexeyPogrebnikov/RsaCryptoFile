using System;

namespace CryptoFile.Client.Forms
{
	public interface IDecipherForm : IForm
	{
		event EventHandler Decipher;
		event EventHandler CancelDecipher;
		event EventHandler OutputFileNameChanged;
		event EventHandler PrivateKeyChanged;
		string InputFileName { get; set; }
		string OutputDirectoryPath { get; set; }
		string PrivateKey { get; set; }
		bool DecipherEnabled { get; set; }
		int ProgressPercent { get; set; }
		bool CloseWindowAfterComlete { get; set; }
	}
}