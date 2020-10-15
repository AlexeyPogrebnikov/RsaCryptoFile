using System.Diagnostics;
using System.IO;

namespace CryptoFile.Client.Environment
{
	public class EnvironmentHelper : IEnvironmentHelper
	{
		public string GetMyDocumentsPath()
		{
			return System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
		}

		public string GetTempFileName()
		{
			return Path.GetTempFileName();
		}

		public string GetTempPath()
		{
			string basePath = Path.GetTempPath();
			string path;
			var index = 0;
			do
			{
				path = Path.Combine(basePath, index.ToString());
				++index;
			} while (Directory.Exists(path));

			return path;
		}

		public void DeleteFile(string fileName)
		{
			File.Delete(fileName);
		}

		public void StartProcess(string fileName)
		{
			Process.Start(fileName);
		}

		public void DeleteDirectory(string path)
		{
			Directory.Delete(path, true);
		}

		public void CopyFile(string sourceFileName, string destinationFileName)
		{
			File.Copy(sourceFileName, destinationFileName, true);
		}

		public bool FileExists(string fileName)
		{
			return File.Exists(fileName);
		}

		public bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}
	}
}