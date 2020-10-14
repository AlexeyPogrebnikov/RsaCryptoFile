using CryptoFile.Library.LongArithmetic;
using CryptoFile.Library.Prime;
using NUnit.Framework;

namespace CryptoFile.Library.Tests.Prime {
	[TestFixture]
	public class MultiplicityChecker_Test {
		[Test]
		public void Check_MaxFactorIs4_NumberIs49() {
			var checker = new MultiplicityChecker(4);
			var number = BigNumber.FromInt(49);
			Assert.IsFalse(checker.Check(number));
		}

		[Test]
		public void Check_MaxFactorIs2_NumberIs4() {
			var checker = new MultiplicityChecker(2);
			var number = BigNumber.FromInt(4);
			Assert.IsTrue(checker.Check(number));
		}

		[Test]
		public void Check_MaxFactorIs3_NumberIs9() {
			var checker = new MultiplicityChecker(3);
			var number = BigNumber.FromInt(9);
			Assert.IsTrue(checker.Check(number));
		}

		[Test]
		public void Check_MaxFactorIs2_NumberIs2() {
			var checker = new MultiplicityChecker(2);
			var number = BigNumber.FromInt(2);
			Assert.IsFalse(checker.Check(number));
		}
	}
}