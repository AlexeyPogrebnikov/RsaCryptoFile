using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using CryptoFile.Client.Configuration;
using CryptoFile.Client.Properties;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Sorting;

namespace CryptoFile.Client.Controls
{
	internal partial class FilesView : UserControl, IFilesView
	{
		private bool isTerminal;
		private readonly ListViewItem toUpperListViewItem;
		private Language language;

		public FilesView()
		{
			InitializeComponent();
			toUpperListViewItem = new ListViewItem("..") { ImageIndex = 0 };
			toUpperListViewItem.SubItems.Add("                       ");
		}

		#region IFilesView Members

		public event EventHandler SortByName;
		public event EventHandler SortByLength;
		public event EventHandler SortByType;
		public event EventHandler SortByModifiedDate;
		public event EventHandler OpenDirectory;
		public event EventHandler ToUpperDirectory;
		public event EventHandler Cipher;
		public event EventHandler Decipher;
		public event EventHandler SelectedEntityChanged;

		public void SetFileSystemEntities(IEnumerable<FileSystemEntity> entities)
		{
			listView.BeginUpdate();
			Clear();
			foreach (FileSystemEntity entity in entities)
			{
				AddItem(entity);
			}

			listView.EndUpdate();
			OnSelectedEntityChanged();
		}

		public void ResizeColumns()
		{
			listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
			for (var i = 0; i < listView.Columns.Count; ++i)
			{
				ColumnHeader column = listView.Columns[i];
				int width = Convert.ToInt32(graphics.MeasureString(column.Text, listView.Font).Width) + 45;
				column.Width = Math.Max(width, column.Width);
			}
		}

		public bool IsTerminal
		{
			get => isTerminal;
			set
			{
				if (isTerminal)
				{
					toUpperListViewItem.Remove();
				}

				isTerminal = value;
				if (!isTerminal)
				{
					listView.Items.Insert(0, toUpperListViewItem);
				}
			}
		}

		public Image DirectoryImage { get; set; }

		public DirectoryEntity SelectedDirectory
		{
			get
			{
				if (listView.SelectedItems.Count != 1)
				{
					return null;
				}

				ListViewItem item = listView.SelectedItems[0];
				if (item.Tag is DirectoryEntity)
				{
					return (DirectoryEntity) item.Tag;
				}

				return null;
			}
		}

		public FileEntity SelectedFile
		{
			get
			{
				if (listView.SelectedItems.Count != 1)
				{
					return null;
				}

				ListViewItem item = listView.SelectedItems[0];
				if (item.Tag is FileEntity)
				{
					return (FileEntity) item.Tag;
				}

				return null;
			}
		}

		public ReadOnlyCollection<FileSystemEntity> SelectedEntities
		{
			get
			{
				var fileSystemEntities = new List<FileSystemEntity>();
				foreach (ListViewItem item in listView.SelectedItems)
				{
					object tag = item.Tag;
					if (tag != null)
					{
						var entity = (FileSystemEntity) tag;
						fileSystemEntities.Add(entity);
					}
				}

				return fileSystemEntities.AsReadOnly();
			}
		}

		public bool OpenDirectoryEnabled
		{
			get => openToolStripMenuItem.Enabled;
			set => openToolStripMenuItem.Enabled = value;
		}

		public SortingInfo SortingInfo { get; set; }

		public Color RsaFileColor { get; set; }

		public bool CipherEnabled
		{
			get => cipherToolStripMenuItem.Enabled;
			set => cipherToolStripMenuItem.Enabled = value;
		}

		public bool DecipherEnabled
		{
			get => decipherToolStripMenuItem.Enabled;
			set => decipherToolStripMenuItem.Enabled = value;
		}

		public Language Language
		{
			get => language;
			set
			{
				language = value;
				if (language == Language.English)
				{
					nameColumnHeader.Text = @"Name";
					lengthColumnHeader.Text = @"Length";
					typeColumnHeader.Text = @"Type";
					modifiedColumnHeader.Text = @"Modified";
					openToolStripMenuItem.Text = @"Open";
					cipherToolStripMenuItem.Text = @"Cipher";
					decipherToolStripMenuItem.Text = @"Decipher";
				}

				if (language == Language.Russian)
				{
					nameColumnHeader.Text = @"Имя";
					lengthColumnHeader.Text = @"Размер";
					typeColumnHeader.Text = @"Тип";
					modifiedColumnHeader.Text = @"Изменен";
					openToolStripMenuItem.Text = @"Открыть";
					cipherToolStripMenuItem.Text = @"Шифровать";
					decipherToolStripMenuItem.Text = @"Расшифровать";
				}
			}
		}

