using System;
using System.Collections;
using System.Collections.Generic;

namespace CryptoFile.Library.LongArithmetic
{
	public class BigNumber
	{
		private readonly List<int> numbers;
		private const int baseOfNumeration = 256;
		private const int countBits = 8;

		private BigNumber(IList<int> numbers)
		{
			CheckNumbers(numbers);
			this.numbers = new List<int>(numbers);
		}

		public static BigNumber FromBytes(IList<int> numbers)
		{
			return new BigNumber(numbers);
		}

		public static BigNumber FromInt(int number)
		{
			if (number < 0)
				throw new ArgumentException("number < 0");
			byte[] bytes = BitConverter.GetBytes(number);
			var numbers = new List<int>();
			for (int i = bytes.Length - 1; i >= 0; --i)
			{
				if (bytes[i] > 0 || numbers.Count > 0)
					numbers.Insert(0, bytes[i]);
			}

			if (numbers.Count == 0)
				numbers.Add(0);
			return new BigNumber(numbers);
		}

		public BigNumber Addition(BigNumber number)
		{
			return new BigNumber(Addition(numbers, number.Numbers));
		}

		public BigNumber Subtraction(BigNumber number)
		{
			CompareResult compare = Compare(number);
			if (compare == CompareResult.Less || compare == CompareResult.Equal)
			{
				return CreateZero();
			}

			return new BigNumber(Subtraction(numbers, number.Numbers));
		}

		public void Increment()
		{
			++numbers[0];
			var i = 0;
			while (i < numbers.Count - 1 && numbers[i] >= baseOfNumeration)
			{
				numbers[i] = 0;
				++numbers[i + 1];
				++i;
			}

			if (numbers[numbers.Count - 1] >= baseOfNumeration)
			{
				numbers[numbers.Count - 1] = 0;
				numbers.Add(1);
			}
		}

		public void Decrement()
		{
			--numbers[0];
			var i = 0;
			while (i < numbers.Count - 1 && numbers[i] < 0)
			{
				numbers[i] = baseOfNumeration - 1;
				--numbers[i + 1];
				++i;
			}

			if (numbers[numbers.Count - 1] == 0)
			{
				numbers.RemoveAt(numbers.Count - 1);
			}
		}

		public BigNumber Multiplication(BigNumber number)
		{
			if (IsZero || number.IsZero)
			{
				return CreateZero();
			}

			return new BigNumber(Multiplication(number.Numbers, numbers));
		}

		/// <summary>
		/// Делит число на divisor, возвращает результат деления
		/// </summary>
		/// <exception cref="DivideByZeroException">divisor == 0</exception>
		public BigNumber Division(BigNumber divisor)
		{
			if (divisor.IsZero)
			{
				throw new DivideByZeroException("divisor == 0");
			}

			BigNumber one = FromInt(1);
			if (divisor.Compare(one) == CompareResult.Equal)
			{
				return Clone();
			}

			CompareResult compare = Compare(divisor);
			if (compare == CompareResult.Less)
			{
				return CreateZero();
			}

			return compare == CompareResult.Equal ? FromInt(1) : new BigNumber(Division(numbers, divisor.Numbers));
		}

		/// <exception cref="DivideByZeroException">foundation == 0</exception>
		public BigNumber Mod(BigNumber foundation)
		{
			if (foundation.IsZero)
			{
				throw new DivideByZeroException("foundation == 0");
			}

			CompareResult compare = Compare(foundation);
			if (compare == CompareResult.Less)
			{
				return Clone();
			}

			if (compare == CompareResult.Equal)
			{
				return CreateZero();
			}

			return new BigNumber(Mod(numbers, foundation.Numbers));
		}

		public BigNumber Power(BigNumber degree)
		{
			if (degree.IsZero)
			{
				return FromInt(1);
			}

			IList<int> result = new List<int> { 1 };
			IList<int> _base = new List<int>(numbers);
			bool[] bits = degree.ToBits();
			for (var i = 0; i < bits.Length - 1; ++i)
			{
				if (bits[i])
				{
					result = Multiplication(result, _base);
				}

				_base = Square(_base);
			}

			return new BigNumber(Multiplication(result, _base));
		}

		/// <summary>
		/// Возводит число в степень по модулю foundation
		/// </summary>
		/// <exception cref="DivideByZeroException">foundation == 0</exception>
		public BigNumber Power(BigNumber degree, BigNumber foundation)
		{
			if (foundation.IsZero)
			{
				throw new DivideByZeroException("foundation == 0");
			}

			if (degree.IsZero)
			{
				return FromInt(1);
			}

			IList<int> result = new List<int> { 1 };
			IList<int> _base = new List<int>(numbers);
			IList<int> foundationNumbers = foundation.numbers;
			bool[] bits = degree.ToBits();
			for (var i = 0; i < bits.Length - 1; ++i)
			{
				if (bits[i])
				{
					result = Mod(Multiplication(result, _base), foundationNumbers);
				}

				_base = Mod(Square(_base), foundationNumbers);
			}

			return new BigNumber(Mod(Multiplication(result, _base), foundationNumbers));
		}

