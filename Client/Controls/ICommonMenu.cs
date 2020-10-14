using System;

namespace CryptoFile.Client.Controls {
	public interface ICommonMenu : ICryptoView {
		event EventHandler Cipher;
		event EventHandler Decipher;
		event EventHandler GenerateKeys;
	}
}