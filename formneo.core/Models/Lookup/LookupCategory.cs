using System;
using System.ComponentModel.DataAnnotations;

namespace vesa.core.Models.Lookup
{
	public class LookupCategory : vesa.core.Models.BaseEntity
	{
		[Required]
		[StringLength(128)]
		public string Key { get; set; }

		[StringLength(512)]
		public string? Description { get; set; }

		public bool IsTenantScoped { get; set; }
		public bool IsReadOnly { get; set; }

		public Guid? ModuleId { get; set; }
		public LookupModule? Module { get; set; }
	}
}


