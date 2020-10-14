using System;
using CryptoFile.Library.LongArithmetic;
using CryptoFile.Library.Prime;
using NUnit.Framework;

namespace CryptoFile.Library.Tests.Prime {
	[TestFixture]
	public class PrimeGenerator_Test {
		private PrimeGenerator generator;

		[SetUp]
		public void SetUp() {
			generator = new PrimeGenerator(new RabinMillerTest(20));
		}

		#region Constructor

		[Test]
		public void Constructor_PrimeTestIsNull() {
			Assert.Throws(typeof(ArgumentNullException), () => new PrimeGenerator(null));
		}

		#endregion

		#region GenerateFrom

		[Test]
		public void GenerateFrom_NumberIsNull() {
			Assert.Throws(typeof(ArgumentNullException), () => generator.GenerateFrom(null));
		}

		[Test]
		public void GenerateFrom_NumberIsEven() {
			generator = new PrimeGenerator(new RabinMillerTest(BigNumber.FromBytes(new[] { 2 })));
			var number = BigNumber.FromBytes(new[] { 14 });
			var simple = generator.GenerateFrom(number);
			CheckNumber(simple, 17);
		}

		[Test]
		public void GenerateFrom_StartNumberIs15() {
			generator = new PrimeGenerator(new RabinMillerTest(CreateWitnesses(2, 14)));
			var number = BigNumber.FromBytes(new[] { 15 });
			var simple = generator.GenerateFrom(number);
			CheckNumber(simple, 17);
		}

		[Test]
		public void GenerateFrom_StartNumberIs45() {
			generator = new PrimeGenerator(new RabinMillerTest(CreateWitnesses(2, 44)));
			var number = BigNumber.FromBytes(new[] { 45 });
			var simple = generator.GenerateFrom(number);
			CheckNumber(simple, 47);
		}

		[Test]
		public void GenerateFrom_StartNumberIs133() {
			generator = new PrimeGenerator(new RabinMillerTest(CreateWitnesses(2, 132)));
			var number = BigNumber.FromBytes(new[] { 133 });
			var simple = generator.GenerateFrom(number);
			CheckNumber(simple, 137);
		}

		#endregion

		private static void CheckNumber(BigNumber number, params int[] expectedNumbers) {
			var numbers = number.Numbers;
			Assert.AreEqual(expectedNumbers.Length, numbers.Count, "Разрядность чисел не совпадает");
			for (var i = 0; i < expectedNumbers.Length; ++i) {
				Assert.AreEqual(expectedNumbers[i], numbers[i], "Ошибка в разряде :" + i);
			}
		}

		private static BigNumber[] CreateWitnesses(int first, int last) {
			var witnesses = new BigNumber[last - first];
			for (var i = 0; i < witnesses.Length; ++i) {
				witnesses[i] = BigNumber.FromInt(i + first);
			}
			return witnesses;
		}
	}
}