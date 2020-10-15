using System;

namespace CryptoFile.Client.Serialization
{
	[Serializable]
	public class BigNumberFormatException : ArgumentException
	{
		public BigNumberFormatException(string message) : base(message)
		{
		}
	}
}