using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using vesa.core.Services;

namespace vesa.api.Controllers.CRM
{
	[Route("api/crm/[controller]")]
	[ApiController]
	public class CrmChangeLogsController : ControllerBase
	{
		private readonly ICrmChangeLogService _service;
		public CrmChangeLogsController(ICrmChangeLogService service){_service=service;}

		[HttpGet("customer/{customerId}")]
		public async Task<IActionResult> ListByCustomer(Guid customerId){var data=await _service.ListByCustomerAsync(customerId);return Ok(data);}

		[HttpGet("entity/{entityName}/{entityId}")]
		public async Task<IActionResult> ListByEntity(string entityName, Guid entityId){var data=await _service.ListByEntityAsync(entityName, entityId);return Ok(data);}
	}
}


