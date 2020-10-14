using System;
using CryptoFile.Library.LongArithmetic;
using CryptoFile.Library.Prime;
using NUnit.Framework;

namespace CryptoFile.Library.Tests.Prime {
	[TestFixture]
	public class RabinMillerTest_Test {
		#region CheckPrimality

		[Test]
		public void CheckPrimality_NumberIsNull() {
			var test = new RabinMillerTest(2);
			Assert.Throws(typeof(ArgumentNullException), () => test.CheckPrimality(null));
		}

		[Test]
		public void CheckPrimality1() {
			var test = new RabinMillerTest(CreateNumber(2));
			var number = CreateNumber(7);
			Assert.IsTrue(test.CheckPrimality(number));
		}

		[Test]
		public void CheckPrimality2() {
			var test = new RabinMillerTest(CreateNumber(3));
			var number = CreateNumber(7);
			Assert.IsTrue(test.CheckPrimality(number));
		}

		[Test]
		public void CheckPrimality3() {
			var test = new RabinMillerTest(CreateNumber(2));
			var number = CreateNumber(37);
			Assert.IsTrue(test.CheckPrimality(number));
		}

		[Test]
		public void CheckPrimality_NumberIs5() {
			var test = new RabinMillerTest(3);
			var number = CreateNumber(5);
			Assert.IsTrue(test.CheckPrimality(number));
		}

		[Test]
		public void CheckPrimality_NumberIs6() {
			var test = new RabinMillerTest(4);
			var number = CreateNumber(6);
			Assert.IsFalse(test.CheckPrimality(number));
		}

		[Test]
		public void CheckPrimality_NumberIs257() {
			var test = new RabinMillerTest(255);
			var number = CreateNumber(1, 1);
			Assert.IsTrue(test.CheckPrimality(number));
		}

		[Test]
		public void CheckPrimality_NumberIs449() {
			var test = new RabinMillerTest(447);
			var number = CreateNumber(193, 1);
			Assert.IsTrue(test.CheckPrimality(number));
		}

		#endregion

		private static BigNumber CreateNumber(params int[] numbers) {
			return BigNumber.FromBytes(numbers);
		}
	}
}