using System.IO;

namespace CryptoFile.IO.Entities.Wrappers {
	public class DirectoryInfoWrapper : FileSystemInfoWrapper, IDirectoryInfo {
		private readonly DirectoryInfo info;

		public DirectoryInfoWrapper(string path)
			: this(new DirectoryInfo(path)) {}

		public DirectoryInfoWrapper(DirectoryInfo info) {
			this.info = info;
			Info = info;
		}

		public override IDirectoryInfo GetParentDirectory() {
			return info.Parent == null ? null : new DirectoryInfoWrapper(info.Parent);
		}

		public IFileInfo[] GetFiles() {
			var fileInfos = info.GetFiles();
			var files = new IFileInfo[fileInfos.Length];
			for (var i = 0; i < fileInfos.Length; ++i) {
				files[i] = new FileInfoWrapper(fileInfos[i], this);
			}
			return files;
		}

		public IDirectoryInfo[] GetDirectories() {
			var directoryInfos = info.GetDirectories();
			var directories = new IDirectoryInfo[directoryInfos.Length];
			for (var i = 0; i < directoryInfos.Length; ++i) {
				directories[i] = new DirectoryInfoWrapper(directoryInfos[i]);
			}
			return directories;
		}

		public override long Length {
			get {
				long length = 0;
				foreach (var file in info.GetFiles("*", SearchOption.AllDirectories))
					length += file.Length;
				return length;
			}
		}
	}
}