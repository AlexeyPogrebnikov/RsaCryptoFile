using System;
using System.IO;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.IO.Writing {
	public class RsaFileWriter : IRsaFileWriter {
		private readonly byte version;
		private readonly short blockLength;
		private readonly FileStream stream;

		public RsaFileWriter(byte version, string fileName, short blockLength) {
			this.version = version;
			this.blockLength = blockLength;
			stream = new FileStream(fileName, FileMode.Create);
		}

		public void WriteHeader(long sourceFileLength, byte[] hashCode) {
			stream.WriteByte(version);
			var blockLengthAsBytes = BitConverter.GetBytes(blockLength);
			stream.Write(blockLengthAsBytes, 0, 2);
			var sourceFileLengthAsBytes = BitConverter.GetBytes(sourceFileLength);
			stream.Write(sourceFileLengthAsBytes, 0, 8);
			stream.Write(hashCode, 0, hashCode.Length);
		}

		public void WriteNextBlock(BigNumber number) {
			if (number.Digit > blockLength)
				throw new ArgumentException("number.Digit > blockLength", "number");
			var bytes = new byte[blockLength];
			var numbers = number.Numbers;
			for (var i = 0; i < numbers.Count; ++i) {
				bytes[i] = (byte)numbers[i];
			}
			stream.Write(bytes, 0, bytes.Length);
		}

		public void Delete() {
			Dispose();
			File.Delete(stream.Name);
		}

		public void Dispose() {
			stream.Dispose();
		}
	}
}