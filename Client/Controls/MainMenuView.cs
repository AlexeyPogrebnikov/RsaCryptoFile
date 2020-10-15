using System;
using System.Windows.Forms;
using CryptoFile.Client.Configuration;

namespace CryptoFile.Client.Controls
{
	internal partial class MainMenuView : UserControl, IMainMenuView
	{
		public MainMenuView()
		{
			InitializeComponent();
		}

		#region IMainMenuView Members

		public event EventHandler Cipher;

		public event EventHandler Decipher;

		public event EventHandler Exit;
		public event EventHandler Properties;
		public event EventHandler LanguageChanged;

		public event EventHandler GenerateKeys;

		public event EventHandler AboutProgram;

		public Language Language
		{
			get => russianToolStripMenuItem.Checked ? Language.Russian : Language.English;
			set
			{
				englishToolStripMenuItem.CheckedChanged -= englishToolStripMenuItem_CheckedChanged;
				russianToolStripMenuItem.CheckedChanged -= russianToolStripMenuItem_CheckedChanged;

				russianToolStripMenuItem.Checked = false;
				englishToolStripMenuItem.Checked = false;
				if (value == Language.Russian)
					russianToolStripMenuItem.Checked = true;
				if (value == Language.English)
					englishToolStripMenuItem.Checked = true;

				englishToolStripMenuItem.CheckedChanged += englishToolStripMenuItem_CheckedChanged;
				russianToolStripMenuItem.CheckedChanged += russianToolStripMenuItem_CheckedChanged;
				if (Language == Language.English)
				{
					fileToolStripMenuItem.Text = @"File";
					cipherToolStripMenuItem.Text = @"Cipher";
					decipherToolStripMenuItem.Text = @"Decipher";
					exitToolStripMenuItem.Text = @"Exit";
					serviceToolStripMenuItem.Text = @"Service";
					propertiesToolStripMenuItem.Text = @"Properties";
					languageToolStripMenuItem.Text = @"Language";
					generateKeysToolStripMenuItem.Text = @"Generate keys";
					helpToolStripMenuItem.Text = @"Help";
					aboutProgramToolStripMenuItem.Text = @"About program";
				}

				if (Language == Language.Russian)
				{
					fileToolStripMenuItem.Text = @"Файл";
					cipherToolStripMenuItem.Text = @"Шифровать";
					decipherToolStripMenuItem.Text = @"Расшифровать";
					exitToolStripMenuItem.Text = @"Выход";
					serviceToolStripMenuItem.Text = @"Сервис";
					propertiesToolStripMenuItem.Text = @"Свойства";
					languageToolStripMenuItem.Text = @"Язык";
					generateKeysToolStripMenuItem.Text = @"Генерация ключей";
					helpToolStripMenuItem.Text = @"Помощь";
					aboutProgramToolStripMenuItem.Text = @"О программе";
				}
			}
		}

		public bool CipherEnabled
		{
			get => cipherToolStripMenuItem.Enabled;
			set => cipherToolStripMenuItem.Enabled = value;
		}

		public bool DecipherEnabled
		{
			get => decipherToolStripMenuItem.Enabled;
			set => decipherToolStripMenuItem.Enabled = value;
		}

		#endregion

		private void cipherToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Cipher != null)
			{
				Cipher(this, e);
			}
		}

		private void decipherToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Decipher != null)
			{
				Decipher(this, e);
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Exit != null)
			{
				Exit(this, e);
			}
		}

		private void generateKeysToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (GenerateKeys != null)
			{
				GenerateKeys(this, e);
			}
		}

		private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (AboutProgram != null)
			{
				AboutProgram(this, e);
			}
		}

		private void russianToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			OnLanguageChanged();
		}

		private void englishToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			OnLanguageChanged();
		}

		private void OnLanguageChanged()
		{
			if (LanguageChanged != null)
				LanguageChanged(this, EventArgs.Empty);
		}

		private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Properties != null)
				Properties(this, e);
		}

		private void russianToolStripMenuItem_Click(object sender, EventArgs e)
		{
			russianToolStripMenuItem.Checked = !russianToolStripMenuItem.Checked;
			englishToolStripMenuItem.Checked = !englishToolStripMenuItem.Checked;
		}

		private void englishToolStripMenuItem_Click(object sender, EventArgs e)
		{
			englishToolStripMenuItem.Checked = !englishToolStripMenuItem.Checked;
			russianToolStripMenuItem.Checked = !russianToolStripMenuItem.Checked;
		}
	}
}