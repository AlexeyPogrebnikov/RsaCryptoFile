using System;
using CryptoFile.Library.LongArithmetic;
using NUnit.Framework;

namespace CryptoFile.Library.Tests.LongArithmetic
{
	[TestFixture]
	public class BigNumber_Test
	{
		#region Constructor

		[Test]
		public void Constructor()
		{
			var numbers = new[] { 1, 2, 3 };
			BigNumber number = BigNumber.FromBytes(numbers);
			numbers[0] = 4;
			CheckNumber(number, 1, 2, 3);
		}

		[Test]
		public void Constructor_NumbersIsEmpty()
		{
			Assert.Throws(typeof(ArgumentException), () => BigNumber.FromBytes(new int[0]));
		}

		[Test]
		public void Constructor_NumbersIsNull()
		{
			Assert.Throws(typeof(ArgumentNullException), () => CreateNumber(null));
		}

		[Test]
		public void Constructor_NumbersIsZero_LenghtIsTwo()
		{
			Assert.Throws(typeof(ArgumentException), () => CreateNumber(0, 0));
		}

		#endregion

		#region Addition

		[Test]
		public void Addition_FirstNumberIs1_SecondNumberIs10()
		{
			BigNumber first = CreateNumber(1);
			BigNumber second = CreateNumber(10);
			BigNumber result = first.Addition(second);
			CheckNumber(result, 11);
		}

		[Test]
		public void Addition_FirstNumberIs255_SecondNumberIs1()
		{
			BigNumber first = CreateNumber(255);
			BigNumber second = CreateNumber(1);
			BigNumber result = first.Addition(second);
			CheckNumber(result, 0, 1);
		}

		[Test]
		public void Addition_FirstNumberIs12810_SecondNumberIs60()
		{
			BigNumber first = CreateNumber(10, 50); // 12810
			BigNumber second = CreateNumber(60);
			BigNumber result = first.Addition(second);
			CheckNumber(result, 70, 50);
		}

		[Test]
		public void Addition_FirstNumberIs65535_SecondNumberIs257()
		{
			BigNumber first = CreateNumber(255, 255); // 65535
			BigNumber second = CreateNumber(1, 1); // 257
			BigNumber result = first.Addition(second);
			CheckNumber(result, 0, 1, 1);
		}

		[Test]
		public void Addition_FirstNumberIs255_SecondNumberIs655617()
		{
			BigNumber first = CreateNumber(255);
			BigNumber second = CreateNumber(1, 1, 10); // 655617
			BigNumber result = first.Addition(second);
			CheckNumber(result, 0, 2, 10);
		}

		[Test]
		public void Addition_FirstNumberIs12445656_SecondNumberIs14472691()
		{
			BigNumber first = CreateNumber(216, 231, 189); // 12445656
			BigNumber second = CreateNumber(243, 213, 220); // 14472691
			BigNumber result = first.Addition(second);
			CheckNumber(result, 203, 189, 154, 1);
		}

		#endregion

		#region Subtraction

		[Test]
		public void Subtraction1()
		{
			BigNumber first = CreateNumber(2);
			BigNumber second = CreateNumber(1);
			BigNumber result = first.Subtraction(second);
			CheckNumber(result, 1);
		}

