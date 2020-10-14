using System;
using CryptoFile.Library;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Client.Serialization {
	class KeySerializer {
		private readonly IBigNumberSerializer serializer;

		/// <exception cref="ArgumentNullException">serializer is null</exception>
		public KeySerializer(IBigNumberSerializer serializer) {
			Checker.CheckNull(serializer);
			this.serializer = serializer;
		}

		/// <exception cref="ArgumentNullException">key is null</exception>
		public string SerializePublicKey(PublicKey key) {
			Checker.CheckNull(key);
			return SerializeKey(key.E, key.N);
		}

		/// <exception cref="ArgumentNullException">key is null</exception>
		public string SerializePrivateKey(PrivateKey key) {
			Checker.CheckNull(key);
			return SerializeKey(key.D, key.N);
		}

		/// <exception cref="ArgumentNullException">line is null</exception>
		/// <exception cref="ArgumentException">line is empty</exception>
		/// <exception cref="KeySerializationException">Ошибка при десериализации ключа</exception>
		public PublicKey DeserializePublicKey(string line) {
			BigNumber e;
			BigNumber n;
			DeserializeKey(line, out e, out n);
			return new PublicKey(e, n);
		}

		/// <exception cref="ArgumentNullException">line is null</exception>
		/// <exception cref="ArgumentException">line is empty</exception>
		/// <exception cref="KeySerializationException">Ошибка при десериализации ключа</exception>
		public PrivateKey DeserializePrivateKey(string line) {
			BigNumber d;
			BigNumber n;
			DeserializeKey(line, out d, out n);
			return new PrivateKey(d, n);
		}

		private string SerializeKey(BigNumber first, BigNumber second) {
			return string.Format("{0}#{1}", Serialize(first), Serialize(second));
		}

		private string Serialize(BigNumber number) {
			return serializer.Serialize(number);
		}

		/// <exception cref="ArgumentNullException">line is null</exception>
		/// <exception cref="ArgumentException">line is empty</exception>
		/// <exception cref="KeySerializationException">Ошибка при десериализации ключа</exception>
		private void DeserializeKey(string line, out BigNumber first, out BigNumber second) {
			Checker.CheckString(line);
			var buff = line.Split('#');
			if (buff.Length != 2) {
				throw new KeySerializationException("parts not equal 2.");
			}
			if (string.IsNullOrEmpty(buff[0])) {
				throw new KeySerializationException("left part is empty.");
			}
			if (string.IsNullOrEmpty(buff[1])) {
				throw new KeySerializationException("right part is empty.");
			}
			try {
				first = Deserialize(buff[0]);
				second = Deserialize(buff[1]);
			} catch (BigNumberFormatException e) {
				throw new KeySerializationException("Ошибка при десериализации числа", e);
			}
		}

		/// <exception cref="ArgumentNullException">line is null</exception>
		/// <exception cref="ArgumentException">line is empty</exception>
		/// <exception cref="BigNumberFormatException">line содержит ошибки</exception>
		private BigNumber Deserialize(string line) {
			return serializer.Deserialize(line);
		}
	}
}