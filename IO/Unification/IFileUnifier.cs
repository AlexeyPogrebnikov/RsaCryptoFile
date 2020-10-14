using System.Collections.Generic;
using CryptoFile.IO.Entities;

namespace CryptoFile.IO.Unification {
	public interface IFileUnifier {
		void Union(IList<FileSystemEntity> fileSystemEntities, string destinationFileName);
		void Split(string sourceFileName, string destinationPath);
	}
}