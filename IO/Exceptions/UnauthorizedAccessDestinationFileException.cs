using System;

namespace CryptoFile.IO.Exceptions
{
	[Serializable]
	public class UnauthorizedAccessDestinationFileException : Exception
	{
		public UnauthorizedAccessDestinationFileException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}