using System;

namespace CryptoFile.Client.Crypto {
	public class TooHighVersionException : Exception {
		public TooHighVersionException(string message) : base(message) {}
	}
}