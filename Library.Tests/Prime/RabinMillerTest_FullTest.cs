using CryptoFile.Library.LongArithmetic;
using CryptoFile.Library.Prime;
using NUnit.Framework;

namespace CryptoFile.Library.Tests.Prime
{
	[TestFixture]
	public class RabinMillerTest_FullTest
	{
		#region CheckPrimality

		[Test]
		public void CheckPrimality1()
		{
			for (var i = 5; i < 500; ++i)
			{
				var test = new RabinMillerTest(i - 2);
				bool result = test.CheckPrimality(BigNumber.FromInt(i));
				if (result != CheckPrimality(i))
				{
					Assert.Fail("Error: " + i);
				}
			}
		}

		private static bool CheckPrimality(int number)
		{
			for (var i = 2; i <= number / 2; ++i)
			{
				if (number % i == 0)
				{
					return false;
				}
			}

			return true;
		}

		#endregion
	}
}