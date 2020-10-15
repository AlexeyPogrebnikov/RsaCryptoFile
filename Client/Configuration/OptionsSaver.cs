using System;
using System.IO;
using System.Xml.Serialization;

namespace CryptoFile.Client.Configuration
{
	internal class OptionsSaver
	{
		private readonly string fileName;

		public OptionsSaver(string fileName)
		{
			this.fileName = fileName;
		}

		public Options LoadOptions()
		{
			return !File.Exists(fileName) ? new Options() : Deserialize(fileName);
		}

		public void SaveOptions(Options options)
		{
			var serializer = new XmlSerializer(options.GetType());
			using (var writer = new StreamWriter(fileName))
			{
				serializer.Serialize(writer, options);
			}
		}

		private static Options Deserialize(string fileName)
		{
			using (var reader = new StreamReader(fileName))
			{
				var serializer = new XmlSerializer(typeof(Options));
				try
				{
					return (Options) serializer.Deserialize(reader);
				}
				catch (Exception)
				{
					return new Options();
				}
			}
		}
	}
}