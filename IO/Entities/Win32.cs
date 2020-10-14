using System;
using System.Runtime.InteropServices;

namespace CryptoFile.IO.Entities {
	class Win32 {
		public const uint SHGFI_DISPLAYNAME = 0x00000200;
		public const uint SHGFI_ICON = 0x100;
		public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
		public const uint SHGFI_SMALLICON = 0x1; // 'Small icon
		public const uint SHGFI_TYPENAME = 0x400;

		[DllImport("shell32.dll")]
		public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi,
		                                          uint cbSizeFileInfo, uint uFlags);
	}
}