using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Sorting;

namespace CryptoFile.Client.Controls {
	public interface IFilesView : ICryptoView {
		event EventHandler SortByName;
		event EventHandler SortByLength;
		event EventHandler SortByType;
		event EventHandler SortByModifiedDate;
		event EventHandler OpenDirectory;
		event EventHandler Cipher;
		event EventHandler Decipher;
		event EventHandler ToUpperDirectory;
		event EventHandler SelectedEntityChanged;
		void SetFileSystemEntities(IEnumerable<FileSystemEntity> entities);
		void ResizeColumns();
		bool IsTerminal { get; set; }
		Image DirectoryImage { get; set; }
		DirectoryEntity SelectedDirectory { get; }
		FileEntity SelectedFile { get; }
		ReadOnlyCollection<FileSystemEntity> SelectedEntities { get; }
		bool OpenDirectoryEnabled { get; set; }
		SortingInfo SortingInfo { get; set; }
		Color RsaFileColor { get; set; }
	}
}