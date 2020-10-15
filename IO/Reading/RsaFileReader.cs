using System;
using System.IO;
using CryptoFile.IO.Exceptions;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.IO.Reading
{
	public class RsaFileReader : IRsaFileReader
	{
		private readonly FileStream stream;
		private bool IsHeaderReaded;

		public RsaFileReader(string fileName)
		{
			stream = new FileStream(fileName, FileMode.Open);
		}

		public void Dispose()
		{
			stream.Dispose();
		}

		public BigNumber ReadNextBlock()
		{
			if (!IsHeaderReaded)
				throw new InvalidOperationException("Header is not read.");
			var buffer = new byte[BlockLength];
			stream.Read(buffer, 0, buffer.Length);
			int index = BlockLength - 1;
			while (index > 0 && buffer[index] == 0)
			{
				--index;
			}

			var numbers = new int[index + 1];
			for (var i = 0; i < numbers.Length; ++i)
			{
				numbers[i] = buffer[i];
			}

			return BigNumber.FromBytes(numbers);
		}

		public bool IsDone => stream.Position >= Length;

		public long Length => stream.Length;

		public void ReadHeader()
		{
			if (Length < 27)
				throw new SourceFileException("Source file is too short.");
			var bytes = new byte[27];
			stream.Read(bytes, 0, bytes.Length);
			Version = bytes[0];
			BlockLength = BitConverter.ToInt16(bytes, 1);
			SourceFileLength = BitConverter.ToInt64(bytes, 3);
			HashCode = new byte[16];
			Array.Copy(bytes, 11, HashCode, 0, HashCode.Length);
			IsHeaderReaded = true;
		}

		public byte Version { get; private set; }

		public short BlockLength { get; private set; }

		public long SourceFileLength { get; private set; }

		public byte[] HashCode { get; private set; }
	}
}