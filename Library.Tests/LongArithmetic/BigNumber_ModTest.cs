using System;
using System.Collections.Generic;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Library.Tests.LongArithmetic
{
	[TestFixture]
	public class BigNumber_ModTest
	{
		#region Power

		[Test]
		public void Power_FoundationIsNull()
		{
			BigNumber foundation = BigNumber.FromBytes(new[] { 0 });
			BigNumber number = BigNumber.FromBytes(new[] { 5 });
			BigNumber degree = BigNumber.FromBytes(new[] { 3 });
			Assert.Throws(typeof(DivideByZeroException), () => number.Power(degree, foundation));
		}

		[Test]
		public void Power_NumberIsZero()
		{
			BigNumber foundation = BigNumber.FromBytes(new[] { 25 });
			BigNumber number = BigNumber.FromBytes(new[] { 0 });
			BigNumber degree = BigNumber.FromBytes(new[] { 3 });
			BigNumber result = number.Power(degree, foundation);
			CheckNumber(result, 0);
		}

		[Test]
		public void Power1()
		{
			BigNumber foundation = BigNumber.FromBytes(new[] { 10 });
			BigNumber number = BigNumber.FromBytes(new[] { 5 });
			BigNumber degree = BigNumber.FromBytes(new[] { 3 });
			BigNumber result = number.Power(degree, foundation);
			CheckNumber(result, 5);
		}

		[Test]
		public void Power2()
		{
			BigNumber foundation = BigNumber.FromBytes(new[] { 25 });
			BigNumber number = BigNumber.FromBytes(new[] { 5 });
			BigNumber degree = BigNumber.FromBytes(new[] { 3 });
			BigNumber result = number.Power(degree, foundation);
			CheckNumber(result, 0);
		}

		[Test]
		public void Power3()
		{
			BigNumber foundation = BigNumber.FromBytes(new[] { 147, 1 });
			BigNumber number = BigNumber.FromBytes(new[] { 0, 1 });
			BigNumber degree = BigNumber.FromBytes(new[] { 2 });
			BigNumber result = number.Power(degree, foundation);
			CheckNumber(result, 250);
		}

		[Test]
		public void Power4()
		{
			BigNumber foundation = BigNumber.FromBytes(new[] { 147, 1 });
			BigNumber number = BigNumber.FromBytes(new[] { 0, 1 });
			BigNumber degree = BigNumber.FromBytes(new[] { 1 });
			BigNumber result = number.Power(degree, foundation);
			CheckNumber(result, 0, 1);
		}

		[Test]
		public void Power5()
		{
			BigNumber foundation = BigNumber.FromBytes(new[] { 147, 1 }); // 403
			BigNumber number = BigNumber.FromBytes(new[] { 180, 2 });
			BigNumber degree = BigNumber.FromBytes(new[] { 1 });
			BigNumber result = number.Power(degree, foundation);
			CheckNumber(result, 33, 1);
		}

		#endregion

		#region Power

		[Test]
		public void FastPower_FoundationIsNull()
		{
			BigNumber number = BigNumber.FromBytes(new[] { 5 });
			var degree = new[] { true, true };
			BigNumber foundation = BigNumber.FromBytes(new[] { 0 });
			Assert.Throws(typeof(DivideByZeroException), () => number.Power(degree, foundation));
		}

		[Test]
		public void FastPower_NumberIsZero()
		{
			BigNumber number = BigNumber.FromBytes(new[] { 0 });
			var degree = new[] { true, true };
			BigNumber foundation = BigNumber.FromBytes(new[] { 25 });
			BigNumber result = number.Power(degree, foundation);
			CheckNumber(result, 0);
		}

		[Test]
		public void FastPower1()
		{
			BigNumber number = BigNumber.FromBytes(new[] { 5 });
			var degree = new[] { true, true };
			BigNumber foundation = BigNumber.FromBytes(new[] { 10 });
			BigNumber result = number.Power(degree, foundation);
			CheckNumber(result, 5);
		}

		[Test]
		public void FastPower2()
		{
			BigNumber number = BigNumber.FromBytes(new[] { 5 });
			var degree = new[] { true, true };
			BigNumber foundation = BigNumber.FromBytes(new[] { 25 });
			BigNumber result = number.Power(degree, foundation);
			CheckNumber(result, 0);
		}

		[Test]
		public void FastPower3()
		{
			BigNumber number = BigNumber.FromBytes(new[] { 0, 1 });
			var degree = new[] { false, true };
			BigNumber foundation = BigNumber.FromBytes(new[] { 147, 1 });
			BigNumber result = number.Power(degree, foundation);
			CheckNumber(result, 250);
		}

		[Test]
		public void FastPower4()
		{
			BigNumber number = BigNumber.FromBytes(new[] { 0, 1 });
			var degree = new[] { true };
			BigNumber foundation = BigNumber.FromBytes(new[] { 147, 1 });
			BigNumber result = number.Power(degree, foundation);
			CheckNumber(result, 0, 1);
		}

		[Test]
		public void FastPower5()
		{
			BigNumber number = BigNumber.FromBytes(new[] { 180, 2 });
			var degree = new[] { true };
			BigNumber foundation = BigNumber.FromBytes(new[] { 147, 1 }); // 403
			BigNumber result = number.Power(degree, foundation);
			CheckNumber(result, 33, 1);
		}

		#endregion

		private static void CheckNumber(BigNumber number, params int[] expectedNumbers)
		{
			IList<int> numbers = number.Numbers;
			Assert.AreEqual(expectedNumbers.Length, numbers.Count, "Разрядность чисел не совпадает");
			for (var i = 0; i < expectedNumbers.Length; ++i)
			{
				Assert.AreEqual(expectedNumbers[i], numbers[i], "Ошибка в разряде :" + i);
			}
		}
	}
}