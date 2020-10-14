using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Library.Tests.LongArithmetic {
	[TestFixture]
	public class BooleanBigNumber_Test {
		[Test]
		public void GetBytes_NumberIs0() {
			var number = BooleanBigNumber.FromInt(0);
			CheckNumber(number, 0);
		}

		[Test]
		public void GetBytes_NumberIs1() {
			var number = BooleanBigNumber.FromInt(1);
			CheckNumber(number, 1);
		}

		[Test]
		public void GetBytes_NumberIs2() {
			var number = BooleanBigNumber.FromInt(2);
			CheckNumber(number, 2);
		}

		[Test]
		public void GetBytes_NumberIs256() {
			var number = BooleanBigNumber.FromInt(256);
			CheckNumber(number, 0, 1);
		}

		[Test]
		public void GetBytes_NumberIs128() {
			var number = BooleanBigNumber.FromInt(128);
			CheckNumber(number, 128);
		}

		[Test]
		public void Addition_FirstIs0_SecondIs0() {
			var first = BooleanBigNumber.FromInt(0);
			var second = BooleanBigNumber.FromInt(0);
			var result = first.Addition(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Addition_FirstIs1_SecondIs0() {
			var first = BooleanBigNumber.FromInt(1);
			var second = BooleanBigNumber.FromInt(0);
			var result = first.Addition(second);
			CheckNumber(result, 1);
		}

		[Test]
		public void Addition_FirstIs1_SecondIs1() {
			var first = BooleanBigNumber.FromInt(1);
			var second = BooleanBigNumber.FromInt(1);
			var result = first.Addition(second);
			CheckNumber(result, 2);
		}

		[Test]
		public void Addition_FirstIs2_SecondIs3() {
			var first = BooleanBigNumber.FromInt(2);
			var second = BooleanBigNumber.FromInt(3);
			var result = first.Addition(second);
			CheckNumber(result, 5);
		}

		[Test]
		public void Addition_FirstIs5_SecondIs7() {
			var first = BooleanBigNumber.FromInt(5);
			var second = BooleanBigNumber.FromInt(7);
			var result = first.Addition(second);
			CheckNumber(result, 12);
		}

		[Test]
		public void Addition_FirstIs3_SecondIs3() {
			var first = BooleanBigNumber.FromInt(3);
			var second = BooleanBigNumber.FromInt(3);
			var result = first.Addition(second);
			CheckNumber(result, 6);
		}

		[Test]
		public void Addition_FirstIs2_SecondIs2() {
			var first = BooleanBigNumber.FromInt(2);
			var second = BooleanBigNumber.FromInt(2);
			var result = first.Addition(second);
			CheckNumber(result, 4);
		}

		[Test]
		public void Addition_Full() {
			for (var i = 0; i < 100; ++i) {
				for (var j = 0; j < 100; ++j) {
					var first = BooleanBigNumber.FromInt(i);
					var second = BooleanBigNumber.FromInt(j);
					var result = first.Addition(second);
					CheckNumber(result, (byte)(i + j));
				}
			}
		}

		private static void CheckNumber(BooleanBigNumber number, params byte[] expectedNumbers) {
			TestHelper.CheckNumber(number, expectedNumbers);
		}
	}
}