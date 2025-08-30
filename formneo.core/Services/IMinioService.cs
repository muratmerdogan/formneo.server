using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace vesa.core.Services
{
	public interface IMinioService
	{
		Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, string bucketName = "documents");
		Task<Stream> DownloadFileAsync(string fileName, string bucketName = "documents");
		Task<bool> DeleteFileAsync(string fileName, string bucketName = "documents");
		Task<bool> FileExistsAsync(string fileName, string bucketName = "documents");
		Task<string> GetFileUrlAsync(string fileName, string bucketName = "documents", int expiryInSeconds = 3600);
		Task<bool> CreateBucketIfNotExistsAsync(string bucketName);
		Task<IEnumerable<string>> ListFilesAsync(string bucketName = "documents", string prefix = "");
	}
}
