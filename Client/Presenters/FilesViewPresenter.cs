using System;
using CryptoFile.Client.Commands;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Controls;
using CryptoFile.Client.Environment;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Entities.Wrappers;
using CryptoFile.IO.Sorting;
using CryptoFile.Library;

namespace CryptoFile.Client.Presenters {
	public class FilesViewPresenter : IFilesViewPresenter {
		private readonly IFilesView filesView;
		private readonly IToolBarView toolBarView;
		private readonly IAddressToolBar addressToolBar;
		private readonly ICommandsContainer commandsContainer;
		private readonly IEnvironmentHelper environmentHelper;
		private readonly Options options;
		private readonly IMessageHelper messageHelper;
		private IDirectoryEntity directory;
		private FileSorter sorter = new FileSorterByName();

		/// <exception cref="ArgumentNullException">any argument is null</exception>
		/// <exception cref="ArgumentException">defaultDirectory is empty</exception>
		public FilesViewPresenter(IFilesView filesView,
		                          IToolBarView toolBarView,
		                          IAddressToolBar addressToolBar,
		                          ICommandsContainer commandsContainer,
		                          IEnvironmentHelper environmentHelper,
		                          Options options,
		                          IMessageHelper messageHelper) {
			Checker.CheckNull(filesView, toolBarView, addressToolBar, commandsContainer, environmentHelper);
			this.filesView = filesView;
			this.toolBarView = toolBarView;
			this.addressToolBar = addressToolBar;
			this.commandsContainer = commandsContainer;
			this.environmentHelper = environmentHelper;
			this.options = options;
			this.messageHelper = messageHelper;
			sorter = FileSorter.CreateSorter(options.InitialSortColumn, options.InitialSortDirection);
			filesView.SortByName += view_SortByName;
			filesView.SortByLength += ViewSortByLength;
			filesView.SortByType += view_SortByType;
			filesView.SortByModifiedDate += ViewSortByModifiedDate;
			filesView.OpenDirectory += view_OpenDirectory;
			filesView.Cipher += view_Cipher;
			filesView.Decipher += view_Decipher;
			filesView.ToUpperDirectory += view_ToUpperDirectory;
			filesView.SelectedEntityChanged += view_SelectedEntityChanged;
			addressToolBar.PathChanged += addressToolBar_PathChanged;
		}

		private void view_SortByName(object sender, EventArgs e) {
			if (!(sorter is FileSorterByName))
				sorter = new FileSorterByName();
			else
				sorter.ChangeDirection();
			RefreshDirectory();
		}

		private void ViewSortByLength(object sender, EventArgs e) {
			if (!(sorter is FileSorterByLength))
				sorter = new FileSorterByLength();
			else
				sorter.ChangeDirection();
			RefreshDirectory();
		}

		private void view_SortByType(object sender, EventArgs e) {
			if (!(sorter is FileSorterByType))
				sorter = new FileSorterByType();
			else
				sorter.ChangeDirection();
			RefreshDirectory();
		}

		private void ViewSortByModifiedDate(object sender, EventArgs e) {
			if (!(sorter is FileSorterByModifiedDate))
				sorter = new FileSorterByModifiedDate();
			else
				sorter.ChangeDirection();
			RefreshDirectory();
		}

		public void OpenDefaultDirectory() {
			directory = CreateDefaultDirectory();
			RefreshDirectory();
		}

		public void ToUpperFolder() {
			InitializeDirectory();
			directory = directory.GetParentDirectory();
			RefreshDirectory();
		}

		public void RefreshDirectory() {
			InitializeDirectory();
			options.InitialDirectory = directory.FullName;
			filesView.RsaFileColor = options.RsaFileColor.Color;
			addressToolBar.PathChanged -= addressToolBar_PathChanged;
			addressToolBar.Path = directory.FullName;
			addressToolBar.PathChanged += addressToolBar_PathChanged;

			var files = directory.GetFiles();
			var directories = directory.GetDirectories();
			files.AddRange(directories);
			var sortingInfo = sorter.GetSortingInfo();
			options.InitialSortColumn = sortingInfo.SortColumn;
			options.InitialSortDirection = sortingInfo.SortDirection;
			filesView.SortingInfo = sortingInfo;
			sorter.Sort(files);
			filesView.SetFileSystemEntities(files);

			var parent = directory.GetParentDirectory();
			filesView.IsTerminal = parent == null;
			toolBarView.ToUpperDirectoryEnabled = parent != null;
			filesView.ResizeColumns();
		}

		private void view_OpenDirectory(object sender, EventArgs e) {
			if (filesView.SelectedDirectory != null) {
				var oldDirectory = directory;
				directory = filesView.SelectedDirectory;
				try {
					RefreshDirectory();
				} catch (FileEntityNotFoundException) {
					var englishMessage = string.Format("{0} not found.", directory.FullName);
					var russianMessage = string.Format("{0} не найден.", directory.FullName);
					messageHelper.Show(englishMessage, russianMessage);
					OpenDefaultDirectory();
				} catch (Exception) {
					var englishMessage = string.Format("Error when opening a folder {0}.", directory.FullName);
					var russianMessage = string.Format("Ошибка при открытии папки {0}.", directory.FullName);
					messageHelper.Show(englishMessage, russianMessage);
					directory = oldDirectory;
					RefreshDirectory();
				}
			}
		}

		private void view_Cipher(object sender, EventArgs e) {
			commandsContainer.CipherCommand.Execute();
		}

		private void view_Decipher(object sender, EventArgs e) {
			commandsContainer.DecipherCommand.Execute();
		}

		private void view_ToUpperDirectory(object sender, EventArgs e) {
			ToUpperFolder();
		}

		private void view_SelectedEntityChanged(object sender, EventArgs e) {
			filesView.OpenDirectoryEnabled = false;
			commandsContainer.RefreshCryptoViewsCommand.Execute();
			if (filesView.SelectedDirectory != null) {
				filesView.OpenDirectoryEnabled = true;
			}
		}

		private void addressToolBar_PathChanged(object sender, EventArgs e) {
			try {
				var path = addressToolBar.Path;
				var info = new DirectoryInfoWrapper(path);
				directory = new DirectoryEntity(info);
			} catch (FileEntityNotFoundException ex) {
				messageHelper.Show(ex.Message, ex.Message);
			}
			RefreshDirectory();
		}

		private IDirectoryEntity CreateDefaultDirectory() {
			var defaultDirectory = environmentHelper.GetMyDocumentsPath();
			return new DirectoryEntity(defaultDirectory);
		}

		private void InitializeDirectory() {
			if (directory == null) {
				directory = string.IsNullOrEmpty(options.InitialDirectory)
				            	? CreateDefaultDirectory()
				            	: new DirectoryEntity(options.InitialDirectory);
			}
		}
	}
}