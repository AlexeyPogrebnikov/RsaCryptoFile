using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Client.Crypto {
	public interface IKeyGenerator {
		RsaKey Generate(int length, BigNumber e);
		void Stop();
		ProcessStatus Status { get; }
	}
}