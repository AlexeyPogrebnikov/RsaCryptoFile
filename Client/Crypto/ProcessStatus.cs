namespace CryptoFile.Client.Crypto {
	public enum ProcessStatus {
		/// <summary>
		/// Процесс не начинался
		/// </summary>
		NotBeginning = 0,
		Processing,
		/// <summary>
		/// Процесс успешно завершился
		/// </summary>
		Complete,
		Stopped
	}
}