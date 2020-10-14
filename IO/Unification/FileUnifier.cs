using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CryptoFile.IO.Entities;
using Ionic.Zip;
using Ionic.Zlib;

namespace CryptoFile.IO.Unification {
	public class FileUnifier : IFileUnifier {
		public void Union(IList<FileSystemEntity> fileSystemEntities, string destinationFileName) {
			if (fileSystemEntities.Count == 0)
				throw new ArgumentException("fileSystemEntities is empty.");
			try {
				using (var zipFile = new ZipFile(Encoding.Default)) {
					zipFile.CompressionLevel = CompressionLevel.None;
					foreach (var entity in fileSystemEntities) {
						if (entity.IsFile) {
							zipFile.AddFile(entity.FullName, string.Empty);
						} else {
							zipFile.AddDirectory(entity.FullName, entity.Name);
						}
					}
					zipFile.Save(destinationFileName);
				}
			} catch (FileNotFoundException) {
				throw;
			} catch (DirectoryNotFoundException) {
				throw;
			} catch (Exception e) {
				throw new UnificationException(e);
			}
		}

		public void Split(string sourceFileName, string destinationPath) {
			if (!File.Exists(sourceFileName)) {
				throw new FileNotFoundException("file not found.", sourceFileName);
			}
			try {
				using (var zipReader = ZipFile.Read(sourceFileName, Encoding.Default)) {
					foreach (var zipEntry in zipReader) {
						zipEntry.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);
					}
				}
			} catch (Exception e) {
				throw new UnificationException(e);
			}
		}
	}
}