using System;
using System.IO;

namespace CryptoFile.IO.Entities.Wrappers
{
	public abstract class FileSystemInfoWrapper : IFileSystemInfo
	{
		protected FileSystemInfo Info { get; set; }

		public abstract IDirectoryInfo GetParentDirectory();

		public bool Exists => Info.Exists;

		public string FullName => Info.FullName;

		public string Name => Info.Name;

		public DateTime ModifiedDate => Info.LastWriteTime;

		public abstract long Length { get; }
	}
}