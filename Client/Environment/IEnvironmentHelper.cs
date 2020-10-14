namespace CryptoFile.Client.Environment {
	public interface IEnvironmentHelper {
		string GetMyDocumentsPath();
		string GetTempFileName();
		string GetTempPath();
		void DeleteFile(string fileName);
		void StartProcess(string fileName);
		void DeleteDirectory(string path);
		void CopyFile(string sourceFileName, string destinationFileName);
		bool FileExists(string fileName);
		bool DirectoryExists(string path);
	}
}