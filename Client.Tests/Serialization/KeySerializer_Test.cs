using System;
using CryptoFile.Client.Serialization;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Serialization
{
	[TestFixture]
	public class KeySerializer_Test
	{
		private KeySerializer serializer;

		[SetUp]
		public void SetUp()
		{
			serializer = new KeySerializer(new BigNumberHexSerializer());
		}

		#region Constructor

		[Test]
		public void Constructor_SerializerIsNull()
		{
			Assert.Throws(typeof(ArgumentNullException), () => new KeySerializer(null));
		}

		#endregion

		#region SerializePublicKey

		[Test]
		public void SerializePublicKey_KeyIsNull()
		{
			Assert.Throws(typeof(ArgumentNullException), () => serializer.SerializePublicKey(null));
		}

		#endregion

		#region SerializePrivateKey

		[Test]
		public void SerializePrivateKey_KeyIsNull()
		{
			Assert.Throws(typeof(ArgumentNullException), () => serializer.SerializePrivateKey(null));
		}

		[Test]
		public void SerializePrivateKey1()
		{
			var key = new PrivateKey(CreateNumber(4), CreateNumber(7));
			string line = serializer.SerializePrivateKey(key);
			Assert.AreEqual("04#07", line);
		}

		[Test]
		public void SerializePrivateKey2()
		{
			var key = new PrivateKey(CreateNumber(35, 91), CreateNumber(82));
			string line = serializer.SerializePrivateKey(key);
			Assert.AreEqual("5B23#52", line);
		}

		[Test]
		public void SerializePublicKey1()
		{
			var key = new PublicKey(CreateNumber(2), CreateNumber(3));
			string line = serializer.SerializePublicKey(key);
			Assert.AreEqual("02#03", line);
		}

		[Test]
		public void SerializePublicKey2()
		{
			var key = new PublicKey(CreateNumber(10), CreateNumber(13));
			string line = serializer.SerializePublicKey(key);
			Assert.AreEqual("0A#0D", line);
		}

		#endregion

		#region DeserializePublicKey

		[Test]
		public void DeserializePublicKey_LineIsNull()
		{
			Assert.Throws(typeof(ArgumentNullException), () => serializer.DeserializePublicKey(null));
		}

		[Test]
		public void DeserializePublicKey_LineIsEmpty()
		{
			Assert.Throws(typeof(ArgumentException), () => serializer.DeserializePublicKey(""));
		}

		[Test]
		public void DeserializePublicKey_LineHasErrors1()
		{
			Assert.Throws(typeof(KeySerializationException), () => serializer.DeserializePublicKey("QWERTY"));
		}

		[Test]
		public void DeserializePublicKey_LineHasErrors2()
		{
			Assert.Throws(typeof(KeySerializationException), () => serializer.DeserializePublicKey("10#10#10"));
		}

		[Test]
		public void DeserializePublicKey_LeftPartIsEmpty()
		{
			Assert.Throws(typeof(KeySerializationException), () => serializer.DeserializePublicKey("#10"));
		}

		[Test]
		public void DeserializePublicKey_RightPartIsEmpty()
		{
			Assert.Throws(typeof(KeySerializationException), () => serializer.DeserializePublicKey("10#"));
		}

		[Test]
		public void DeserializePublicKey_LeftPartHasErrors()
		{
			Assert.Throws(typeof(KeySerializationException), () => serializer.DeserializePublicKey("w0#18"));
		}

		[Test]
		public void DeserializePublicKey_RightPartHasErrors()
		{
			Assert.Throws(typeof(KeySerializationException), () => serializer.DeserializePublicKey("34#ew"));
		}

		[Test]
		public void DeserializePublicKey1()
		{
			PublicKey key = serializer.DeserializePublicKey("73#28");
			CheckNumber(key.E, 115);
			CheckNumber(key.N, 40);
		}

		[Test]
		public void DeserializePublicKey2()
		{
			PublicKey key = serializer.DeserializePublicKey("F208#1274E9");
			CheckNumber(key.E, 8, 242);
			CheckNumber(key.N, 233, 116, 18);
		}

		#endregion

		#region DeserializePrivateKey

		[Test]
		public void DeserializePrivateKey_LineIsNull()
		{
			Assert.Throws(typeof(ArgumentNullException), () => serializer.DeserializePrivateKey(null));
		}

		[Test]
		public void DeserializePrivateKey_LineIsEmpty()
		{
			Assert.Throws(typeof(ArgumentException), () => serializer.DeserializePrivateKey(""));
		}

		[Test]
		public void DeserializePrivate_LineHasErrors1()
		{
			Assert.Throws(typeof(KeySerializationException), () => serializer.DeserializePrivateKey("QWERTY"));
		}

		[Test]
		public void DeserializePrivateKey_LeftPartIsEmpty()
		{
			Assert.Throws(typeof(KeySerializationException), () => serializer.DeserializePrivateKey("#10"));
		}

		[Test]
		public void DeserializePrivateKey_RightPartIsEmpty()
		{
			Assert.Throws(typeof(KeySerializationException), () => serializer.DeserializePrivateKey("10#"));
		}

		[Test]
		public void DeserializePrivateKey_LeftPartHasErrors()
		{
			Assert.Throws(typeof(KeySerializationException), () => serializer.DeserializePrivateKey("w0#18"));
		}

		[Test]
		public void DeserializePrivateKey_RightPartHasErrors()
		{
			Assert.Throws(typeof(KeySerializationException), () => serializer.DeserializePrivateKey("34#ew"));
		}

		#endregion

		private static BigNumber CreateNumber(params int[] numbers)
		{
			return BigNumber.FromBytes(numbers);
		}

		private static void CheckNumber(BigNumber number, params int[] expectedNumbers)
		{
			TestHelper.CheckNumber(number, expectedNumbers);
		}
	}
}