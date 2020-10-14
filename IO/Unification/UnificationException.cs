using System;

namespace CryptoFile.IO.Unification {
	public class UnificationException : Exception {
		public UnificationException(Exception e) : base(null, e) {}
	}
}