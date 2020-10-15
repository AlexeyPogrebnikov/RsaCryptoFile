using System;
using System.Drawing;
using System.Windows.Forms;
using CryptoFile.Client.Configuration;

namespace CryptoFile.Client.Forms
{
	internal partial class PropertiesForm : Form, IPropertiesForm
	{
		private Language language;

		public PropertiesForm()
		{
			InitializeComponent();
		}

		public Color RsaFileColor
		{
			get => rsaFileColorPanel.BackColor;
			set => rsaFileColorPanel.BackColor = value;
		}

		public Language Language
		{
			get => language;
			set
			{
				language = value;
				if (language == Language.English)
				{
					Text = @"Properties";
					rsaFileColorLabel.Text = @"RSA file color:";
					cancelButton.Text = @"Cancel";
				}

				if (language == Language.Russian)
				{
					Text = @"Свойства";
					rsaFileColorLabel.Text = @"Цвет RSA файлов:";
					cancelButton.Text = @"Отмена";
				}
			}
		}

		private void rsaFileColorButton_Click(object sender, EventArgs e)
		{
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				rsaFileColorPanel.BackColor = colorDialog.Color;
			}
		}
	}
}