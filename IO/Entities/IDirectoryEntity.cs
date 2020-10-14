using System.Collections.Generic;

namespace CryptoFile.IO.Entities {
	public interface IDirectoryEntity {
		List<FileSystemEntity> GetFiles();
		List<FileSystemEntity> GetDirectories();
		IDirectoryEntity GetParentDirectory();
		bool IsFile { get; }
		string FullName { get; }
		string Name { get; }
	}
}