		/// <summary>
		/// Возводит число в степень по модулю foundation
		/// </summary>
		/// <exception cref="DivideByZeroException">foundation == 0</exception>
		public BigNumber Power(IList<bool> degree, BigNumber foundation)
		{
			if (foundation.IsZero)
			{
				throw new DivideByZeroException("foundation == 0");
			}

			if (degree.Count == 1 && !degree[0])
			{
				return FromInt(1);
			}

			IList<int> result = new List<int> { 1 };
			IList<int> _base = new List<int>(numbers);
			IList<int> _foundation = foundation.numbers;
			for (var i = 0; i < degree.Count - 1; ++i)
			{
				if (degree[i])
				{
					result = Mod(Multiplication(result, _base), _foundation);
				}

				_base = Mod(Square(_base), _foundation);
			}

			return new BigNumber(Mod(Multiplication(result, _base), _foundation));
		}

		/// <summary>
		/// Возвращает квадрат числа
		/// </summary>
		public BigNumber Square()
		{
			return new BigNumber(Square(numbers));
		}

		public CompareResult Compare(BigNumber number)
		{
			return Compare(numbers, number.Numbers);
		}

		/// <summary>
		/// Возвщает число в виде массива бит
		/// </summary>
		public bool[] ToBits()
		{
			IList<bool> firstBits = ToBits(numbers[numbers.Count - 1]);
			var result = new bool[(Digit - 1) * countBits + firstBits.Count];
			for (var i = 0; i < Digit - 1; ++i)
			{
				var array = new BitArray(new[] { (byte) numbers[i] });
				for (var j = 0; j < countBits; ++j)
				{
					result[i * countBits + j] = array[j];
				}
			}

			for (var i = 0; i < firstBits.Count; ++i)
			{
				result[(Digit - 1) * countBits + i] = firstBits[i];
			}

			return result;
		}

		public BigNumber Clone()
		{
			return new BigNumber(numbers);
		}

		/// <summary>
		/// Возвращает разрядность числа
		/// </summary>
		public int Digit => numbers.Count;

		/// <summary>
		/// Возвращает true, если число четное.
		/// </summary>
		public bool IsEven => numbers[0] % 2 == 0;

		public IList<int> Numbers => numbers;

		public bool IsZero
		{
			get
			{
				BigNumber zero = CreateZero();
				return zero.Compare(this) == CompareResult.Equal;
			}
		}

		/// <exception cref="ArgumentException"></exception>
		private static void CheckNumbers(IList<int> numbers)
		{
			Checker.CheckNull(numbers);
			if (numbers.Count == 0)
			{
				throw new ArgumentException("numbers.Length is empty", "numbers");
			}

			if (numbers.Count > 1 && numbers[numbers.Count - 1] == 0)
			{
				throw new ArgumentException("numbers[numbers.Count - 1] == 0 and numbers is not zero", "numbers");
			}
		}

		private static BigNumber CreateZero()
		{
			return FromInt(0);
		}

		private static IList<int> Addition(IList<int> first, IList<int> second)
		{
			int count = Math.Max(first.Count, second.Count);
			var result = new List<int>(count);
			var remainder = 0;
			for (var i = 0; i < count; ++i)
			{
				int n1 = GetElement(first, i);
				int n2 = GetElement(second, i);
				int sum = n1 + n2 + remainder;
				remainder = sum / baseOfNumeration;
				result.Add(sum % baseOfNumeration);
			}

			if (remainder > 0)
				result.Add(remainder);
			return result;
		}

		private static IList<int> Subtraction(ICollection<int> first, IList<int> second)
		{
			var result = new List<int>(first);
			int count = Math.Min(first.Count, second.Count);
			for (var i = 0; i < count; ++i)
			{
				result[i] -= second[i];
				int k = i;
				while (result[k] < 0)
				{
					result[k] += baseOfNumeration;
					--result[k + 1];
					++k;
				}
			}

			while (result[result.Count - 1] == 0)
			{
				result.RemoveAt(result.Count - 1);
			}

			return result;
		}

		private static IList<int> Multiplication(IList<int> first, IList<int> second)
		{
			var result = new List<int>(Math.Max(first.Count, second.Count));
			for (var i = 0; i < first.Count; ++i)
			{
				var remainder = 0;
				for (var j = 0; j < second.Count; ++j)
				{
					int pos = i + j;
					if (pos >= result.Count)
					{
						result.Add(0);
					}

					int buff = result[pos] + first[i] * second[j] + remainder;
					remainder = buff / baseOfNumeration;
					result[pos] = buff % baseOfNumeration;
				}

				if (remainder > 0)
				{
					result.Add(remainder);
				}
			}

			return result;
		}

