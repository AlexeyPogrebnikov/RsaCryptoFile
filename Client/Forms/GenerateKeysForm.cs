using System;
using System.Windows.Forms;
using CryptoFile.Client.Configuration;

namespace CryptoFile.Client.Forms
{
	public partial class GenerateKeysForm : Form, IGenerateKeysForm
	{
		private int publicExponent;
		private Language language;

		public GenerateKeysForm()
		{
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;
		}

		#region IGenerateKeysForm Members

		public event EventHandler Generate;
		public event EventHandler ChangePublicExponent;
		public event EventHandler CancelGenerateKeys;

		public int MinRsaKeyLength
		{
			get => decimal.ToInt32(lengthNumericUpDown.Minimum);
			set => lengthNumericUpDown.Minimum = value;
		}

		public int MaxRsaKeyLength
		{
			get => decimal.ToInt32(lengthNumericUpDown.Maximum);
			set => lengthNumericUpDown.Maximum = value;
		}

		public int RsaKeyLength
		{
			get => decimal.ToInt32(lengthNumericUpDown.Value);
			set => lengthNumericUpDown.Value = value;
		}

		public string PublicKey
		{
			get => publicKeyTextBox.Text;
			set => publicKeyTextBox.Text = value;
		}

		public string PrivateKey
		{
			get => privateKeyTextBox.Text;
			set => privateKeyTextBox.Text = value;
		}

		public bool GenerateEnabled
		{
			get => generateButton.Enabled;
			set => generateButton.Enabled = value;
		}

		public int PublicExponent
		{
			get => publicExponent;
			set
			{
				publicExponent = value;
				publicExponentTextBox.Text = publicExponent.ToString();
			}
		}

		public Language Language
		{
			get => language;
			set
			{
				language = value;
				if (language == Language.English)
				{
					Text = @"Generate Keys";
					lengthLabel.Text = @"Length:";
					publicExponentLabel.Text = @"Public exponent:";
					publicKeyLabel.Text = @"Public key:";
					privateKeyLabel.Text = @"Private key:";
					generateButton.Text = @"Generate";
					cancelButton.Text = @"Cancel";
				}

				if (language == Language.Russian)
				{
					Text = @"Генерация ключей";
					lengthLabel.Text = @"Длина:";
					publicExponentLabel.Text = @"Открытая экспонента:";
					publicKeyLabel.Text = @"Публичный ключ:";
					privateKeyLabel.Text = @"Секретный ключ:";
					generateButton.Text = @"Генерировать";
					cancelButton.Text = @"Отмена";
				}
			}
		}

		#endregion

		private void generateButton_Click(object sender, EventArgs e)
		{
			if (Generate != null)
			{
				Generate(this, e);
			}
		}

		private void publicExponentButton_Click(object sender, EventArgs e)
		{
			if (ChangePublicExponent != null)
				ChangePublicExponent(this, e);
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			if (CancelGenerateKeys != null)
				CancelGenerateKeys(this, e);
		}
	}
}