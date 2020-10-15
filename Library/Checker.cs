using System;

namespace CryptoFile.Library
{
	/// <summary>
	/// Выполняет проверки значений, генерирует исключения
	/// </summary>
	public static class Checker
	{
		/// <summary>
		/// Проверяет строку на корректность
		/// </summary>
		/// <param name="value">Проверяемая строка</param>
		/// <exception cref="ArgumentNullException">если value == null</exception>
		/// <exception cref="ArgumentException">если value пустая строка</exception>
		public static void CheckString(string value)
		{
			CheckNull(value);
			if (String.IsNullOrEmpty(value))
			{
				throw new ArgumentException("value пустая строка");
			}
		}

		/// <summary>
		/// Проверяет объект на равенство null
		/// </summary>
		/// <param name="values">Проверяемый объект</param>
		/// <exception cref="ArgumentNullException">если value == null</exception>
		public static void CheckNull(params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}

			foreach (object value in values)
			{
				if (value == null)
				{
					throw new ArgumentNullException("values");
				}
			}
		}
	}
}