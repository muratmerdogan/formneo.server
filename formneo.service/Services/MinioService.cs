using Minio;
using Minio.DataModel.Args;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using vesa.core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace vesa.service.Services
{
	public class MinioService : IMinioService
	{
		private readonly IMinioClient _minioClient;
		private readonly ILogger<MinioService> _logger;
		private readonly string _defaultBucket;

		public MinioService(IConfiguration configuration, ILogger<MinioService> logger)
		{
			_logger = logger;
			_defaultBucket = configuration.GetValue<string>("MinIO:DefaultBucket", "documents");

			var endpoint = configuration.GetValue<string>("MinIO:Endpoint", "localhost:9000");
			var accessKey = configuration.GetValue<string>("MinIO:AccessKey", "minioadmin");
			var secretKey = configuration.GetValue<string>("MinIO:SecretKey", "minioadmin123");
			var secure = configuration.GetValue<bool>("MinIO:Secure", false);

			_minioClient = new MinioClient()
				.WithEndpoint(endpoint)
				.WithCredentials(accessKey, secretKey)
				.WithSSL(secure)
				.Build();
		}

		public async Task<bool> CreateBucketIfNotExistsAsync(string bucketName)
		{
			try
			{
				var bucketExistsArgs = new BucketExistsArgs().WithBucket(bucketName);
				bool found = await _minioClient.BucketExistsAsync(bucketExistsArgs);

				if (!found)
				{
					var makeBucketArgs = new MakeBucketArgs().WithBucket(bucketName);
					await _minioClient.MakeBucketAsync(makeBucketArgs);
					_logger.LogInformation($"Bucket '{bucketName}' created successfully.");
				}

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error creating bucket '{bucketName}'");
				return false;
			}
		}

		public async Task<bool> DeleteFileAsync(string fileName, string bucketName = "documents")
		{
			try
			{
				await CreateBucketIfNotExistsAsync(bucketName);

				var removeObjectArgs = new RemoveObjectArgs()
					.WithBucket(bucketName)
					.WithObject(fileName);

				await _minioClient.RemoveObjectAsync(removeObjectArgs);
				_logger.LogInformation($"File '{fileName}' deleted from bucket '{bucketName}'");
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error deleting file '{fileName}' from bucket '{bucketName}'");
				return false;
			}
		}

		public async Task<Stream> DownloadFileAsync(string fileName, string bucketName = "documents")
		{
			try
			{
				await CreateBucketIfNotExistsAsync(bucketName);

				var memoryStream = new MemoryStream();
				var getObjectArgs = new GetObjectArgs()
					.WithBucket(bucketName)
					.WithObject(fileName)
					.WithCallbackStream(stream => stream.CopyTo(memoryStream));

				await _minioClient.GetObjectAsync(getObjectArgs);
				memoryStream.Position = 0;
				return memoryStream;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error downloading file '{fileName}' from bucket '{bucketName}'");
				throw;
			}
		}

		public async Task<bool> FileExistsAsync(string fileName, string bucketName = "documents")
		{
			try
			{
				await CreateBucketIfNotExistsAsync(bucketName);

				var statObjectArgs = new StatObjectArgs()
					.WithBucket(bucketName)
					.WithObject(fileName);

				await _minioClient.StatObjectAsync(statObjectArgs);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<string> GetFileUrlAsync(string fileName, string bucketName = "documents", int expiryInSeconds = 3600)
		{
			try
			{
				await CreateBucketIfNotExistsAsync(bucketName);

				var presignedGetObjectArgs = new PresignedGetObjectArgs()
					.WithBucket(bucketName)
					.WithObject(fileName)
					.WithExpiry(expiryInSeconds);

				return await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error generating URL for file '{fileName}' in bucket '{bucketName}'");
				throw;
			}
		}

		public async Task<IEnumerable<string>> ListFilesAsync(string bucketName = "documents", string prefix = "")
		{
			try
			{
				await CreateBucketIfNotExistsAsync(bucketName);

				var files = new List<string>();
				var listObjectsArgs = new ListObjectsArgs()
					.WithBucket(bucketName)
					.WithPrefix(prefix)
					.WithRecursive(true);

				await foreach (var item in _minioClient.ListObjectsEnumAsync(listObjectsArgs))
				{
					files.Add(item.Key);
				}

				return files;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error listing files in bucket '{bucketName}' with prefix '{prefix}'");
				throw;
			}
		}

		public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, string bucketName = "documents")
		{
			try
			{
				await CreateBucketIfNotExistsAsync(bucketName);

				// Unique filename with timestamp
				var uniqueFileName = $"{DateTime.UtcNow:yyyyMMdd_HHmmss}_{Guid.NewGuid():N}_{fileName}";

				var putObjectArgs = new PutObjectArgs()
					.WithBucket(bucketName)
					.WithObject(uniqueFileName)
					.WithStreamData(fileStream)
					.WithObjectSize(fileStream.Length)
					.WithContentType(contentType);

				await _minioClient.PutObjectAsync(putObjectArgs);
				_logger.LogInformation($"File '{fileName}' uploaded as '{uniqueFileName}' to bucket '{bucketName}'");

				return uniqueFileName;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error uploading file '{fileName}' to bucket '{bucketName}'");
				throw;
			}
		}
	}
}
