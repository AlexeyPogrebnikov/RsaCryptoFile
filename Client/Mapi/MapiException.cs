using System;

namespace CryptoFile.Client.Mapi
{
	internal class MapiException : Exception
	{
		internal MapiException(int errorCode)
			: base(CreateErrorString(errorCode))
		{
		}

		private static string CreateErrorString(int errorCode)
		{
			if (errorCode < (int) MapiResult.MAX_VALUE)
				return String.Format("MAPI Error: {0} [{1}]", (MapiResult) errorCode, errorCode);
			return String.Format("Unknown MAPI error [{0}]", errorCode);
		}
	}
}