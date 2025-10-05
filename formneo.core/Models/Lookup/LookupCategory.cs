using System;
using System.ComponentModel.DataAnnotations;

namespace formneo.core.Models.Lookup
{
	public class TenantLookupCategory : formneo.core.Models.BaseEntity
	{
		[Required]
		[StringLength(128)]
		public string Key { get; set; }

		[StringLength(512)]
		public string? Description { get; set; }
		
        public bool IsReadOnly { get; set; }

		public Guid? ModuleId { get; set; }
		public TenantLookupModule? Module { get; set; }
	}
}


