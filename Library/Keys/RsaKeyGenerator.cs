using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Library.Keys {
	public class RsaKeyGenerator {
		private static readonly BigNumber one = BigNumber.FromInt(1);
		private static readonly BigNumber two = BigNumber.FromInt(2);

		public RsaKey Generate(BigNumber p, BigNumber q, BigNumber startE) {
			var euler = Euler(p, q);
			var e = CalcE(euler, startE);
			var d = CalcD(euler, e);
			var n = p.Multiplication(q);
			var publicKey = new PublicKey(e, n);
			var privateKey = new PrivateKey(d, n);
			return new RsaKey(publicKey, privateKey);
		}

		private static BigNumber CalcE(BigNumber euler, BigNumber e) {
			while (euler.Mod(e).IsZero || !CheckPrime(e)) {
				e = e.Addition(two);
			}
			return e;
		}

		private static BigNumber CalcD(BigNumber euler, BigNumber e) {
			var t = euler.Clone();
			t.Increment();
			while (!t.Mod(e).IsZero) {
				t = t.Addition(euler);
			}
			return t.Division(e);
		}

		private static BigNumber Euler(BigNumber p, BigNumber q) {
			var first = p.Subtraction(one);
			var second = q.Subtraction(one);
			return first.Multiplication(second);
		}

		private static bool CheckPrime(BigNumber number) {
			var last = number.Division(two);
			for (var i = two.Clone(); i.Compare(last) == CompareResult.Less; i.Increment()) {
				if (number.Mod(i).IsZero) {
					return false;
				}
			}
			return true;
		}
	}
}