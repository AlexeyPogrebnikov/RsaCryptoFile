using System.Drawing;
using System.Xml.Serialization;

namespace CryptoFile.Client.Configuration {
	public class ColorXml {
		public ColorXml() {}

		public ColorXml(Color color) {
			Color = color;
		}

		[XmlIgnore]
		public Color Color {
			get { return Color.FromArgb(Red, Green, Blue); }
			set {
				Red = value.R;
				Green = value.G;
				Blue = value.B;
			}
		}

		public void SetColor() {}

		[XmlAttribute]
		public int Red { get; set; }

		[XmlAttribute]
		public int Green { get; set; }

		[XmlAttribute]
		public int Blue { get; set; }
	}
}