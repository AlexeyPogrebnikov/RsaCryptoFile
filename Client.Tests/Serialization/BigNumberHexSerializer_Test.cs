using System;
using CryptoFile.Client.Serialization;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Client.Tests.Serialization {
	[TestFixture]
	public class BigNumberHexSerializer_Test {
		private BigNumberHexSerializer serializer;

		[SetUp]
		public void SetUp() {
			serializer = new BigNumberHexSerializer();
		}

		#region Serialize

		[Test]
		public void Serialize_NumberIsNull() {
			Assert.Throws(typeof(ArgumentNullException), () => serializer.Serialize(null));
		}

		[Test]
		public void Serialize0() {
			var number = CreateNumber(0);
			Assert.AreEqual("00", serializer.Serialize(number));
		}

		[Test]
		public void Serialize0_1() {
			var number = CreateNumber(0, 1);
			Assert.AreEqual("0100", serializer.Serialize(number));
		}

		[Test]
		public void Serialize1() {
			var number = CreateNumber(1);
			Assert.AreEqual("01", serializer.Serialize(number));
		}

		[Test]
		public void Serialize10() {
			var number = CreateNumber(10);
			Assert.AreEqual("0A", serializer.Serialize(number));
		}

		[Test]
		public void Serialize11() {
			var number = CreateNumber(11);
			Assert.AreEqual("0B", serializer.Serialize(number));
		}

		[Test]
		public void Serialize12() {
			var number = CreateNumber(12);
			Assert.AreEqual("0C", serializer.Serialize(number));
		}

		[Test]
		public void Serialize13() {
			var number = CreateNumber(13);
			Assert.AreEqual("0D", serializer.Serialize(number));
		}

		[Test]
		public void Serialize139() {
			var number = CreateNumber(139);
			Assert.AreEqual("8B", serializer.Serialize(number));
		}

		[Test]
		public void Serialize14() {
			var number = CreateNumber(14);
			Assert.AreEqual("0E", serializer.Serialize(number));
		}

		[Test]
		public void Serialize15() {
			var number = CreateNumber(15);
			Assert.AreEqual("0F", serializer.Serialize(number));
		}

		[Test]
		public void Serialize16() {
			var number = CreateNumber(16);
			Assert.AreEqual("10", serializer.Serialize(number));
		}

		[Test]
		public void Serialize17() {
			var number = CreateNumber(17);
			Assert.AreEqual("11", serializer.Serialize(number));
		}

		[Test]
		public void Serialize18() {
			var number = CreateNumber(18);
			Assert.AreEqual("12", serializer.Serialize(number));
		}

		[Test]
		public void Serialize19() {
			var number = CreateNumber(19);
			Assert.AreEqual("13", serializer.Serialize(number));
		}

		[Test]
		public void Serialize2() {
			var number = CreateNumber(2);
			Assert.AreEqual("02", serializer.Serialize(number));
		}

		[Test]
		public void Serialize255_111_67() {
			var number = CreateNumber(255, 111, 67);
			Assert.AreEqual("436FFF", serializer.Serialize(number));
		}

		[Test]
		public void Serialize3() {
			var number = CreateNumber(3);
			Assert.AreEqual("03", serializer.Serialize(number));
		}

		[Test]
		public void Serialize4() {
			var number = CreateNumber(4);
			Assert.AreEqual("04", serializer.Serialize(number));
		}

		[Test]
		public void Serialize5() {
			var number = CreateNumber(5);
			Assert.AreEqual("05", serializer.Serialize(number));
		}

		[Test]
		public void Serialize5_9() {
			var number = CreateNumber(5, 9);
			Assert.AreEqual("0905", serializer.Serialize(number));
		}

		[Test]
		public void Serialize6() {
			var number = CreateNumber(6);
			Assert.AreEqual("06", serializer.Serialize(number));
		}

		[Test]
		public void Serialize7() {
			var number = CreateNumber(7);
			Assert.AreEqual("07", serializer.Serialize(number));
		}

		[Test]
		public void Serialize8() {
			var number = CreateNumber(8);
			Assert.AreEqual("08", serializer.Serialize(number));
		}

		[Test]
		public void Serialize9() {
			var number = CreateNumber(9);
			Assert.AreEqual("09", serializer.Serialize(number));
		}

		#endregion

		#region Deserialize

		[Test]
		public void Deserialize_LineIsNull() {
			Assert.Throws(typeof(ArgumentNullException), () => serializer.Deserialize(null));
		}

		[Test]
		public void Deserialize_LineIsEmpty() {
			Assert.Throws(typeof(ArgumentException), () => serializer.Deserialize(""));
		}

		[Test]
		public void Deserialize_LineLengthIsOdd() {
			Assert.Throws(typeof(BigNumberFormatException), () => serializer.Deserialize("0"));
		}

		[Test]
		public void Deserialize_LineHasOtherSymbols() {
			Assert.Throws(typeof(BigNumberFormatException), () => serializer.Deserialize("tt"));
		}

		[Test]
		public void Deserialize0() {
			var number = serializer.Deserialize("00");
			CheckNumber(number, 0);
		}

		[Test]
		public void Deserialize0100() {
			var number = serializer.Deserialize("0100");
			CheckNumber(number, 0, 1);
		}

		[Test]
		public void Deserialize1() {
			var number = serializer.Deserialize("01");
			CheckNumber(number, 1);
		}

		[Test]
		public void Deserialize2() {
			var number = serializer.Deserialize("02");
			CheckNumber(number, 2);
		}

		[Test]
		public void Deserialize2F() {
			var number = serializer.Deserialize("2F");
			CheckNumber(number, 47);
		}

		[Test]
		public void Deserialize2F_Lowercase() {
			var number = serializer.Deserialize("2f");
			CheckNumber(number, 47);
		}

		[Test]
		public void Deserialize3() {
			var number = serializer.Deserialize("03");
			CheckNumber(number, 3);
		}

		[Test]
		public void Deserialize4() {
			var number = serializer.Deserialize("04");
			CheckNumber(number, 4);
		}

		[Test]
		public void Deserialize5() {
			var number = serializer.Deserialize("05");
			CheckNumber(number, 5);
		}

		[Test]
		public void Deserialize6() {
			var number = serializer.Deserialize("06");
			CheckNumber(number, 6);
		}

		[Test]
		public void Deserialize62() {
			var number = serializer.Deserialize("62");
			CheckNumber(number, 98);
		}

		[Test]
		public void Deserialize7() {
			var number = serializer.Deserialize("07");
			CheckNumber(number, 7);
		}

		[Test]
		public void Deserialize8() {
			var number = serializer.Deserialize("08");
			CheckNumber(number, 8);
		}

		[Test]
		public void Deserialize9() {
			var number = serializer.Deserialize("09");
			CheckNumber(number, 9);
		}

		[Test]
		public void DeserializeA() {
			var number = serializer.Deserialize("0A");
			CheckNumber(number, 10);
		}

		[Test]
		public void DeserializeA_Lowercase() {
			var number = serializer.Deserialize("0a");
			CheckNumber(number, 10);
		}

		[Test]
		public void DeserializeB() {
			var number = serializer.Deserialize("0B");
			CheckNumber(number, 11);
		}

		[Test]
		public void DeserializeB_Lowercase() {
			var number = serializer.Deserialize("0b");
			CheckNumber(number, 11);
		}

		[Test]
		public void DeserializeC() {
			var number = serializer.Deserialize("0C");
			CheckNumber(number, 12);
		}

		[Test]
		public void DeserializeC_Lowercase() {
			var number = serializer.Deserialize("0c");
			CheckNumber(number, 12);
		}

		[Test]
		public void DeserializeD() {
			var number = serializer.Deserialize("0D");
			CheckNumber(number, 13);
		}

		[Test]
		public void DeserializeD_Lowercase() {
			var number = serializer.Deserialize("0d");
			CheckNumber(number, 13);
		}

		[Test]
		public void DeserializeE() {
			var number = serializer.Deserialize("0E");
			CheckNumber(number, 14);
		}

		[Test]
		public void DeserializeE_Lowercase() {
			var number = serializer.Deserialize("0e");
			CheckNumber(number, 14);
		}

		[Test]
		public void DeserializeF() {
			var number = serializer.Deserialize("0F");
			CheckNumber(number, 15);
		}

		[Test]
		public void DeserializeF_Lowercase() {
			var number = serializer.Deserialize("0f");
			CheckNumber(number, 15);
		}

		[Test]
		public void DeserializeFE1497() {
			var number = serializer.Deserialize("FE1497");
			CheckNumber(number, 151, 20, 254);
		}

		[Test]
		public void DeserializeFE1497_Lowercase() {
			var number = serializer.Deserialize("fe1497");
			CheckNumber(number, 151, 20, 254);
		}

		#endregion

		private static BigNumber CreateNumber(params int[] numbers) {
			return BigNumber.FromBytes(numbers);
		}

		private static void CheckNumber(BigNumber number, params int[] expectedNumbers) {
			TestHelper.CheckNumber(number, expectedNumbers);
		}
	}
}