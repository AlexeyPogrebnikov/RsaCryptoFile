using System;
using System.Collections.Generic;
using CryptoFile.IO.Entities.Wrappers;

namespace CryptoFile.IO.Entities
{
	public class FileEntity : FileSystemEntity
	{
		private readonly IFileInfo info;

		/// <exception cref="ArgumentNullException">info is null</exception>
		/// <exception cref="FileEntityNotFoundException">info.FullName not found</exception>
		public FileEntity(IFileInfo info)
			: base(info)
		{
			this.info = info;
			Extension = info.Extension;
			IsCryptoFile = string.Compare(Extension, ".rsa", StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		public override long Length => info.Length;

		public override bool IsFile => true;

		public override byte[] GetData()
		{
			return info.GetData();
		}

		public override List<FileSystemEntity> GetFiles()
		{
			throw new NotSupportedException();
		}

		public override List<FileSystemEntity> GetDirectories()
		{
			throw new NotSupportedException();
		}
	}
}