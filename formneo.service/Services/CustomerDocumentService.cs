using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using formneo.core.DTOs.CRM;
using formneo.core.Models.CRM;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;
using formneo.service.Exceptions;

namespace formneo.service.Services
{
	public class CustomerDocumentService : ICustomerDocumentService
	{
		private readonly ICustomerDocumentRepository _customerDocumentRepository;
		private readonly IMinioService _minioService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private const string BUCKET_NAME = "customer-documents";

		public CustomerDocumentService(
			ICustomerDocumentRepository customerDocumentRepository,
			IMinioService minioService,
			IUnitOfWork unitOfWork,
			IMapper mapper)
		{
			_customerDocumentRepository = customerDocumentRepository;
			_minioService = minioService;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _customerDocumentRepository.GetByIdAsync(id);
			if (entity == null) return;

			// MinIO'dan dosyayı sil
			if (!string.IsNullOrEmpty(entity.FilePath))
			{
				await _minioService.DeleteFileAsync(entity.FilePath, BUCKET_NAME);
			}

			// Soft delete
			entity.IsDelete = true;
			entity.UpdatedDate = DateTime.UtcNow;
			_customerDocumentRepository.Update(entity);
			await _unitOfWork.CommitAsync();
		}

		public async Task<Stream> DownloadAsync(Guid id)
		{
			var entity = await _customerDocumentRepository.GetByIdAsync(id);
			if (entity == null)
				throw new ClientSideException("Doküman bulunamadı.");

			if (string.IsNullOrEmpty(entity.FilePath))
				throw new ClientSideException("Dosya yolu bulunamadı.");

			return await _minioService.DownloadFileAsync(entity.FilePath, BUCKET_NAME);
		}

		public async Task<IEnumerable<CustomerDocumentDto>> GetByCategoryAsync(string category)
		{
			var documents = await _customerDocumentRepository.GetByCategoryAsync(category);
			var documentDtos = _mapper.Map<List<CustomerDocumentDto>>(documents);

			// Download URL'lerini ekle
			foreach (var dto in documentDtos)
			{
				if (!string.IsNullOrEmpty(dto.FilePath))
				{
					try
					{
						dto.DownloadUrl = await _minioService.GetFileUrlAsync(dto.FilePath, BUCKET_NAME);
					}
					catch
					{
						dto.DownloadUrl = null;
					}
				}
			}

			return documentDtos;
		}

		public async Task<IEnumerable<CustomerDocumentDto>> GetByCustomerAndCategoryAsync(Guid customerId, string category)
		{
			var documents = await _customerDocumentRepository.GetByCustomerAndCategoryAsync(customerId, category);
			var documentDtos = _mapper.Map<List<CustomerDocumentDto>>(documents);

			// Download URL'lerini ekle
			foreach (var dto in documentDtos)
			{
				if (!string.IsNullOrEmpty(dto.FilePath))
				{
					try
					{
						dto.DownloadUrl = await _minioService.GetFileUrlAsync(dto.FilePath, BUCKET_NAME);
					}
					catch
					{
						dto.DownloadUrl = null;
					}
				}
			}

			return documentDtos;
		}

		public async Task<IEnumerable<CustomerDocumentDto>> GetByCustomerIdAsync(Guid customerId)
		{
			var documents = await _customerDocumentRepository.GetByCustomerIdAsync(customerId);
			var documentDtos = _mapper.Map<List<CustomerDocumentDto>>(documents);

			// Download URL'lerini ekle
			foreach (var dto in documentDtos)
			{
				if (!string.IsNullOrEmpty(dto.FilePath))
				{
					try
					{
						dto.DownloadUrl = await _minioService.GetFileUrlAsync(dto.FilePath, BUCKET_NAME);
					}
					catch
					{
						dto.DownloadUrl = null;
					}
				}
			}

			return documentDtos;
		}

