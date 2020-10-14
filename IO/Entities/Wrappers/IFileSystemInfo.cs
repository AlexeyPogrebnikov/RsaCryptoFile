using System;

namespace CryptoFile.IO.Entities.Wrappers {
	public interface IFileSystemInfo {
		IDirectoryInfo GetParentDirectory();
		bool Exists { get; }
		string FullName { get; }
		string Name { get; }
		long Length { get; }
		DateTime ModifiedDate { get; }
	}
}