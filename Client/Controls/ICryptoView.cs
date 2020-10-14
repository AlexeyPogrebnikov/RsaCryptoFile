namespace CryptoFile.Client.Controls {
	public interface ICryptoView : IControl {
		bool CipherEnabled { get; set; }
		bool DecipherEnabled { get; set; }
	}
}