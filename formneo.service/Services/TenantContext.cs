using System;
using vesa.core.Services;

namespace vesa.service.Services
{
	public class TenantContext : ITenantContext
	{
		public Guid? CurrentTenantId { get; set; }
	}
}


