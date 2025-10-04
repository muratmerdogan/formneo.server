using System;
using formneo.core.Services;

namespace formneo.service.Services
{
	public class TenantContext : ITenantContext
	{
		public Guid? CurrentTenantId { get; set; }
	}
}


