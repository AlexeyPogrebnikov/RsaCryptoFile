using System;

namespace CryptoFile.Client.Crypto
{
	public class IncorrectPrivateKeyException : Exception
	{
		public IncorrectPrivateKeyException(string message) : base(message)
		{
		}
	}
}