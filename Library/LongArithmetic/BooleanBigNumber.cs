using System;
using System.Collections;
using System.Collections.Generic;

namespace CryptoFile.Library.LongArithmetic
{
	public class BooleanBigNumber
	{
		private readonly IList<bool> numbers;

		private BooleanBigNumber(IEnumerable<bool> numbers)
		{
			this.numbers = new List<bool>(numbers);
		}

		public static BooleanBigNumber FromInt(int number)
		{
			var array = new BitArray(BitConverter.GetBytes(number));
			var result = new List<bool>();
			for (int i = array.Count - 1; i >= 0; --i)
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

			return new BooleanBigNumber(result);
		}

		public BooleanBigNumber Addition(BooleanBigNumber second)
		{
			return new BooleanBigNumber(Addition(numbers, second.numbers));
		}

		private static IEnumerable<bool> Addition(IList<bool> first, IList<bool> second)
		{
			int count = Math.Max(first.Count, second.Count);
			var result = new List<bool>(count);
			var remainder = false;
			for (var i = 0; i < count; ++i)
			{
				bool n1 = GetElement(first, i);
				bool n2 = GetElement(second, i);
				bool sum = n1 ^ n2 ^ remainder;
				remainder = n1 && n2 || n1 && remainder || n2 && remainder;
				result.Add(sum);
			}

			if (remainder)
				result.Add(true);
			return result;
		}

		public byte[] GetBytes()
		{
			var bytes = new List<byte>();
			var buff = 1;
			var element = 0;
			for (var i = 0; i < numbers.Count; ++i)
			{
				if (numbers[i])
					element += buff;
				buff *= 2;
				if (i > 0 && i % 7 == 0 || i == numbers.Count - 1)
				{
					buff = 1;
					bytes.Add((byte) element);
				}
			}

			return bytes.ToArray();
		}

		private static bool GetElement(IList<bool> array, int index)
		{
			return index < array.Count ? array[index] : false;
		}
	}
}