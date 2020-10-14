using System.IO;

namespace CryptoFile.IO.Entities.Wrappers {
	public class FileInfoWrapper : FileSystemInfoWrapper, IFileInfo {
		private readonly FileInfo info;
		private readonly IDirectoryInfo parentDirectory;

		public FileInfoWrapper(FileInfo info, IDirectoryInfo parentDirectory) {
			this.info = info;
			this.parentDirectory = parentDirectory;
			Info = info;
		}

		public override IDirectoryInfo GetParentDirectory() {
			return parentDirectory;
		}

		public override long Length {
			get { return info.Length; }
		}

		public string Extension {
			get { return info.Extension; }
		}

		public byte[] GetData() {
			using (var stream = info.OpenRead()) {
				var buffer = new byte[stream.Length];
				stream.Read(buffer, 0, (int)stream.Length);
				return buffer;
			}
		}
	}
}