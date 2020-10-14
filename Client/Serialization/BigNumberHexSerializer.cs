using CryptoFile.Library;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Client.Serialization {
	class BigNumberHexSerializer : IBigNumberSerializer {
		#region IBigNumberSerializer Members

		public string Serialize(BigNumber number) {
			Checker.CheckNull(number);
			var numbers = number.Numbers;
			var result = "";
			for (var i = numbers.Count - 1; i >= 0; --i) {
				result += ConvertToHex(numbers[i]);
			}
			return result;
		}

		public BigNumber Deserialize(string line) {
			Checker.CheckString(line);
			line = line.ToUpper();
			CheckFormat(line);
			var numbers = new int[line.Length/2];
			for (var i = 0; i < numbers.Length; ++i) {
				var first = GetValue(line[line.Length - 2*i - 1]);
				var second = GetValue(line[line.Length - 2*i - 2]);
				numbers[i] = first + 16*second;
			}
			return BigNumber.FromBytes(numbers);
		}

		#endregion

		private static void CheckFormat(string line) {
			if (line.Length%2 != 0) {
				throw new BigNumberFormatException("line.Length % 2 != 0");
			}
			foreach (var symbol in line) {
				if (!CheckSymbol(symbol)) {
					throw new BigNumberFormatException("error in symbol: " + symbol);
				}
			}
		}

		private static bool CheckSymbol(char symbol) {
			switch (symbol) {
				case '0' :
					return true;
				case '1' :
					return true;
				case '2' :
					return true;
				case '3' :
					return true;
				case '4' :
					return true;
				case '5' :
					return true;
				case '6' :
					return true;
				case '7' :
					return true;
				case '8' :
					return true;
				case '9' :
					return true;
				case 'A' :
					return true;
				case 'B' :
					return true;
				case 'C' :
					return true;
				case 'D' :
					return true;
				case 'E' :
					return true;
				case 'F' :
					return true;
			}
			return false;
		}

		private static string ConvertToHex(int number) {
			var first = number/16;
			var second = number - 16*first;
			return GetSymbol(first) + GetSymbol(second);
		}

		private static int GetValue(char symbol) {
			switch (symbol) {
				case 'A' :
					return 10;
				case 'B' :
					return 11;
				case 'C' :
					return 12;
				case 'D' :
					return 13;
				case 'E' :
					return 14;
				case 'F' :
					return 15;
			}
			return int.Parse(symbol.ToString());
		}

		private static string GetSymbol(int number) {
			switch (number) {
				case 10 :
					return "A";
				case 11 :
					return "B";
				case 12 :
					return "C";
				case 13 :
					return "D";
				case 14 :
					return "E";
				case 15 :
					return "F";
			}
			return number.ToString();
		}
	}
}