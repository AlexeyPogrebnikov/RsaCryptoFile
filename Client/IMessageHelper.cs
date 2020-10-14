using System.Windows.Forms;

namespace CryptoFile.Client {
	public interface IMessageHelper {
		DialogResult Show(string englishMessage, string russianMessage);
		DialogResult Show(string englishMessage, string russianMessage, MessageBoxButtons buttons);
	}
}