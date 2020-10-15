using System;
using System.Runtime.Serialization;

namespace CryptoFile.IO.Exceptions
{
	[Serializable]
	public class SourceFileException : Exception
	{
		public SourceFileException()
		{
		}

		public SourceFileException(string message) : base(message)
		{
		}

		public SourceFileException(string message, Exception inner) : base(message, inner)
		{
		}

		protected SourceFileException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}