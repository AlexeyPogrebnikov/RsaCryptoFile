using System;

namespace CryptoFile.Client.Forms {
	public interface IGenerateKeysForm : IForm {
		event EventHandler Generate;
		event EventHandler ChangePublicExponent;
		event EventHandler CancelGenerateKeys;
		int MinRsaKeyLength { get; set; }
		int MaxRsaKeyLength { get; set; }
		int RsaKeyLength { get; set; }
		string PublicKey { get; set; }
		string PrivateKey { get; set; }
		bool GenerateEnabled { get; set; }
		int PublicExponent { get; set; }
	}
}