		[Test]
		public void Subtraction2()
		{
			BigNumber first = CreateNumber(2);
			BigNumber second = CreateNumber(10);
			BigNumber result = first.Subtraction(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Subtraction3()
		{
			BigNumber first = CreateNumber(10, 45);
			BigNumber second = CreateNumber(10, 45);
			BigNumber result = first.Subtraction(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Subtraction4()
		{
			BigNumber first = CreateNumber(0, 1);
			BigNumber second = CreateNumber(50);
			BigNumber result = first.Subtraction(second);
			CheckNumber(result, 206);
		}

		[Test]
		public void Subtraction5()
		{
			BigNumber first = CreateNumber(4, 8, 7);
			BigNumber second = CreateNumber(1, 4);
			BigNumber result = first.Subtraction(second);
			CheckNumber(result, 3, 4, 7);
		}

		[Test]
		public void Subtraction6()
		{
			BigNumber first = CreateNumber(4, 8, 7);
			BigNumber second = CreateNumber(6, 9);
			BigNumber result = first.Subtraction(second);
			CheckNumber(result, 254, 254, 6);
		}

		[Test]
		public void Subtraction7()
		{
			BigNumber first = CreateNumber(17, 5, 6, 7);
			BigNumber second = CreateNumber(6, 5, 6, 7);
			BigNumber result = first.Subtraction(second);
			CheckNumber(result, 11);
		}

		[Test]
		public void Subtraction8()
		{
			BigNumber first = CreateNumber(0, 0, 1);
			BigNumber second = CreateNumber(2);
			BigNumber result = first.Subtraction(second);
			CheckNumber(result, 254, 255);
		}

		#endregion

		#region Increment

		[Test]
		public void Increment1()
		{
			BigNumber number = CreateNumber(1);
			number.Increment();
			CheckNumber(number, 2);
		}

		[Test]
		public void Increment2()
		{
			BigNumber number = CreateNumber(255);
			number.Increment();
			CheckNumber(number, 0, 1);
		}

		[Test]
		public void Increment3()
		{
			BigNumber number = CreateNumber(255, 255);
			number.Increment();
			CheckNumber(number, 0, 0, 1);
		}

		[Test]
		public void Increment4()
		{
			BigNumber number = CreateNumber(255, 255, 255);
			number.Increment();
			CheckNumber(number, 0, 0, 0, 1);
		}

		[Test]
		public void Increment5()
		{
			BigNumber number = CreateNumber(255, 10);
			number.Increment();
			CheckNumber(number, 0, 11);
		}

		#endregion

		#region Decrement

		[Test]
		public void Decrement1()
		{
			BigNumber number = CreateNumber(2);
			number.Decrement();
			CheckNumber(number, 1);
		}

		[Test]
		public void Decrement2()
		{
			BigNumber number = CreateNumber(0, 1);
			number.Decrement();
			CheckNumber(number, 255);
		}

		[Test]
		public void Decrement3()
		{
			BigNumber number = CreateNumber(23, 7);
			number.Decrement();
			CheckNumber(number, 22, 7);
		}

		[Test]
		public void Decrement4()
		{
			BigNumber number = CreateNumber(0, 0, 1);
			number.Decrement();
			CheckNumber(number, 255, 255);
		}

		[Test]
		public void Decrement5()
		{
			BigNumber number = CreateNumber(0, 4, 1);
			number.Decrement();
			CheckNumber(number, 255, 3, 1);
		}

		#endregion

		#region Multiplication

		[Test]
		public void Multiplication1()
		{
			BigNumber first = CreateNumber(10, 7);
			BigNumber second = CreateNumber(5, 3);
			BigNumber result = first.Multiplication(second);
			CheckNumber(result, 50, 65, 21);
		}

		[Test]
		public void Multiplication2()
		{
			BigNumber first = CreateNumber(10, 7);
			BigNumber second = CreateNumber(255);
			BigNumber result = first.Multiplication(second);
			CheckNumber(result, 246, 2, 7);
		}

		[Test]
		public void Multiplication3()
		{
			BigNumber first = CreateNumber(255, 255);
			BigNumber second = CreateNumber(255, 255);
			BigNumber result = first.Multiplication(second);
			CheckNumber(result, 1, 0, 254, 255);
		}

		[Test]
		public void Multiplication4()
		{
			BigNumber first = CreateNumber(0);
			BigNumber second = CreateNumber(8, 6);
			BigNumber result = first.Multiplication(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Multiplication5()
		{
			BigNumber first = CreateNumber(14, 56);
			BigNumber second = CreateNumber(0);
			BigNumber result = first.Multiplication(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Multiplication6()
		{
			BigNumber first = CreateNumber(12, 6, 9, 4);
			BigNumber second = CreateNumber(1);
			BigNumber result = first.Multiplication(second);
			CheckNumber(result, 12, 6, 9, 4);
		}

		[Test]
		public void Multiplication7()
		{
			BigNumber first = CreateNumber(1);
			BigNumber second = CreateNumber(12, 6, 9, 4);
			BigNumber result = first.Multiplication(second);
			CheckNumber(result, 12, 6, 9, 4);
		}

		[Test]
		public void Multiplication8()
		{
			BigNumber first = CreateNumber(60, 2);
			BigNumber second = CreateNumber(10, 3);
			BigNumber result = first.Multiplication(second);
			CheckNumber(result, 88, 202, 6);
		}

		[Test]
		public void Multiplication9()
		{
			BigNumber first = CreateNumber(0);
			BigNumber second = CreateNumber(0);
			BigNumber result = first.Multiplication(second);
			CheckNumber(result, 0);
		}

		#endregion

		#region Division

		[Test]
		public void Division_DivisorIsZero()
		{
			BigNumber number = CreateNumber(10);
			BigNumber divisor = CreateNumber(0);
			Assert.Throws(typeof(DivideByZeroException), () => number.Division(divisor));
		}

		[Test]
		public void Division1()
		{
			BigNumber divisible = CreateNumber(10);
			BigNumber divisor = CreateNumber(2);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 5);
			CheckNumber(divisible, 10);
			CheckNumber(divisor, 2);
		}

		[Test]
		public void Division2()
		{
			BigNumber divisible = CreateNumber(67, 23);
			BigNumber divisor = CreateNumber(1);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 67, 23);
		}

		[Test]
		public void Division3()
		{
			BigNumber divisible = CreateNumber(31);
			BigNumber divisor = CreateNumber(4);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 7);
		}

		[Test]
		public void Division4()
		{
			BigNumber divisible = CreateNumber(97);
			BigNumber divisor = CreateNumber(150);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 0);
		}

		[Test]
		public void Division5()
		{
			BigNumber divisible = CreateNumber(47, 92);
			BigNumber divisor = CreateNumber(47, 92);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 1);
		}

		[Test]
		public void Division6()
		{
			BigNumber divisible = CreateNumber(0, 1);
			BigNumber divisor = CreateNumber(3);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 85);
		}

		[Test]
		public void Division7()
		{
			BigNumber divisible = CreateNumber(0, 2);
			BigNumber divisor = CreateNumber(2);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 0, 1);
		}

		[Test]
		public void Division8()
		{
			BigNumber divisible = CreateNumber(21, 49, 76);
			BigNumber divisor = CreateNumber(4, 11);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 234, 6);
		}

