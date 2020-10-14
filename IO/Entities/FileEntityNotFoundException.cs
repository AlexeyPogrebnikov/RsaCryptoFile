using System;

namespace CryptoFile.IO.Entities {
	[Serializable]
	public class FileEntityNotFoundException : Exception {
		public FileEntityNotFoundException(string message)
			: base(message) {}

		public FileEntityNotFoundException(string message, Exception inner) : base(message, inner) {}
	}
}