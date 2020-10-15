using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Library.Tests.LongArithmetic
{
	[TestFixture]
	public class BooleanBigNumber_Test
	{
		[Test]
		public void GetBytes_NumberIs0()
		{
			BooleanBigNumber number = BooleanBigNumber.FromInt(0);
			CheckNumber(number, 0);
		}

		[Test]
		public void GetBytes_NumberIs1()
		{
			BooleanBigNumber number = BooleanBigNumber.FromInt(1);
			CheckNumber(number, 1);
		}

		[Test]
		public void GetBytes_NumberIs2()
		{
			BooleanBigNumber number = BooleanBigNumber.FromInt(2);
			CheckNumber(number, 2);
		}

		[Test]
		public void GetBytes_NumberIs256()
		{
			BooleanBigNumber number = BooleanBigNumber.FromInt(256);
			CheckNumber(number, 0, 1);
		}

		[Test]
		public void GetBytes_NumberIs128()
		{
			BooleanBigNumber number = BooleanBigNumber.FromInt(128);
			CheckNumber(number, 128);
		}

		[Test]
		public void Addition_FirstIs0_SecondIs0()
		{
			BooleanBigNumber first = BooleanBigNumber.FromInt(0);
			BooleanBigNumber second = BooleanBigNumber.FromInt(0);
			BooleanBigNumber result = first.Addition(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Addition_FirstIs1_SecondIs0()
		{
			BooleanBigNumber first = BooleanBigNumber.FromInt(1);
			BooleanBigNumber second = BooleanBigNumber.FromInt(0);
			BooleanBigNumber result = first.Addition(second);
			CheckNumber(result, 1);
		}

		[Test]
		public void Addition_FirstIs1_SecondIs1()
		{
			BooleanBigNumber first = BooleanBigNumber.FromInt(1);
			BooleanBigNumber second = BooleanBigNumber.FromInt(1);
			BooleanBigNumber result = first.Addition(second);
			CheckNumber(result, 2);
		}

		[Test]
		public void Addition_FirstIs2_SecondIs3()
		{
			BooleanBigNumber first = BooleanBigNumber.FromInt(2);
			BooleanBigNumber second = BooleanBigNumber.FromInt(3);
			BooleanBigNumber result = first.Addition(second);
			CheckNumber(result, 5);
		}

		[Test]
		public void Addition_FirstIs5_SecondIs7()
		{
			BooleanBigNumber first = BooleanBigNumber.FromInt(5);
			BooleanBigNumber second = BooleanBigNumber.FromInt(7);
			BooleanBigNumber result = first.Addition(second);
			CheckNumber(result, 12);
		}

		[Test]
		public void Addition_FirstIs3_SecondIs3()
		{
			BooleanBigNumber first = BooleanBigNumber.FromInt(3);
			BooleanBigNumber second = BooleanBigNumber.FromInt(3);
			BooleanBigNumber result = first.Addition(second);
			CheckNumber(result, 6);
		}

		[Test]
		public void Addition_FirstIs2_SecondIs2()
		{
			BooleanBigNumber first = BooleanBigNumber.FromInt(2);
			BooleanBigNumber second = BooleanBigNumber.FromInt(2);
			BooleanBigNumber result = first.Addition(second);
			CheckNumber(result, 4);
		}

		[Test]
		public void Addition_Full()
		{
			for (var i = 0; i < 100; ++i)
			{
				for (var j = 0; j < 100; ++j)
				{
					BooleanBigNumber first = BooleanBigNumber.FromInt(i);
					BooleanBigNumber second = BooleanBigNumber.FromInt(j);
					BooleanBigNumber result = first.Addition(second);
					CheckNumber(result, (byte) (i + j));
				}
			}
		}

		private static void CheckNumber(BooleanBigNumber number, params byte[] expectedNumbers)
		{
			TestHelper.CheckNumber(number, expectedNumbers);
		}
	}
}