		public async Task<CustomerDocumentDto> GetByIdAsync(Guid id)
		{
			var entity = await _customerDocumentRepository.GetDetailAsync(id);
			if (entity == null) return null;

			var dto = _mapper.Map<CustomerDocumentDto>(entity);

			// Download URL'ini ekle
			if (!string.IsNullOrEmpty(dto.FilePath))
			{
				try
				{
					dto.DownloadUrl = await _minioService.GetFileUrlAsync(dto.FilePath, BUCKET_NAME);
				}
				catch
				{
					dto.DownloadUrl = null;
				}
			}

			return dto;
		}

		public async Task<string> GetDownloadUrlAsync(Guid id, int expiryInSeconds = 3600)
		{
			var entity = await _customerDocumentRepository.GetByIdAsync(id);
			if (entity == null)
				throw new ClientSideException("Doküman bulunamadı.");

			if (string.IsNullOrEmpty(entity.FilePath))
				throw new ClientSideException("Dosya yolu bulunamadı.");

			return await _minioService.GetFileUrlAsync(entity.FilePath, BUCKET_NAME, expiryInSeconds);
		}

		public async Task<IEnumerable<CustomerDocumentDto>> GetListAsync()
		{
			var documents = await _customerDocumentRepository.GetAllAsync();
			var documentDtos = _mapper.Map<List<CustomerDocumentDto>>(documents.OrderByDescending(x => x.CreatedDate));

			// Download URL'lerini ekle
			foreach (var dto in documentDtos)
			{
				if (!string.IsNullOrEmpty(dto.FilePath))
				{
					try
					{
						dto.DownloadUrl = await _minioService.GetFileUrlAsync(dto.FilePath, BUCKET_NAME);
					}
					catch
					{
						dto.DownloadUrl = null;
					}
				}
			}

			return documentDtos;
		}

		public async Task<CustomerDocumentDto> UpdateAsync(CustomerDocumentUpdateDto dto)
		{
			var entity = await _customerDocumentRepository.GetByIdAsync(dto.Id);
			if (entity == null) return null;

			// Sadece metadata güncellenebilir, dosya değiştirilemez
			entity.FileName = dto.FileName ?? entity.FileName;
			entity.Description = dto.Description ?? entity.Description;
			entity.Category = dto.Category ?? entity.Category;
			entity.UpdatedDate = DateTime.UtcNow;

			_customerDocumentRepository.Update(entity);
			
			try
			{
				await _unitOfWork.CommitAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");
			}

			var result = _mapper.Map<CustomerDocumentDto>(entity);

			// Download URL'ini ekle
			if (!string.IsNullOrEmpty(result.FilePath))
			{
				try
				{
					result.DownloadUrl = await _minioService.GetFileUrlAsync(result.FilePath, BUCKET_NAME);
				}
				catch
				{
					result.DownloadUrl = null;
				}
			}

			return result;
		}

		public async Task<CustomerDocumentDto> UploadAsync(Stream fileStream, string fileName, string contentType, CustomerDocumentUploadDto dto)
		{
			try
			{
				// MinIO'ya dosyayı yükle
				var filePath = await _minioService.UploadFileAsync(fileStream, fileName, contentType, BUCKET_NAME);

				// Veritabanına kaydet
				var entity = new CustomerDocument
				{
					CustomerId = dto.CustomerId,
					FileName = fileName,
					FilePath = filePath,
					ContentType = contentType,
					FileSize = fileStream.Length,
					Description = dto.Description,
					Category = dto.Category,
					CreatedDate = DateTime.UtcNow
				};

				await _customerDocumentRepository.AddAsync(entity);
				await _unitOfWork.CommitAsync();

				var result = _mapper.Map<CustomerDocumentDto>(entity);

				// Download URL'ini ekle
				try
				{
					result.DownloadUrl = await _minioService.GetFileUrlAsync(filePath, BUCKET_NAME);
				}
				catch
				{
					result.DownloadUrl = null;
				}

				return result;
			}
			catch (Exception ex)
			{
				throw new ClientSideException($"Dosya yükleme hatası: {ex.Message}");
			}
		}
	}
}
