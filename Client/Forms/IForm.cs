using System;
using System.Windows.Forms;
using CryptoFile.Client.Configuration;

namespace CryptoFile.Client.Forms {
	public interface IForm : IDisposable {
		event FormClosedEventHandler FormClosing;
		Language Language { get; set; }
		DialogResult DialogResult { get; set; }
		DialogResult ShowDialog();
	}
}