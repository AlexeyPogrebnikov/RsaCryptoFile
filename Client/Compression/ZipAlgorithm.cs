using System;
using System.Text;
using CryptoFile.Client.Environment;
using CryptoFile.IO.Entities;
using CryptoFile.IO.Entities.Wrappers;
using CryptoFile.Library;
using Ionic.Zip;
using Ionic.Zlib;

namespace CryptoFile.Client.Compression
{
	public class ZipAlgorithm : IZipAlgorithm
	{
		private readonly IEnvironmentHelper environmentHelper;

		public ZipAlgorithm(IEnvironmentHelper environmentHelper)
		{
			this.environmentHelper = environmentHelper;
		}

		public void CompressFile(string sourceFileName, string destinationFileName)
		{
			Checker.CheckString(sourceFileName);
			Checker.CheckString(destinationFileName);
			try
			{
				using (var zipFile = new ZipFile(Encoding.Default))
				{
					zipFile.CompressionLevel = CompressionLevel.BestCompression;
					zipFile.AddFile(sourceFileName, string.Empty);
					zipFile.Save(destinationFileName);
				}
			}
			catch (Exception e)
			{
				throw new CompressionException(e);
			}
		}

		public void DecompressFile(string sourceFileName, string destinationFileName)
		{
			Checker.CheckString(sourceFileName);
			Checker.CheckString(destinationFileName);
			string temporaryPath = environmentHelper.GetTempPath();
			try
			{
				using (var zipFile = new ZipFile(sourceFileName, Encoding.Default))
				{
					zipFile.ExtractAll(temporaryPath);
				}

				var wrapper = new DirectoryInfoWrapper(temporaryPath);
				var directoryEntity = new DirectoryEntity(wrapper);
				FileSystemEntity file = directoryEntity.GetFiles()[0];
				environmentHelper.CopyFile(file.FullName, destinationFileName);
			}
			catch (Exception e)
			{
				throw new CompressionException(e);
			}
			finally
			{
				environmentHelper.DeleteDirectory(temporaryPath);
			}
		}
	}
}