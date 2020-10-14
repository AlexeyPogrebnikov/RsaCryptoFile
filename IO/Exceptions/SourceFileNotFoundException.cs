using System;

namespace CryptoFile.IO.Exceptions {
	[Serializable]
	public class SourceFileNotFoundException : Exception {
		public SourceFileNotFoundException(string message) : base(message) {}

		public SourceFileNotFoundException(string message, Exception inner) : base(message, inner) {}
	}
}