		#endregion

		private void Clear()
		{
			listView.Items.Clear();
			listView.SmallImageList.Images.Clear();
			listView.SmallImageList.Images.Add(DirectoryImage);
		}

		private void AddItem(FileSystemEntity entity)
		{
			listView.SmallImageList.Images.Add(entity.Icon);
			var item = new ListViewItem(entity.Name);
			if (entity.IsCryptoFile)
				item.BackColor = RsaFileColor;
			item.ImageIndex = listView.SmallImageList.Images.Count - 1;
			item.Tag = entity;
			item.SubItems.Add(entity.IsFile ? entity.Length.ToString() : string.Empty);
			item.SubItems.Add(entity.Type);
			item.SubItems.Add(entity.ModifiedDate.ToString());
			listView.Items.Add(item);
		}

		private void listView_DoubleClick(object sender, EventArgs e)
		{
			if (listView.SelectedItems.Count != 1)
			{
				return;
			}

			ListViewItem item = listView.SelectedItems[0];
			if (item.Index == 0 && !IsTerminal)
			{
				OnToUpperDirectory();
			}

			if (item.Tag is DirectoryEntity)
			{
				OnOpenDirectory();
			}

			if (item.Tag is FileEntity)
			{
				if (SelectedFile.IsCryptoFile)
				{
					OnDecipher();
				}
				else
				{
					OnCipher();
				}
			}
		}

		private void listView_KeyDown(object sender, KeyEventArgs e)
		{
			if (listView.SelectedItems.Count != 1)
			{
				return;
			}

			if (e.KeyCode == Keys.Enter)
			{
				ListViewItem item = listView.SelectedItems[0];
				if (item.Index == 0 && !IsTerminal)
				{
					OnToUpperDirectory();
				}

				if (item.Tag is DirectoryEntity)
				{
					OnOpenDirectory();
				}

				if (item.Tag is FileEntity)
				{
					if (SelectedFile.IsCryptoFile)
					{
						OnDecipher();
					}
					else
					{
						OnCipher();
					}
				}
			}
		}

		private void listView_SelectedIndexChanged(object sender, EventArgs e)
		{
			OnSelectedEntityChanged();
		}

		private void cipherToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnCipher();
		}

		private void decipherToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnDecipher();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OnOpenDirectory();
		}

		private void OnOpenDirectory()
		{
			if (OpenDirectory != null)
			{
				OpenDirectory(this, EventArgs.Empty);
			}
		}

		private void OnToUpperDirectory()
		{
			if (ToUpperDirectory != null)
			{
				ToUpperDirectory(this, EventArgs.Empty);
			}
		}

		private void OnCipher()
		{
			if (Cipher != null)
			{
				Cipher(this, EventArgs.Empty);
			}
		}

		private void OnDecipher()
		{
			if (Decipher != null)
			{
				Decipher(this, EventArgs.Empty);
			}
		}

		private void OnSelectedEntityChanged()
		{
			if (SelectedEntityChanged != null)
			{
				SelectedEntityChanged(this, EventArgs.Empty);
			}
		}

		private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (e.Column == 0 && SortByName != null)
				SortByName(this, e);

			if (e.Column == 1 && SortByLength != null)
				SortByLength(this, e);

			if (e.Column == 2 && SortByType != null)
				SortByType(this, e);

			if (e.Column == 3 && SortByModifiedDate != null)
				SortByModifiedDate(this, e);
		}

		private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			if (SortingInfo == null || e.ColumnIndex != (int) SortingInfo.SortColumn)
			{
				e.DrawBackground();
				e.DrawText(TextFormatFlags.VerticalCenter);
				return;
			}

			e.DrawBackground();
			e.DrawText(TextFormatFlags.VerticalCenter);
			Bitmap image = SortingInfo.SortDirection == SortDirection.Ascending ? Resources.ascending : Resources.descending;
			int x = e.Bounds.X + e.Bounds.Width - image.Width - 5;
			int y = (e.Bounds.Height - image.Height) / 2 - 1;
			e.Graphics.DrawImage(image, x, y);
		}

		private static void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
			e.DrawDefault = true;
		}

		private static void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			e.DrawDefault = true;
		}
	}
}