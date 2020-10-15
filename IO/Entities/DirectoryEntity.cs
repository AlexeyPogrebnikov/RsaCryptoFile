using System;
using System.Collections.Generic;
using System.IO;
using CryptoFile.IO.Entities.Wrappers;

namespace CryptoFile.IO.Entities
{
	public class DirectoryEntity : FileSystemEntity, IDirectoryEntity
	{
		private readonly IDirectoryInfo info;

		/// <exception cref="ArgumentNullException">info is null</exception>
		/// <exception cref="FileEntityNotFoundException">info.FullName not found</exception>
		public DirectoryEntity(IDirectoryInfo info)
			: base(info)
		{
			this.info = info;
		}

		public DirectoryEntity(string path)
			: this(new DirectoryInfoWrapper(path))
		{
		}

		/// <exception cref="FileEntityNotFoundException">Ошибка при получении списка файлов</exception>
		public override List<FileSystemEntity> GetFiles()
		{
			IFileInfo[] files;
			try
			{
				files = info.GetFiles();
			}
			catch (DirectoryNotFoundException e)
			{
				throw new FileEntityNotFoundException("Ошибка при получении списка файлов.", e);
			}

			var result = new List<FileSystemEntity>();
			for (var i = 0; i < files.Length; ++i)
			{
				result.Add(new FileEntity(files[i]));
			}

			return result;
		}

		/// <exception cref="FileEntityNotFoundException">Ошибка при получении списка папок</exception>
		public override List<FileSystemEntity> GetDirectories()
		{
			IDirectoryInfo[] directories;
			try
			{
				directories = info.GetDirectories();
			}
			catch (DirectoryNotFoundException e)
			{
				throw new FileEntityNotFoundException("Ошибка при получении списка папок.", e);
			}

			var result = new List<FileSystemEntity>();
			for (var i = 0; i < directories.Length; ++i)
			{
				result.Add(new DirectoryEntity(directories[i]));
			}

			return result;
		}

		public override long Length => info.Length;

		public override bool IsFile => false;

		public override byte[] GetData()
		{
			throw new NotSupportedException();
		}
	}
}