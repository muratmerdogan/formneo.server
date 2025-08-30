using System;

namespace vesa.core.Services
{
	public interface ITenantContext
	{
		Guid? CurrentTenantId { get; set; }
	}
}


