using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Library.Prime {
	public interface IPrimeTest {
		bool CheckPrimality(BigNumber number);
	}
}