using System;

namespace formneo.core.Services
{
	public interface ITenantContext
	{
		Guid? CurrentTenantId { get; set; }
	}
}


