using System;
using System.Threading;
using System.Windows.Forms;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Crypto;
using CryptoFile.Client.Forms;
using CryptoFile.Client.Serialization;
using CryptoFile.Library.Keys;
using CryptoFile.Library.LongArithmetic;

namespace CryptoFile.Client.Presenters
{
	internal class GenerateKeysFormPresenter
	{
		private readonly IGenerateKeysForm generateKeysForm;
		private readonly IKeyGenerator keyGenerator;
		private readonly Options options;
		private readonly IMessageHelper messageHelper;
		private readonly IFormFactory formFactory;
		private Thread thread;

		public GenerateKeysFormPresenter(IGenerateKeysForm generateKeysForm,
			IKeyGenerator keyGenerator,
			Options options,
			IMessageHelper messageHelper,
			IFormFactory formFactory)
		{
			this.generateKeysForm = generateKeysForm;
			this.keyGenerator = keyGenerator;
			this.options = options;
			this.messageHelper = messageHelper;
			this.formFactory = formFactory;
			generateKeysForm.MaxRsaKeyLength = options.MaxRsaKeyLength;
			generateKeysForm.MinRsaKeyLength = options.MinRsaKeyLength;
			generateKeysForm.RsaKeyLength = options.RsaKeyLength;
			RefreshPublicExponent();
			generateKeysForm.Generate += generateKeysForm_Generate;
			generateKeysForm.ChangePublicExponent += generateKeysForm_ChangePublicExponent;
			generateKeysForm.CancelGenerateKeys += generateKeysForm_CancelGenerateKeys;
		}

		private void generateKeysForm_Generate(object sender, EventArgs e)
		{
			generateKeysForm.GenerateEnabled = false;
			RefreshThread();
			thread.Start();
		}

		private void generateKeysForm_ChangePublicExponent(object sender, EventArgs e)
		{
			using (IPublicExponentForm form = formFactory.CreatePublicExponentForm())
			{
				form.SetPublicExponents(new[] { 3, 17, 257, 65537 });
				form.PublicExponent = options.PublicExponent;
				if (form.ShowDialog() == DialogResult.OK)
				{
					options.PublicExponent = form.PublicExponent;
					RefreshPublicExponent();
				}
			}
		}

		private void generateKeysForm_CancelGenerateKeys(object sender, EventArgs e)
		{
			if (keyGenerator.Status == ProcessStatus.Processing)
			{
				keyGenerator.Stop();
				thread.Abort();
				RefreshThread();
				generateKeysForm.GenerateEnabled = true;
			}
			else
			{
				generateKeysForm.DialogResult = DialogResult.Cancel;
			}
		}

		private void StartGenerateKeys()
		{
			options.RsaKeyLength = generateKeysForm.RsaKeyLength;
			RsaKey key = keyGenerator.Generate(options.RsaKeyLength, BigNumber.FromInt(options.PublicExponent));
			generateKeysForm.GenerateEnabled = true;
			if (key == null)
			{
				messageHelper.Show("Unable to generate a key.", "Не удалось сгенерировать ключ.");
				return;
			}

			var serializer = new KeySerializer(new BigNumberHexSerializer());
			generateKeysForm.PublicKey = serializer.SerializePublicKey(key.PublicKey);
			generateKeysForm.PrivateKey = serializer.SerializePrivateKey(key.PrivateKey);
		}

		private void RefreshThread()
		{
			thread = new Thread(StartGenerateKeys);
		}

		private void RefreshPublicExponent()
		{
			generateKeysForm.PublicExponent = options.PublicExponent;
		}
	}
}