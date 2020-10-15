namespace CryptoFile.IO.Entities.Wrappers
{
	public interface IDirectoryInfo : IFileSystemInfo
	{
		IFileInfo[] GetFiles();
		IDirectoryInfo[] GetDirectories();
	}
}