using System;

namespace CryptoFile.IO.Exceptions
{
	public class DestinationFileException : Exception
	{
		public DestinationFileException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}