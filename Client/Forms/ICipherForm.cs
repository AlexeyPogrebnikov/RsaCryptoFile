using System;
using System.Collections.Generic;
using CryptoFile.IO.Entities;

namespace CryptoFile.Client.Forms
{
	public interface ICipherForm : IForm
	{
		event EventHandler Cipher;
		event EventHandler CancelCipher;
		event EventHandler OutputFileNameChanged;
		event EventHandler PublicKeyChanged;
		IEnumerable<FileSystemEntity> InputFileEntities { get; set; }
		long TotalLength { get; set; }
		string OutputFileName { get; set; }
		string PublicKey { get; set; }
		bool CipherEnabled { get; set; }
		int ProgressPercent { get; set; }
		bool CloseWindowAfterComlete { get; set; }
		bool ZipСompression { get; set; }
	}
}