using System;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Library.Tests.LongArithmetic {
	[TestFixture]
	public class BigNumber_Test {
		#region Constructor

		[Test]
		public void Constructor() {
			var numbers = new[] { 1, 2, 3 };
			var number = BigNumber.FromBytes(numbers);
			numbers[0] = 4;
			CheckNumber(number, 1, 2, 3);
		}

		[Test]
		public void Constructor_NumbersIsEmpty() {
			Assert.Throws(typeof(ArgumentException), () => BigNumber.FromBytes(new int[0]));
		}

		[Test]
		public void Constructor_NumbersIsNull() {
			Assert.Throws(typeof(ArgumentNullException), () => CreateNumber(null));
		}

		[Test]
		public void Constructor_NumbersIsZero_LenghtIsTwo() {
			Assert.Throws(typeof(ArgumentException), () => CreateNumber(new[] { 0, 0 }));
		}

		#endregion

		#region Addition

		[Test]
		public void Addition_FirstNumberIs1_SecondNumberIs10() {
			var first = CreateNumber(new[] { 1 });
			var second = CreateNumber(new[] { 10 });
			var result = first.Addition(second);
			CheckNumber(result, 11);
		}

		[Test]
		public void Addition_FirstNumberIs255_SecondNumberIs1() {
			var first = CreateNumber(new[] { 255 });
			var second = CreateNumber(new[] { 1 });
			var result = first.Addition(second);
			CheckNumber(result, 0, 1);
		}

		[Test]
		public void Addition_FirstNumberIs12810_SecondNumberIs60() {
			var first = CreateNumber(new[] { 10, 50 }); // 12810
			var second = CreateNumber(new[] { 60 });
			var result = first.Addition(second);
			CheckNumber(result, 70, 50);
		}

		[Test]
		public void Addition_FirstNumberIs65535_SecondNumberIs257() {
			var first = CreateNumber(new[] { 255, 255 }); // 65535
			var second = CreateNumber(new[] { 1, 1 }); // 257
			var result = first.Addition(second);
			CheckNumber(result, 0, 1, 1);
		}

		[Test]
		public void Addition_FirstNumberIs255_SecondNumberIs655617() {
			var first = CreateNumber(new[] { 255 });
			var second = CreateNumber(new[] { 1, 1, 10 }); // 655617
			var result = first.Addition(second);
			CheckNumber(result, 0, 2, 10);
		}

		[Test]
		public void Addition_FirstNumberIs12445656_SecondNumberIs14472691() {
			var first = CreateNumber(new[] { 216, 231, 189 }); // 12445656
			var second = CreateNumber(new[] { 243, 213, 220 }); // 14472691
			var result = first.Addition(second);
			CheckNumber(result, 203, 189, 154, 1);
		}

		#endregion

		#region Subtraction

		[Test]
		public void Subtraction1() {
			var first = CreateNumber(new[] { 2 });
			var second = CreateNumber(new[] { 1 });
			var result = first.Subtraction(second);
			CheckNumber(result, 1);
		}

		[Test]
		public void Subtraction2() {
			var first = CreateNumber(new[] { 2 });
			var second = CreateNumber(new[] { 10 });
			var result = first.Subtraction(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Subtraction3() {
			var first = CreateNumber(new[] { 10, 45 });
			var second = CreateNumber(new[] { 10, 45 });
			var result = first.Subtraction(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Subtraction4() {
			var first = CreateNumber(new[] { 0, 1 });
			var second = CreateNumber(new[] { 50 });
			var result = first.Subtraction(second);
			CheckNumber(result, 206);
		}

		[Test]
		public void Subtraction5() {
			var first = CreateNumber(new[] { 4, 8, 7 });
			var second = CreateNumber(new[] { 1, 4 });
			var result = first.Subtraction(second);
			CheckNumber(result, 3, 4, 7);
		}

		[Test]
		public void Subtraction6() {
			var first = CreateNumber(new[] { 4, 8, 7 });
			var second = CreateNumber(new[] { 6, 9 });
			var result = first.Subtraction(second);
			CheckNumber(result, 254, 254, 6);
		}

		[Test]
		public void Subtraction7() {
			var first = CreateNumber(new[] { 17, 5, 6, 7 });
			var second = CreateNumber(new[] { 6, 5, 6, 7 });
			var result = first.Subtraction(second);
			CheckNumber(result, 11);
		}

		[Test]
		public void Subtraction8() {
			var first = CreateNumber(0, 0, 1);
			var second = CreateNumber(2);
			var result = first.Subtraction(second);
			CheckNumber(result, 254, 255);
		}

		#endregion

		#region Increment

		[Test]
		public void Increment1() {
			var number = CreateNumber(new[] { 1 });
			number.Increment();
			CheckNumber(number, 2);
		}

		[Test]
		public void Increment2() {
			var number = CreateNumber(new[] { 255 });
			number.Increment();
			CheckNumber(number, 0, 1);
		}

		[Test]
		public void Increment3() {
			var number = CreateNumber(new[] { 255, 255 });
			number.Increment();
			CheckNumber(number, 0, 0, 1);
		}

		[Test]
		public void Increment4() {
			var number = CreateNumber(new[] { 255, 255, 255 });
			number.Increment();
			CheckNumber(number, 0, 0, 0, 1);
		}

		[Test]
		public void Increment5() {
			var number = CreateNumber(new[] { 255, 10 });
			number.Increment();
			CheckNumber(number, 0, 11);
		}

		#endregion

		#region Decrement

		[Test]
		public void Decrement1() {
			var number = CreateNumber(new[] { 2 });
			number.Decrement();
			CheckNumber(number, 1);
		}

		[Test]
		public void Decrement2() {
			var number = CreateNumber(new[] { 0, 1 });
			number.Decrement();
			CheckNumber(number, 255);
		}

		[Test]
		public void Decrement3() {
			var number = CreateNumber(new[] { 23, 7 });
			number.Decrement();
			CheckNumber(number, 22, 7);
		}

		[Test]
		public void Decrement4() {
			var number = CreateNumber(new[] { 0, 0, 1 });
			number.Decrement();
			CheckNumber(number, 255, 255);
		}

		[Test]
		public void Decrement5() {
			var number = CreateNumber(new[] { 0, 4, 1 });
			number.Decrement();
			CheckNumber(number, 255, 3, 1);
		}

		#endregion

		#region Multiplication

		[Test]
		public void Multiplication1() {
			var first = CreateNumber(new[] { 10, 7 });
			var second = CreateNumber(new[] { 5, 3 });
			var result = first.Multiplication(second);
			CheckNumber(result, 50, 65, 21);
		}

		[Test]
		public void Multiplication2() {
			var first = CreateNumber(new[] { 10, 7 });
			var second = CreateNumber(new[] { 255 });
			var result = first.Multiplication(second);
			CheckNumber(result, 246, 2, 7);
		}

		[Test]
		public void Multiplication3() {
			var first = CreateNumber(new[] { 255, 255 });
			var second = CreateNumber(new[] { 255, 255 });
			var result = first.Multiplication(second);
			CheckNumber(result, 1, 0, 254, 255);
		}

		[Test]
		public void Multiplication4() {
			var first = CreateNumber(new[] { 0 });
			var second = CreateNumber(new[] { 8, 6 });
			var result = first.Multiplication(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Multiplication5() {
			var first = CreateNumber(new[] { 14, 56 });
			var second = CreateNumber(new[] { 0 });
			var result = first.Multiplication(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Multiplication6() {
			var first = CreateNumber(new[] { 12, 6, 9, 4 });
			var second = CreateNumber(new[] { 1 });
			var result = first.Multiplication(second);
			CheckNumber(result, 12, 6, 9, 4);
		}

		[Test]
		public void Multiplication7() {
			var first = CreateNumber(new[] { 1 });
			var second = CreateNumber(new[] { 12, 6, 9, 4 });
			var result = first.Multiplication(second);
			CheckNumber(result, 12, 6, 9, 4);
		}

		[Test]
		public void Multiplication8() {
			var first = CreateNumber(new[] { 60, 2 });
			var second = CreateNumber(new[] { 10, 3 });
			var result = first.Multiplication(second);
			CheckNumber(result, 88, 202, 6);
		}

		[Test]
		public void Multiplication9() {
			var first = CreateNumber(new[] { 0 });
			var second = CreateNumber(new[] { 0 });
			var result = first.Multiplication(second);
			CheckNumber(result, 0);
		}

		#endregion

		#region Division

		[Test]
		public void Division_DivisorIsZero() {
			var number = CreateNumber(10);
			var divisor = CreateNumber(0);
			Assert.Throws(typeof(DivideByZeroException), () => number.Division(divisor));
		}

		[Test]
		public void Division1() {
			var divisible = CreateNumber(10);
			var divisor = CreateNumber(2);
			var result = divisible.Division(divisor);
			CheckNumber(result, 5);
			CheckNumber(divisible, 10);
			CheckNumber(divisor, 2);
		}

		[Test]
		public void Division2() {
			var divisible = CreateNumber(67, 23);
			var divisor = CreateNumber(1);
			var result = divisible.Division(divisor);
			CheckNumber(result, 67, 23);
		}

		[Test]
		public void Division3() {
			var divisible = CreateNumber(31);
			var divisor = CreateNumber(4);
			var result = divisible.Division(divisor);
			CheckNumber(result, 7);
		}

		[Test]
		public void Division4() {
			var divisible = CreateNumber(97);
			var divisor = CreateNumber(150);
			var result = divisible.Division(divisor);
			CheckNumber(result, 0);
		}

		[Test]
		public void Division5() {
			var divisible = CreateNumber(47, 92);
			var divisor = CreateNumber(47, 92);
			var result = divisible.Division(divisor);
			CheckNumber(result, 1);
		}

		[Test]
		public void Division6() {
			var divisible = CreateNumber(0, 1);
			var divisor = CreateNumber(3);
			var result = divisible.Division(divisor);
			CheckNumber(result, 85);
		}

		[Test]
		public void Division7() {
			var divisible = CreateNumber(0, 2);
			var divisor = CreateNumber(2);
			var result = divisible.Division(divisor);
			CheckNumber(result, 0, 1);
		}

		[Test]
		public void Division8() {
			var divisible = CreateNumber(21, 49, 76);
			var divisor = CreateNumber(4, 11);
			var result = divisible.Division(divisor);
			CheckNumber(result, 234, 6);
		}

		[Test]
		public void Division9() {
			var divisible = CreateNumber(0, 255);
			var divisor = CreateNumber(15);
			var result = divisible.Division(divisor);
			CheckNumber(result, 0, 17);
		}

		[Test]
		public void Division10() {
			var divisible = CreateNumber(0, 0, 0, 255);
			var divisor = CreateNumber(15);
			var result = divisible.Division(divisor);
			CheckNumber(result, 0, 0, 0, 17);
		}

		[Test]
		public void Division11() {
			var divisible = CreateNumber(0, 0, 1);
			var divisor = CreateNumber(0, 1);
			var result = divisible.Division(divisor);
			CheckNumber(result, 0, 1);
		}

		[Test]
		public void Division12() {
			var divisible = CreateNumber(1, 0, 1);
			var divisor = CreateNumber(1, 1);
			var result = divisible.Division(divisor);
			CheckNumber(result, 255);
		}

		[Test]
		public void Division13() {
			var divisible = CreateNumber(7, 3, 6, 1); // 17171207
			var divisor = CreateNumber(9, 15); // 3849
			var result = divisible.Division(divisor);
			CheckNumber(result, 109, 17); // 4461
		}

		[Test]
		public void Division14() {
			var divisible = CreateNumber(2, 2); // 514
			var divisor = CreateNumber(2);
			var result = divisible.Division(divisor);
			CheckNumber(result, 1, 1); // 257
		}

		[Test]
		public void Division15() {
			var divisible = CreateNumber(0, 1);
			var divisor = CreateNumber(2);
			var result = divisible.Division(divisor);
			CheckNumber(result, 128);
		}

		[Test]
		public void Division16() {
			var divisible = CreateNumber(2, 1);
			var divisor = CreateNumber(2);
			var result = divisible.Division(divisor);
			CheckNumber(result, 129);
		}

		#endregion

		#region Mod

		[Test]
		public void Mod_FoundationIsZero() {
			var first = CreateNumber(new[] { 15 });
			var second = CreateNumber(new[] { 0 });
			Assert.Throws(typeof(DivideByZeroException), () => first.Mod(second));
		}

		[Test]
		public void Mod1() {
			var first = CreateNumber(new[] { 5, 3 });
			var second = CreateNumber(new[] { 5, 3 });
			var result = first.Mod(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Mod2() {
			var first = CreateNumber(new[] { 5, 3 });
			var second = CreateNumber(new[] { 7, 8 });
			var result = first.Mod(second);
			CheckNumber(result, 5, 3);
		}

		[Test]
		public void Mod3() {
			var first = CreateNumber(new[] { 15 });
			var second = CreateNumber(new[] { 7 });
			var result = first.Mod(second);
			CheckNumber(result, 1);
		}

		[Test]
		public void Mod4() {
			var first = CreateNumber(new[] { 0 });
			var second = CreateNumber(new[] { 7 });
			var result = first.Mod(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Mod5() {
			var first = CreateNumber(new[] { 23, 2 });
			var second = CreateNumber(new[] { 28 });
			var result = first.Mod(second);
			CheckNumber(result, 3);
		}

		[Test]
		public void Mod6() {
			var first = CreateNumber(0, 0, 5);
			var second = CreateNumber(4);
			var result = first.Mod(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Mod7() {
			var foundation = CreateNumber(new[] { 147, 1 });
			var number = CreateNumber(new[] { 0, 0, 1 });
			var result = number.Mod(foundation);
			CheckNumber(result, 250);
		}

		[Test]
		public void Mod8() {
			var foundation = CreateNumber(new[] { 0, 17 });
			var number = CreateNumber(new[] { 0, 0, 2 });
			var result = number.Mod(foundation);
			CheckNumber(result, 0, 2);
		}

		[Test]
		public void Mod9() {
			var foundation = CreateNumber(new[] { 145, 17, 57 });
			var number = CreateNumber(new[] { 0, 0, 2 });
			var result = number.Mod(foundation);
			CheckNumber(result, 0, 0, 2);
		}

		[Test]
		public void Mod10() {
			var foundation = CreateNumber(new[] { 145, 17, 57, 9 });
			var number = CreateNumber(new[] { 145, 17, 57, 9 });
			var result = number.Mod(foundation);
			CheckNumber(result, 0);
		}

		[Test]
		public void Mod11() {
			var foundation = CreateNumber(new[] { 135, 1 });
			var number = CreateNumber(new[] { 222, 1 });
			var result = number.Mod(foundation);
			CheckNumber(result, 87);
		}

		[Test]
		public void Mod12() {
			var foundation = CreateNumber(new[] { 236, 255, 0, 1 });
			var number = CreateNumber(new[] { 237, 255, 0, 1 });
			var result = number.Mod(foundation);
			CheckNumber(result, 1);
		}

		[Test]
		public void Mod13() {
			var number = CreateNumber(new[] { 201, 107, 201 }); // 13200329
			var foundation = CreateNumber(new[] { 202, 67 }); // 17354
			var result = number.Mod(foundation);
			CheckNumber(result, 25, 44); // 11289
		}

		[Test]
		public void Mod14() {
			var number = CreateNumber(new[] { 201, 107, 202 }); // 13265865
			var foundation = CreateNumber(new[] { 202, 67 }); // 17354
			var result = number.Mod(foundation);
			CheckNumber(result, 241, 28); // 7409
		}

		[Test]
		public void Mod15() {
			var number = CreateNumber(new[] { 0, 16, 3 }); // 200704
			var foundation = CreateNumber(new[] { 193, 1 }); // 449
			var result = number.Mod(foundation);
			CheckNumber(result, 1);
		}

		[Test]
		public void Mod16() {
			var number = CreateNumber(new[] { 0, 0, 255 }); // 16711680
			var foundation = CreateNumber(new[] { 255, 1 }); // 511
			var result = number.Mod(foundation);
			CheckNumber(result, 191, 1); // 447
		}

		[Test]
		public void Mod17() {
			var number = CreateNumber(new[] { 0, 0, 255 }); // 16711680
			var foundation = CreateNumber(new[] { 255, 254 }); // 65279
			var result = number.Mod(foundation);
			CheckNumber(result, 0, 1); // 256
		}

		[Test]
		public void Mod_NumberIs_205609252335492_FoundationIs_3137302433() {
			var number = CreateNumber(new[] { 132, 175, 114, 34, 0, 187 }); // 205609252335492
			var foundation = CreateNumber(new[] { 161, 111, 255, 186 }); // 3137302433
			var result = number.Mod(foundation);
			CheckNumber(result, 132, 175, 209, 178); // 3000086404
		}

		#endregion

		#region Power

		[Test]
		public void Power_DegreeIsZero() {
			var number = CreateNumber(new[] { 12, 90 });
			var degree = CreateNumber(new[] { 0 });
			var result = number.Power(degree);
			CheckNumber(result, 1);
		}

		[Test]
		public void Power_NumberIsZero() {
			var number = CreateNumber(new[] { 0 });
			var degree = CreateNumber(new[] { 10 });
			var result = number.Power(degree);
			CheckNumber(result, 0);
		}

		[Test]
		public void Power1() {
			var number = CreateNumber(new[] { 10 });
			var degree = CreateNumber(new[] { 1 });
			var result = number.Power(degree);
			CheckNumber(result, 10);
		}

		[Test]
		public void Power2() {
			var number = CreateNumber(new[] { 10 });
			var degree = CreateNumber(new[] { 2 });
			var result = number.Power(degree);
			CheckNumber(result, 100);
		}

		[Test]
		public void Power3() {
			var number = CreateNumber(new[] { 10 });
			var degree = CreateNumber(new[] { 3 });
			var result = number.Power(degree);
			CheckNumber(result, 232, 3);
		}

		[Test]
		public void Power4() {
			var number = CreateNumber(new[] { 1 });
			var degree = CreateNumber(new[] { 10 });
			var result = number.Power(degree);
			CheckNumber(result, 1);
		}

		[Test]
		public void Power5() {
			var number = CreateNumber(new[] { 23, 8 });
			var degree = CreateNumber(new[] { 2 });
			var result = number.Power(degree);
			CheckNumber(result, 17, 114, 65);
		}

		[Test]
		public void Power6() {
			var number = CreateNumber(new[] { 1, 1 });
			var degree = CreateNumber(new[] { 2 });
			var result = number.Power(degree);
			CheckNumber(result, 1, 2, 1);
		}

		[Test]
		public void Power7() {
			var number = CreateNumber(new[] { 1, 1 });
			var degree = CreateNumber(new[] { 3 });
			var result = number.Power(degree);
			CheckNumber(result, 1, 3, 3, 1);
		}

		[Test]
		public void Power8() {
			var number = CreateNumber(new[] { 0, 1 });
			var degree = CreateNumber(new[] { 2 });
			var result = number.Power(degree);
			CheckNumber(result, 0, 0, 1);
		}

		[Test]
		public void Power9() {
			var number = CreateNumber(new[] { 24 });
			var degree = CreateNumber(new[] { 10 });
			var result = number.Power(degree);
			CheckNumber(result, 0, 0, 0, 64, 170, 57);
		}

		#endregion

		#region Square

		[Test]
		public void Square1() {
			var number = CreateNumber(new[] { 2 });
			var result = number.Square();
			CheckNumber(result, 4);
		}

		[Test]
		public void Square2() {
			var number = CreateNumber(new[] { 60 });
			var result = number.Square();
			CheckNumber(result, 16, 14);
		}

		[Test]
		public void Square3() {
			var number = CreateNumber(new[] { 10 });
			var result = number.Square();
			CheckNumber(result, 100);
		}

		[Test]
		public void Square4() {
			var number = CreateNumber(new[] { 255 });
			var result = number.Square();
			CheckNumber(result, 1, 254);
		}

		[Test]
		public void Square5() {
			var number = CreateNumber(new[] { 92, 54 });
			var result = number.Square();
			CheckNumber(result, 16, 241, 138, 11);
		}

		[Test]
		public void Square6() {
			var number = CreateNumber(new[] { 0 });
			var result = number.Square();
			CheckNumber(result, 0);
		}

		[Test]
		public void Square7() {
			var number = CreateNumber(new[] { 1 });
			var result = number.Square();
			CheckNumber(result, 1);
		}

		[Test]
		public void Square8() {
			var number = CreateNumber(new[] { 0, 0, 1 });
			var result = number.Square();
			CheckNumber(result, 0, 0, 0, 0, 1);
		}

		[Test]
		public void Square9() {
			var number = CreateNumber(new[] { 5, 2, 7 });
			var result = number.Square();
			CheckNumber(result, 25, 20, 74, 28, 49);
		}

		[Test]
		public void Square10() {
			var number = CreateNumber(new[] { 255, 255, 255, 255 });
			var result = number.Square();
			CheckNumber(result, 1, 0, 0, 0, 254, 255, 255, 255);
		}

		#endregion

		#region Compare

		[Test]
		public void Compare_CompareResultIsEqual() {
			var first = CreateNumber(new[] { 10, 11 });
			var second = CreateNumber(new[] { 10, 11 });
			Assert.AreEqual(CompareResult.Equal, first.Compare(second));
		}

		[Test]
		public void Compare_CompareResultIsGreater() {
			var first = CreateNumber(new[] { 2 });
			var second = CreateNumber(new[] { 1 });
			Assert.AreEqual(CompareResult.Greater, first.Compare(second));
		}

		[Test]
		public void Compare_CompareResultIsLess() {
			var first = CreateNumber(new[] { 0 });
			var second = CreateNumber(new[] { 1 });
			Assert.AreEqual(CompareResult.Less, first.Compare(second));
		}

		[Test]
		public void Compare1() {
			var first = CreateNumber(new[] { 10, 15 });
			var second = CreateNumber(new[] { 9, 16 });
			Assert.AreEqual(CompareResult.Less, first.Compare(second));
		}

		#endregion

		#region ToBits

		[Test]
		public void ToBits_NumberIs0() {
			var number = CreateNumber(new[] { 0 });
			var bits = number.ToBits();
			// Check Up
			Assert.AreEqual(1, bits.Length);
			Assert.IsFalse(bits[0]);
		}

		[Test]
		public void ToBits_NumberIs58() {
			var number = CreateNumber(new[] { 58 });
			var bits = number.ToBits();
			// Check Up
			Assert.AreEqual(6, bits.Length);
			Assert.IsFalse(bits[0]);
			Assert.IsTrue(bits[1]);
			Assert.IsFalse(bits[2]);
			Assert.IsTrue(bits[3]);
			Assert.IsTrue(bits[4]);
			Assert.IsTrue(bits[5]);
		}

		[Test]
		public void ToBits_NumberIs314() {
			var number = CreateNumber(new[] { 58, 1 }); // 314
			var bits = number.ToBits();
			// Check Up
			Assert.AreEqual(9, bits.Length);
			Assert.IsFalse(bits[0]);
			Assert.IsTrue(bits[1]);
			Assert.IsFalse(bits[2]);
			Assert.IsTrue(bits[3]);
			Assert.IsTrue(bits[4]);
			Assert.IsTrue(bits[5]);
			Assert.IsFalse(bits[6]);
			Assert.IsFalse(bits[7]);
			Assert.IsTrue(bits[8]);
		}

		#endregion

		#region IsZero

		[Test]
		public void IsZero_NumberIsNotZero_LenghtIsOne() {
			var number = CreateNumber(new[] { 1 });
			Assert.IsFalse(number.IsZero);
		}

		[Test]
		public void IsZero_NumberIsNotZero_LengthIsTree() {
			var number = CreateNumber(new[] { 0, 4, 4 });
			Assert.IsFalse(number.IsZero);
		}

		[Test]
		public void IsZero_NumberIsZero_LenghtIsOne() {
			var number = CreateNumber(new[] { 0 });
			Assert.IsTrue(number.IsZero);
		}

		#endregion

		#region FromInt

		[Test]
		public void FromInt_NumberLessZero() {
			Assert.Throws(typeof(ArgumentException), () => BigNumber.FromInt(-1));
		}

		[Test]
		public void FromInt_NumberIs0() {
			var number = BigNumber.FromInt(0);
			CheckNumber(number, 0);
		}

		[Test]
		public void FromInt_NumberIs42() {
			var number = BigNumber.FromInt(42);
			CheckNumber(number, 42);
		}

		[Test]
		public void FromInt_NumberIs259() {
			var number = BigNumber.FromInt(259);
			CheckNumber(number, 3, 1);
		}

		[Test]
		public void FromInt_NumberIs65279() {
			var number = BigNumber.FromInt(65279);
			CheckNumber(number, 255, 254);
		}

		[Test]
		public void FromInt_NumberIs200705() {
			var number = BigNumber.FromInt(200705);
			CheckNumber(number, 1, 16, 3);
		}

		[Test]
		public void FromInt_NumberIs2147483647() {
			var number = BigNumber.FromInt(2147483647);
			CheckNumber(number, 255, 255, 255, 127);
		}

		#endregion

		#region IsEven

		[Test]
		public void IsEven_NumberIsZero() {
			var number = BigNumber.FromInt(0);
			Assert.IsTrue(number.IsEven);
		}

		[Test]
		public void IsEven_NumberIs1() {
			var number = BigNumber.FromInt(1);
			Assert.IsFalse(number.IsEven);
		}

		[Test]
		public void IsEven_NumberIs2() {
			var number = BigNumber.FromInt(2);
			Assert.IsTrue(number.IsEven);
		}

		[Test]
		public void IsEven_NumberIs3() {
			var number = BigNumber.FromInt(3);
			Assert.IsFalse(number.IsEven);
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