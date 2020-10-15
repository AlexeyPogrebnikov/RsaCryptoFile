using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using CryptoFile.IO.Entities.Wrappers;
using CryptoFile.Library;

namespace CryptoFile.IO.Entities
{
	public abstract class FileSystemEntity
	{
		private readonly IFileSystemInfo info;

		/// <exception cref="ArgumentNullException">info is null</exception>
		/// <exception cref="FileEntityNotFoundException">info.FullName not found</exception>
		protected FileSystemEntity(IFileSystemInfo info)
		{
			this.info = info;
			Checker.CheckNull(info);
			if (!info.Exists)
			{
				throw new FileEntityNotFoundException(info.FullName + " not found.");
			}

			Name = info.Name;
			FullName = info.FullName;
			ModifiedDate = info.ModifiedDate;
			var shinfo = new SHFILEINFO();
			Win32.SHGetFileInfo(info.FullName, 0, ref shinfo, (uint) Marshal.SizeOf(shinfo),
				Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON | Win32.SHGFI_TYPENAME);
			Type = shinfo.szTypeName;
			if (shinfo.hIcon.ToInt32() != 0)
				Icon = Icon.FromHandle(shinfo.hIcon);
		}

		/// <exception cref="FileEntityNotFoundException">parent folder not found</exception>
		public IDirectoryEntity GetParentDirectory()
		{
			IDirectoryInfo parent = info.GetParentDirectory();
			return parent == null ? null : new DirectoryEntity(parent);
		}

		public abstract byte[] GetData();

		public abstract List<FileSystemEntity> GetFiles();

		public abstract List<FileSystemEntity> GetDirectories();

		/// <summary>
		/// Возращает короткое имя сущности
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Возращает полное имя сущности
		/// </summary>
		public string FullName { get; protected set; }

		public abstract long Length { get; }

		/// <summary>
		/// Возвращает тип сущности
		/// </summary>
		public string Type { get; private set; }

		/// <summary>
		/// Возвращает дату последнего редактирования сущности
		/// </summary>
		public DateTime ModifiedDate { get; private set; }

		/// <summary>
		/// Возвращает иконку файла
		/// </summary>
		public Icon Icon { get; private set; }

		public abstract bool IsFile { get; }

		public bool IsCryptoFile { get; protected set; }

		public string Extension { get; protected set; }
	}
}