		/// <summary>
		/// Делит число на цело
		/// </summary>
		/// <exception cref="DivideByZeroException">divisor == 0</exception>
		private static IList<int> Division(IList<int> divisible, IList<int> divisor)
		{
			divisible = new List<int>(divisible);
			int firstIndex = divisible.Count - divisor.Count;
			var result = new List<int>(new int[firstIndex + 1]);
			int lastIndex = divisible.Count - 1;
			do
			{
				if (lastIndex - firstIndex <= divisor.Count)
				{
					int t = lastIndex - firstIndex + 1;
					while (Compare(divisible, divisor, firstIndex, t, 0, divisor.Count) == CompareResult.Less)
					{
						--firstIndex;
						++t;
						if (firstIndex < 0)
						{
							break;
						}
					}

					if (firstIndex < 0)
					{
						break;
					}
				}

				// Фаза вычитания
				for (var i = 0; i < divisor.Count; ++i)
				{
					int k = firstIndex + i;
					divisible[k] -= divisor[i];
					while (divisible[k] < 0)
					{
						divisible[k] += baseOfNumeration;
						--divisible[k + 1];
						++k;
					}
				}

				while (lastIndex >= 0 && divisible[lastIndex] == 0)
				{
					--lastIndex;
				}

				++result[firstIndex];
			} while (firstIndex >= 0);

			while (result.Count > 1 && result[result.Count - 1] == 0)
			{
				result.RemoveAt(result.Count - 1);
			}

			return result;
		}

		private static IList<int> Mod(IEnumerable<int> number, IList<int> foundation)
		{
			var result = new List<int>(number);
			int foundationCount = foundation.Count;
			int resultCount = result.Count;
			while (Compare(result, foundation) == CompareResult.Greater)
			{
				int p = resultCount - foundationCount;
				var factor = 1;
				if (p > 0)
				{
					int highByte = foundation[foundationCount - 1];
					if (result[resultCount - 1] <= highByte)
					{
						--p;
						factor = result[resultCount - 2] / highByte;
					}
					else
					{
						factor = result[resultCount - 1] / (highByte + 1);
					}

					factor = Math.Max(factor, 1);
				}

				for (var i = 0; i < foundationCount; ++i)
				{
					int index = i + p;
					result[index] -= foundation[i] * factor;
					while (result[index] < 0)
					{
						int k = 1 - result[index] / baseOfNumeration;
						result[index] += baseOfNumeration * k;
						result[index + 1] -= k;
						++index;
					}
				}

				while (resultCount > 1 && result[resultCount - 1] == 0)
				{
					result.RemoveAt(resultCount - 1);
					--resultCount;
				}
			}

			if (Compare(result, foundation) == CompareResult.Equal)
			{
				return new[] { 0 };
			}

			return result;
		}

		private static IList<int> Square(IList<int> numbers)
		{
			var result = new List<int>(new int[numbers.Count * 2]);
			for (var i = 0; i < numbers.Count; ++i)
			{
				int pos = i + i;
				int buff = result[pos] + numbers[i] * numbers[i];
				int remainder = buff / baseOfNumeration;
				result[pos] = buff % baseOfNumeration;
				for (int j = i + 1; j < numbers.Count; ++j)
				{
					pos = i + j;
					buff = result[pos] + 2 * numbers[i] * numbers[j] + remainder;
					remainder = buff / baseOfNumeration;
					result[pos] = buff % baseOfNumeration;
				}

				result[pos + 1] = remainder;
			}

			if (result[result.Count - 1] == 0)
			{
				result.RemoveAt(result.Count - 1);
			}

			return result;
		}

		private static CompareResult Compare(IList<int> first, IList<int> second)
		{
			int compare = first.Count.CompareTo(second.Count);
			if (compare == 1)
			{
				return CompareResult.Greater;
			}

			if (compare == -1)
			{
				return CompareResult.Less;
			}

			for (int i = first.Count - 1; i >= 0; --i)
			{
				compare = first[i].CompareTo(second[i]);
				if (compare == 1)
				{
					return CompareResult.Greater;
				}

				if (compare == -1)
				{
					return CompareResult.Less;
				}
			}

			return CompareResult.Equal;
		}

		private static CompareResult Compare(IList<int> first, IList<int> second, int startIndexOfFirst, int firstLength,
			int startIndexOfSecond, int secondLength)
		{
			if (firstLength > secondLength)
			{
				return CompareResult.Greater;
			}

			if (firstLength < secondLength)
			{
				return CompareResult.Less;
			}

			for (var i = 0; i < firstLength; ++i)
			{
				int firstValue = first[startIndexOfFirst + firstLength - i - 1];
				int secondValue = second[startIndexOfSecond + secondLength - i - 1];
				if (firstValue > secondValue)
				{
					return CompareResult.Greater;
				}

				if (firstValue < secondValue)
				{
					return CompareResult.Less;
				}
			}

			return CompareResult.Equal;
		}

		private static IList<bool> ToBits(int number)
		{
			var array = new BitArray(new[] { (byte) number });
			var result = new List<bool>();
			for (int i = countBits - 1; i >= 0; --i)
			{
				if (array[i])
				{
					result.Insert(0, true);
				}
				else if (result.Count > 0)
				{
					result.Insert(0, false);
				}
			}

			if (result.Count == 0)
			{
				result.Add(false);
			}

			return result;
		}

		private static int GetElement(IList<int> array, int index)
		{
			return index < array.Count ? array[index] : 0;
		}
	}
}