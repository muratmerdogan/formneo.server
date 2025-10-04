using System;
using System.ComponentModel.DataAnnotations;

namespace formneo.core.Models.Lookup
{
	public class LookupCategory : formneo.core.Models.GlobalBaseEntity
	{
		[Required]
		[StringLength(128)]
		public string Key { get; set; }

		[StringLength(512)]
		public string? Description { get; set; }

		public bool IsTenantScoped { get; set; }
		public bool IsReadOnly { get; set; }

		public Guid? TenantId { get; set; }

		public Guid? ModuleId { get; set; }
		public LookupModule? Module { get; set; }
	}
}


