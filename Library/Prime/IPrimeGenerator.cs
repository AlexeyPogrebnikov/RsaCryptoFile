using System;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Library.Prime {
	public interface IPrimeGenerator {
		BigNumber Generate(int length);

		/// <summary>
		/// Возвращает простое число большее чем number
		/// </summary>
		/// <exception cref="ArgumentNullException">number is null</exception>
		/// <exception cref="ArgumentException">number четное число</exception>
		BigNumber GenerateFrom(BigNumber number);
	}
}