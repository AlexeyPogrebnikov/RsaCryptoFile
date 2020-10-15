using System;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Client.Serialization
{
	internal interface IBigNumberSerializer
	{
		/// <exception cref="ArgumentNullException">number is null</exception>
		string Serialize(BigNumber number);

		/// <exception cref="ArgumentNullException">line is null</exception>
		/// <exception cref="ArgumentException">line is empty</exception>
		/// <exception cref="BigNumberFormatException">line содержит ошибки</exception>
		BigNumber Deserialize(string line);
	}
}