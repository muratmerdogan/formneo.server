using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using vesa.core.DTOs.CRM;
using vesa.core.Services;

namespace vesa.api.Controllers.CRM
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerDocumentsController : ControllerBase
	{
		private readonly ICustomerDocumentService _customerDocumentService;

		public CustomerDocumentsController(ICustomerDocumentService customerDocumentService)
		{
			_customerDocumentService = customerDocumentService;
		}

		[HttpGet]
		public async Task<IActionResult> GetList()
		{
			var data = await _customerDocumentService.GetListAsync();
			return Ok(data);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var data = await _customerDocumentService.GetByIdAsync(id);
			if (data == null) return NotFound();
			return Ok(data);
		}

		[HttpGet("customer/{customerId}")]
		public async Task<IActionResult> GetByCustomerId(Guid customerId)
		{
			var data = await _customerDocumentService.GetByCustomerIdAsync(customerId);
			return Ok(data);
		}

		[HttpGet("category/{category}")]
		public async Task<IActionResult> GetByCategory(string category)
		{
			var data = await _customerDocumentService.GetByCategoryAsync(category);
			return Ok(data);
		}

		[HttpGet("customer/{customerId}/category/{category}")]
		public async Task<IActionResult> GetByCustomerAndCategory(Guid customerId, string category)
		{
			var data = await _customerDocumentService.GetByCustomerAndCategoryAsync(customerId, category);
			return Ok(data);
		}

		[HttpPost("upload")]
		public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] CustomerDocumentUploadDto dto)
		{
			if (file == null || file.Length == 0)
				return BadRequest("Dosya seçilmedi.");

			if (file.Length > 50 * 1024 * 1024) // 50MB limit
				return BadRequest("Dosya boyutu çok büyük. Maksimum 50MB olabilir.");

			try
			{
				using var stream = file.OpenReadStream();
				var result = await _customerDocumentService.UploadAsync(
					stream,
					file.FileName,
					file.ContentType,
					dto);

				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest($"Dosya yükleme hatası: {ex.Message}");
			}
		}

		[HttpGet("{id}/download")]
		public async Task<IActionResult> Download(Guid id)
		{
			try
			{
				var document = await _customerDocumentService.GetByIdAsync(id);
				if (document == null) return NotFound();

				var fileStream = await _customerDocumentService.DownloadAsync(id);

				return File(fileStream, document.ContentType, document.FileName);
			}
			catch (Exception ex)
			{
				return BadRequest($"Dosya indirme hatası: {ex.Message}");
			}
		}

		[HttpGet("{id}/download-url")]
		public async Task<IActionResult> GetDownloadUrl(Guid id, [FromQuery] int expiryInSeconds = 3600)
		{
			try
			{
				var url = await _customerDocumentService.GetDownloadUrlAsync(id, expiryInSeconds);
				return Ok(new { downloadUrl = url, expiryInSeconds });
			}
			catch (Exception ex)
			{
				return BadRequest($"URL oluşturma hatası: {ex.Message}");
			}
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] CustomerDocumentUpdateDto dto)
		{
			var updated = await _customerDocumentService.UpdateAsync(dto);
			if (updated == null) return NotFound();
			return Ok(updated);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _customerDocumentService.DeleteAsync(id);
			return NoContent();
		}

		[HttpGet("categories")]
		public IActionResult GetCategories()
		{
			var categories = new[]
			{
				"Sözleşme",
				"Fatura",
				"Teklif",
				"Kimlik",
				"Vergi Levhası",
				"İmza Sirküleri",
				"Diğer"
			};
			return Ok(categories);
		}
	}
}
