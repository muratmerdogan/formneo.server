using System;
using System.ComponentModel.DataAnnotations;

namespace formneo.core.Models.Lookup
{
	public class TenantLookupModule : BaseEntity
	{
		[Required]
		[StringLength(128)]
		public string Key { get; set; }

		[StringLength(256)]
		public string? Name { get; set; }

		public bool IsTenantScoped { get; set; }
		public bool IsReadOnly { get; set; }
	}
}



