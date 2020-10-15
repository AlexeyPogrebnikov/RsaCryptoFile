namespace CryptoFile.IO.Entities.Wrappers
{
	public interface IFileInfo : IFileSystemInfo
	{
		string Extension { get; }
		byte[] GetData();
	}
}