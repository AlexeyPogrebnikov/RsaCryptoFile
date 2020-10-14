using System;

namespace CryptoFile.IO.Exceptions {
	[Serializable]
	public class UnauthorizedAccessSourceFileException : Exception {
		public UnauthorizedAccessSourceFileException(string message, Exception inner) : base(message, inner) {}
	}
}