using System;
using System.IO;

namespace CryptoFile.IO.Entities.Wrappers {
	public abstract class FileSystemInfoWrapper : IFileSystemInfo {
		protected FileSystemInfo Info { get; set; }

		public abstract IDirectoryInfo GetParentDirectory();

		public bool Exists {
			get { return Info.Exists; }
		}

		public string FullName {
			get { return Info.FullName; }
		}

		public string Name {
			get { return Info.Name; }
		}

		public DateTime ModifiedDate {
			get { return Info.LastWriteTime; }
		}

		public abstract long Length { get; }
	}
}