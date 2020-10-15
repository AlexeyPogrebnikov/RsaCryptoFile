using System;

namespace CryptoFile.Client.Compression
{
	public class CompressionException : Exception
	{
		public CompressionException(Exception innerException)
			: base(null, innerException)
		{
		}
	}
}