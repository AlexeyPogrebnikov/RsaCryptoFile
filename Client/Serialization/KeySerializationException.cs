using System;

namespace CryptoFile.Client.Serialization {
	[Serializable]
	public class KeySerializationException : Exception {
		public KeySerializationException(string message) : base(message) {}
		public KeySerializationException(string message, Exception inner) : base(message, inner) {}
	}
}