using System;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Library.Tests.LongArithmetic {
	[TestFixture]
	public class BigNumber_ModTest {
		#region Power

		[Test]
		public void Power_FoundationIsNull() {
			var foundation = BigNumber.FromBytes(new[] { 0 });
			var number = BigNumber.FromBytes(new[] { 5 });
			var degree = BigNumber.FromBytes(new[] { 3 });
			Assert.Throws(typeof(DivideByZeroException), () => number.Power(degree, foundation));
		}

		[Test]
		public void Power_NumberIsZero() {
			var foundation = BigNumber.FromBytes(new[] { 25 });
			var number = BigNumber.FromBytes(new[] { 0 });
			var degree = BigNumber.FromBytes(new[] { 3 });
			var result = number.Power(degree, foundation);
			CheckNumber(result, 0);
		}

		[Test]
		public void Power1() {
			var foundation = BigNumber.FromBytes(new[] { 10 });
			var number = BigNumber.FromBytes(new[] { 5 });
			var degree = BigNumber.FromBytes(new[] { 3 });
			var result = number.Power(degree, foundation);
			CheckNumber(result, 5);
		}

		[Test]
		public void Power2() {
			var foundation = BigNumber.FromBytes(new[] { 25 });
			var number = BigNumber.FromBytes(new[] { 5 });
			var degree = BigNumber.FromBytes(new[] { 3 });
			var result = number.Power(degree, foundation);
			CheckNumber(result, 0);
		}

		[Test]
		public void Power3() {
			var foundation = BigNumber.FromBytes(new[] { 147, 1 });
			var number = BigNumber.FromBytes(new[] { 0, 1 });
			var degree = BigNumber.FromBytes(new[] { 2 });
			var result = number.Power(degree, foundation);
			CheckNumber(result, 250);
		}

		[Test]
		public void Power4() {
			var foundation = BigNumber.FromBytes(new[] { 147, 1 });
			var number = BigNumber.FromBytes(new[] { 0, 1 });
			var degree = BigNumber.FromBytes(new[] { 1 });
			var result = number.Power(degree, foundation);
			CheckNumber(result, 0, 1);
		}

		[Test]
		public void Power5() {
			var foundation = BigNumber.FromBytes(new[] { 147, 1 }); // 403
			var number = BigNumber.FromBytes(new[] { 180, 2 });
			var degree = BigNumber.FromBytes(new[] { 1 });
			var result = number.Power(degree, foundation);
			CheckNumber(result, 33, 1);
		}

		#endregion

		#region Power

		[Test]
		public void FastPower_FoundationIsNull() {
			var number = BigNumber.FromBytes(new[] { 5 });
			var degree = new[] { true, true };
			var foundation = BigNumber.FromBytes(new[] { 0 });
			Assert.Throws(typeof(DivideByZeroException), () => number.Power(degree, foundation));
		}

		[Test]
		public void FastPower_NumberIsZero() {
			var number = BigNumber.FromBytes(new[] { 0 });
			var degree = new[] { true, true };
			var foundation = BigNumber.FromBytes(new[] { 25 });
			var result = number.Power(degree, foundation);
			CheckNumber(result, 0);
		}

		[Test]
		public void FastPower1() {
			var number = BigNumber.FromBytes(new[] { 5 });
			var degree = new[] { true, true };
			var foundation = BigNumber.FromBytes(new[] { 10 });
			var result = number.Power(degree, foundation);
			CheckNumber(result, 5);
		}

		[Test]
		public void FastPower2() {
			var number = BigNumber.FromBytes(new[] { 5 });
			var degree = new[] { true, true };
			var foundation = BigNumber.FromBytes(new[] { 25 });
			var result = number.Power(degree, foundation);
			CheckNumber(result, 0);
		}

		[Test]
		public void FastPower3() {
			var number = BigNumber.FromBytes(new[] { 0, 1 });
			var degree = new[] { false, true };
			var foundation = BigNumber.FromBytes(new[] { 147, 1 });
			var result = number.Power(degree, foundation);
			CheckNumber(result, 250);
		}

		[Test]
		public void FastPower4() {
			var number = BigNumber.FromBytes(new[] { 0, 1 });
			var degree = new[] { true };
			var foundation = BigNumber.FromBytes(new[] { 147, 1 });
			var result = number.Power(degree, foundation);
			CheckNumber(result, 0, 1);
		}

		[Test]
		public void FastPower5() {
			var number = BigNumber.FromBytes(new[] { 180, 2 });
			var degree = new[] { true };
			var foundation = BigNumber.FromBytes(new[] { 147, 1 }); // 403
			var result = number.Power(degree, foundation);
			CheckNumber(result, 33, 1);
		}

		#endregion

		private static void CheckNumber(BigNumber number, params int[] expectedNumbers) {
			var numbers = number.Numbers;
			Assert.AreEqual(expectedNumbers.Length, numbers.Count, "Разрядность чисел не совпадает");
			for (var i = 0; i < expectedNumbers.Length; ++i) {
				Assert.AreEqual(expectedNumbers[i], numbers[i], "Ошибка в разряде :" + i);
			}
		}
	}
}