		[Test]
		public void Division9()
		{
			BigNumber divisible = CreateNumber(0, 255);
			BigNumber divisor = CreateNumber(15);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 0, 17);
		}

		[Test]
		public void Division10()
		{
			BigNumber divisible = CreateNumber(0, 0, 0, 255);
			BigNumber divisor = CreateNumber(15);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 0, 0, 0, 17);
		}

		[Test]
		public void Division11()
		{
			BigNumber divisible = CreateNumber(0, 0, 1);
			BigNumber divisor = CreateNumber(0, 1);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 0, 1);
		}

		[Test]
		public void Division12()
		{
			BigNumber divisible = CreateNumber(1, 0, 1);
			BigNumber divisor = CreateNumber(1, 1);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 255);
		}

		[Test]
		public void Division13()
		{
			BigNumber divisible = CreateNumber(7, 3, 6, 1); // 17171207
			BigNumber divisor = CreateNumber(9, 15); // 3849
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 109, 17); // 4461
		}

		[Test]
		public void Division14()
		{
			BigNumber divisible = CreateNumber(2, 2); // 514
			BigNumber divisor = CreateNumber(2);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 1, 1); // 257
		}

		[Test]
		public void Division15()
		{
			BigNumber divisible = CreateNumber(0, 1);
			BigNumber divisor = CreateNumber(2);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 128);
		}

		[Test]
		public void Division16()
		{
			BigNumber divisible = CreateNumber(2, 1);
			BigNumber divisor = CreateNumber(2);
			BigNumber result = divisible.Division(divisor);
			CheckNumber(result, 129);
		}

		#endregion

		#region Mod

		[Test]
		public void Mod_FoundationIsZero()
		{
			BigNumber first = CreateNumber(15);
			BigNumber second = CreateNumber(0);
			Assert.Throws(typeof(DivideByZeroException), () => first.Mod(second));
		}

		[Test]
		public void Mod1()
		{
			BigNumber first = CreateNumber(5, 3);
			BigNumber second = CreateNumber(5, 3);
			BigNumber result = first.Mod(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Mod2()
		{
			BigNumber first = CreateNumber(5, 3);
			BigNumber second = CreateNumber(7, 8);
			BigNumber result = first.Mod(second);
			CheckNumber(result, 5, 3);
		}

		[Test]
		public void Mod3()
		{
			BigNumber first = CreateNumber(15);
			BigNumber second = CreateNumber(7);
			BigNumber result = first.Mod(second);
			CheckNumber(result, 1);
		}

		[Test]
		public void Mod4()
		{
			BigNumber first = CreateNumber(0);
			BigNumber second = CreateNumber(7);
			BigNumber result = first.Mod(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Mod5()
		{
			BigNumber first = CreateNumber(23, 2);
			BigNumber second = CreateNumber(28);
			BigNumber result = first.Mod(second);
			CheckNumber(result, 3);
		}

		[Test]
		public void Mod6()
		{
			BigNumber first = CreateNumber(0, 0, 5);
			BigNumber second = CreateNumber(4);
			BigNumber result = first.Mod(second);
			CheckNumber(result, 0);
		}

		[Test]
		public void Mod7()
		{
			BigNumber foundation = CreateNumber(147, 1);
			BigNumber number = CreateNumber(0, 0, 1);
			BigNumber result = number.Mod(foundation);
			CheckNumber(result, 250);
		}

		[Test]
		public void Mod8()
		{
			BigNumber foundation = CreateNumber(0, 17);
			BigNumber number = CreateNumber(0, 0, 2);
			BigNumber result = number.Mod(foundation);
			CheckNumber(result, 0, 2);
		}

		[Test]
		public void Mod9()
		{
			BigNumber foundation = CreateNumber(145, 17, 57);
			BigNumber number = CreateNumber(0, 0, 2);
			BigNumber result = number.Mod(foundation);
			CheckNumber(result, 0, 0, 2);
		}

		[Test]
		public void Mod10()
		{
			BigNumber foundation = CreateNumber(145, 17, 57, 9);
			BigNumber number = CreateNumber(145, 17, 57, 9);
			BigNumber result = number.Mod(foundation);
			CheckNumber(result, 0);
		}

		[Test]
		public void Mod11()
		{
			BigNumber foundation = CreateNumber(135, 1);
			BigNumber number = CreateNumber(222, 1);
			BigNumber result = number.Mod(foundation);
			CheckNumber(result, 87);
		}

		[Test]
		public void Mod12()
		{
			BigNumber foundation = CreateNumber(236, 255, 0, 1);
			BigNumber number = CreateNumber(237, 255, 0, 1);
			BigNumber result = number.Mod(foundation);
			CheckNumber(result, 1);
		}

		[Test]
		public void Mod13()
		{
			BigNumber number = CreateNumber(201, 107, 201); // 13200329
			BigNumber foundation = CreateNumber(202, 67); // 17354
			BigNumber result = number.Mod(foundation);
			CheckNumber(result, 25, 44); // 11289
		}

		[Test]
		public void Mod14()
		{
			BigNumber number = CreateNumber(201, 107, 202); // 13265865
			BigNumber foundation = CreateNumber(202, 67); // 17354
			BigNumber result = number.Mod(foundation);
			CheckNumber(result, 241, 28); // 7409
		}

		[Test]
		public void Mod15()
		{
			BigNumber number = CreateNumber(0, 16, 3); // 200704
			BigNumber foundation = CreateNumber(193, 1); // 449
			BigNumber result = number.Mod(foundation);
			CheckNumber(result, 1);
		}

		[Test]
		public void Mod16()
		{
			BigNumber number = CreateNumber(0, 0, 255); // 16711680
			BigNumber foundation = CreateNumber(255, 1); // 511
			BigNumber result = number.Mod(foundation);
			CheckNumber(result, 191, 1); // 447
		}

		[Test]
		public void Mod17()
		{
			BigNumber number = CreateNumber(0, 0, 255); // 16711680
			BigNumber foundation = CreateNumber(255, 254); // 65279
			BigNumber result = number.Mod(foundation);
			CheckNumber(result, 0, 1); // 256
		}

		[Test]
		public void Mod_NumberIs_205609252335492_FoundationIs_3137302433()
		{
			BigNumber number = CreateNumber(132, 175, 114, 34, 0, 187); // 205609252335492
			BigNumber foundation = CreateNumber(161, 111, 255, 186); // 3137302433
			BigNumber result = number.Mod(foundation);
			CheckNumber(result, 132, 175, 209, 178); // 3000086404
		}

		#endregion

		#region Power

		[Test]
		public void Power_DegreeIsZero()
		{
			BigNumber number = CreateNumber(12, 90);
			BigNumber degree = CreateNumber(0);
			BigNumber result = number.Power(degree);
			CheckNumber(result, 1);
		}

		[Test]
		public void Power_NumberIsZero()
		{
			BigNumber number = CreateNumber(0);
			BigNumber degree = CreateNumber(10);
			BigNumber result = number.Power(degree);
			CheckNumber(result, 0);
		}

		[Test]
		public void Power1()
		{
			BigNumber number = CreateNumber(10);
			BigNumber degree = CreateNumber(1);
			BigNumber result = number.Power(degree);
			CheckNumber(result, 10);
		}

		[Test]
		public void Power2()
		{
			BigNumber number = CreateNumber(10);
			BigNumber degree = CreateNumber(2);
			BigNumber result = number.Power(degree);
			CheckNumber(result, 100);
		}

		[Test]
		public void Power3()
		{
			BigNumber number = CreateNumber(10);
			BigNumber degree = CreateNumber(3);
			BigNumber result = number.Power(degree);
			CheckNumber(result, 232, 3);
		}

		[Test]
		public void Power4()
		{
			BigNumber number = CreateNumber(1);
			BigNumber degree = CreateNumber(10);
			BigNumber result = number.Power(degree);
			CheckNumber(result, 1);
		}

		[Test]
		public void Power5()
		{
			BigNumber number = CreateNumber(23, 8);
			BigNumber degree = CreateNumber(2);
			BigNumber result = number.Power(degree);
			CheckNumber(result, 17, 114, 65);
		}

		[Test]
		public void Power6()
		{
			BigNumber number = CreateNumber(1, 1);
			BigNumber degree = CreateNumber(2);
			BigNumber result = number.Power(degree);
			CheckNumber(result, 1, 2, 1);
		}

		[Test]
		public void Power7()
		{
			BigNumber number = CreateNumber(1, 1);
			BigNumber degree = CreateNumber(3);
			BigNumber result = number.Power(degree);
			CheckNumber(result, 1, 3, 3, 1);
		}

		[Test]
		public void Power8()
		{
			BigNumber number = CreateNumber(0, 1);
			BigNumber degree = CreateNumber(2);
			BigNumber result = number.Power(degree);
			CheckNumber(result, 0, 0, 1);
		}

		[Test]
		public void Power9()
		{
			BigNumber number = CreateNumber(24);
			BigNumber degree = CreateNumber(10);
			BigNumber result = number.Power(degree);
			CheckNumber(result, 0, 0, 0, 64, 170, 57);
		}

		#endregion

		#region Square

		[Test]
		public void Square1()
		{
			BigNumber number = CreateNumber(2);
			BigNumber result = number.Square();
			CheckNumber(result, 4);
		}

		[Test]
		public void Square2()
		{
			BigNumber number = CreateNumber(60);
			BigNumber result = number.Square();
			CheckNumber(result, 16, 14);
		}

		[Test]
		public void Square3()
		{
			BigNumber number = CreateNumber(10);
			BigNumber result = number.Square();
			CheckNumber(result, 100);
		}

		[Test]
		public void Square4()
		{
			BigNumber number = CreateNumber(255);
			BigNumber result = number.Square();
			CheckNumber(result, 1, 254);
		}

		[Test]
		public void Square5()
		{
			BigNumber number = CreateNumber(92, 54);
			BigNumber result = number.Square();
			CheckNumber(result, 16, 241, 138, 11);
		}

		[Test]
		public void Square6()
		{
			BigNumber number = CreateNumber(0);
			BigNumber result = number.Square();
			CheckNumber(result, 0);
		}

		[Test]
		public void Square7()
		{
			BigNumber number = CreateNumber(1);
			BigNumber result = number.Square();
			CheckNumber(result, 1);
		}

		[Test]
		public void Square8()
		{
			BigNumber number = CreateNumber(0, 0, 1);
			BigNumber result = number.Square();
			CheckNumber(result, 0, 0, 0, 0, 1);
		}

		[Test]
		public void Square9()
		{
			BigNumber number = CreateNumber(5, 2, 7);
			BigNumber result = number.Square();
			CheckNumber(result, 25, 20, 74, 28, 49);
		}

		[Test]
		public void Square10()
		{
			BigNumber number = CreateNumber(255, 255, 255, 255);
			BigNumber result = number.Square();
			CheckNumber(result, 1, 0, 0, 0, 254, 255, 255, 255);
		}

		#endregion

		#region Compare

		[Test]
		public void Compare_CompareResultIsEqual()
		{
			BigNumber first = CreateNumber(10, 11);
			BigNumber second = CreateNumber(10, 11);
			Assert.AreEqual(CompareResult.Equal, first.Compare(second));
		}

		[Test]
		public void Compare_CompareResultIsGreater()
		{
			BigNumber first = CreateNumber(2);
			BigNumber second = CreateNumber(1);
			Assert.AreEqual(CompareResult.Greater, first.Compare(second));
		}

		[Test]
		public void Compare_CompareResultIsLess()
		{
			BigNumber first = CreateNumber(0);
			BigNumber second = CreateNumber(1);
			Assert.AreEqual(CompareResult.Less, first.Compare(second));
		}

		[Test]
		public void Compare1()
		{
			BigNumber first = CreateNumber(10, 15);
			BigNumber second = CreateNumber(9, 16);
			Assert.AreEqual(CompareResult.Less, first.Compare(second));
		}

		#endregion

		#region ToBits

		[Test]
		public void ToBits_NumberIs0()
		{
			BigNumber number = CreateNumber(0);
			bool[] bits = number.ToBits();
			// Check Up
			Assert.AreEqual(1, bits.Length);
			Assert.IsFalse(bits[0]);
		}

		[Test]
		public void ToBits_NumberIs58()
		{
			BigNumber number = CreateNumber(58);
			bool[] bits = number.ToBits();
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
		public void ToBits_NumberIs314()
		{
			BigNumber number = CreateNumber(58, 1); // 314
			bool[] bits = number.ToBits();
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
		public void IsZero_NumberIsNotZero_LenghtIsOne()
		{
			BigNumber number = CreateNumber(1);
			Assert.IsFalse(number.IsZero);
		}

		[Test]
		public void IsZero_NumberIsNotZero_LengthIsTree()
		{
			BigNumber number = CreateNumber(0, 4, 4);
			Assert.IsFalse(number.IsZero);
		}

		[Test]
		public void IsZero_NumberIsZero_LenghtIsOne()
		{
			BigNumber number = CreateNumber(0);
			Assert.IsTrue(number.IsZero);
		}

		#endregion

		#region FromInt

		[Test]
		public void FromInt_NumberLessZero()
		{
			Assert.Throws(typeof(ArgumentException), () => BigNumber.FromInt(-1));
		}

		[Test]
		public void FromInt_NumberIs0()
		{
			BigNumber number = BigNumber.FromInt(0);
			CheckNumber(number, 0);
		}

		[Test]
		public void FromInt_NumberIs42()
		{
			BigNumber number = BigNumber.FromInt(42);
			CheckNumber(number, 42);
		}

		[Test]
		public void FromInt_NumberIs259()
		{
			BigNumber number = BigNumber.FromInt(259);
			CheckNumber(number, 3, 1);
		}

		[Test]
		public void FromInt_NumberIs65279()
		{
			BigNumber number = BigNumber.FromInt(65279);
			CheckNumber(number, 255, 254);
		}

		[Test]
		public void FromInt_NumberIs200705()
		{
			BigNumber number = BigNumber.FromInt(200705);
			CheckNumber(number, 1, 16, 3);
		}

		[Test]
		public void FromInt_NumberIs2147483647()
		{
			BigNumber number = BigNumber.FromInt(2147483647);
			CheckNumber(number, 255, 255, 255, 127);
		}

		#endregion

		#region IsEven

		[Test]
		public void IsEven_NumberIsZero()
		{
			BigNumber number = BigNumber.FromInt(0);
			Assert.IsTrue(number.IsEven);
		}

		[Test]
		public void IsEven_NumberIs1()
		{
			BigNumber number = BigNumber.FromInt(1);
			Assert.IsFalse(number.IsEven);
		}

		[Test]
		public void IsEven_NumberIs2()
		{
			BigNumber number = BigNumber.FromInt(2);
			Assert.IsTrue(number.IsEven);
		}

		[Test]
		public void IsEven_NumberIs3()
		{
			BigNumber number = BigNumber.FromInt(3);
			Assert.IsFalse(number.IsEven);
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