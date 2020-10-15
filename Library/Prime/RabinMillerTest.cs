using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Library.Prime
{
	public class RabinMillerTest : IPrimeTest
	{
		private static readonly BigNumber zero = BigNumber.FromInt(0);
		private static readonly BigNumber one = BigNumber.FromInt(1);
		private static readonly BigNumber two = BigNumber.FromInt(2);
		private readonly BigNumber[] witnesses;

		public RabinMillerTest(int countWitnesses)
		{
			witnesses = new BigNumber[countWitnesses];
			for (var i = 0; i < countWitnesses; ++i)
			{
				witnesses[i] = BigNumber.FromInt(i + 2);
			}
		}

		internal RabinMillerTest(params BigNumber[] witnesses)
		{
			this.witnesses = witnesses;
		}

		#region IPrimeTest Members

		public bool CheckPrimality(BigNumber number)
		{
			Checker.CheckNull(number);
			BigNumber buff = number.Subtraction(one);
			BigNumber degree = zero.Clone();
			do
			{
				buff = buff.Division(two);
				degree.Increment();
			} while (buff.IsEven);

			foreach (BigNumber witness in witnesses)
			{
				if (CheckPseudoprimeNumber(witness, buff, number))
				{
					continue;
				}

				BigNumber power = witness.Power(buff, number);
				var flag = false;
				for (BigNumber index = zero.Clone(); index.Compare(degree.Subtraction(one)) == CompareResult.Less; index.Increment())
				{
					power = power.Square().Mod(number);
					if (power.Compare(one) == CompareResult.Equal)
					{
						return false;
					}

					if (power.Compare(number.Subtraction(one)) == CompareResult.Equal)
					{
						flag = true;
						break;
					}
				}

				if (flag)
				{
					continue;
				}

				return false;
			}

			return true;
		}

		#endregion

		/// <summary>
		/// Проверяет равенство (witness ^ r) MOD number == 1
		/// </summary>
		private static bool CheckPseudoprimeNumber(BigNumber witness, BigNumber r, BigNumber number)
		{
			BigNumber power = witness.Power(r, number);
			if (one.Compare(power) == CompareResult.Equal)
			{
				return true;
			}

			number = number.Subtraction(one);
			return number.Compare(power) == CompareResult.Equal;
		}
	}
}