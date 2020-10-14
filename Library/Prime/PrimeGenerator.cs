using System;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Library.Prime {
	public class PrimeGenerator : IPrimeGenerator {
		private readonly IPrimeTest primeTest;
		private readonly MultiplicityChecker multiplicityChecker = new MultiplicityChecker(255);

		/// <exception cref="ArgumentNullException">primeTest is null</exception>
		public PrimeGenerator(IPrimeTest primeTest) {
			Checker.CheckNull(primeTest);
			this.primeTest = primeTest;
		}

		#region IPrimeGenerator Members

		public BigNumber Generate(int length) {
			var number = GenerateOddNumber(length);
			return GenerateFrom(number);
		}

		public BigNumber GenerateFrom(BigNumber number) {
			Checker.CheckNull(number);
			if (number.IsEven) {
				number = number.Clone();
				number.Increment();
			}
			var two = BigNumber.FromInt(2);
			while (!PrimeTestNumber(number)) {
				number = number.Addition(two);
			}
			return number;
		}

		#endregion

		/// <summary>
		/// Если число вероятно простое, то возвращается true.
		/// </summary>
		private bool PrimeTestNumber(BigNumber number) {
			return !multiplicityChecker.Check(number) && primeTest.CheckPrimality(number);
		}

		private static BigNumber GenerateOddNumber(int length) {
			var number = BigNumber.FromBytes(GenerateArray(length));
			if (number.IsEven)
				number.Increment();
			return number;
		}

		private static int[] GenerateArray(int length) {
			var random = new Random();
			var bytes = new byte[length];
			random.NextBytes(bytes);
			var numbers = new int[length];
			for (var i = 0; i < length; ++i) {
				numbers[i] = bytes[i];
			}
			if (numbers[length - 1] == 0) {
				numbers[length - 1] = 1;
			}
			return numbers;
		}
	}
}