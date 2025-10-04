using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using formneo.core.DTOs.CRM;

namespace formneo.core.Services
{
	public interface ICustomerDocumentService
	{
		Task<CustomerDocumentDto> GetByIdAsync(Guid id);
		Task<IEnumerable<CustomerDocumentDto>> GetListAsync();
		Task<IEnumerable<CustomerDocumentDto>> GetByCustomerIdAsync(Guid customerId);
		Task<IEnumerable<CustomerDocumentDto>> GetByCategoryAsync(string category);
		Task<IEnumerable<CustomerDocumentDto>> GetByCustomerAndCategoryAsync(Guid customerId, string category);
		Task<CustomerDocumentDto> UploadAsync(Stream fileStream, string fileName, string contentType, CustomerDocumentUploadDto dto);
		Task<CustomerDocumentDto> UpdateAsync(CustomerDocumentUpdateDto dto);
		Task<Stream> DownloadAsync(Guid id);
		Task<string> GetDownloadUrlAsync(Guid id, int expiryInSeconds = 3600);
		Task DeleteAsync(Guid id);
	}
}
