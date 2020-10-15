using System.Collections.Generic;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Library.Prime
{
	public class MultiplicityChecker
	{
		private readonly IList<BigNumber> factors = new List<BigNumber>();

		public MultiplicityChecker(int maxFactor)
		{
			for (var i = 2; i <= maxFactor; ++i)
			{
				BigNumber number = BigNumber.FromInt(i);
				var isPrimeNumber = true;
				for (var j = 0; j < factors.Count; ++j)
				{
					if (!factors[j].Mod(number).IsZero) continue;
					isPrimeNumber = false;
					break;
				}

				if (isPrimeNumber)
					factors.Add(number);
			}
		}

		/// <summary>
		/// Возвращает true, если число делится
		/// </summary>
		public bool Check(BigNumber number)
		{
			foreach (BigNumber factor in factors)
			{
				CompareResult compareResult = number.Compare(factor);
				if (compareResult == CompareResult.Less || compareResult == CompareResult.Equal)
					return false;
				if (number.Mod(factor).IsZero)
					return true;
			}

			return false;
		}
	}
}