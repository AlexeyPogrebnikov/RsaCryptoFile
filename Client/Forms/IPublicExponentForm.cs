using System.Collections.Generic;

namespace CryptoFile.Client.Forms {
	public interface IPublicExponentForm : IForm {
		void SetPublicExponents(IEnumerable<int> publicExponents);
		int PublicExponent { get; set; }
	}
}