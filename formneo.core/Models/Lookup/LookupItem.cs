using System;
using System.ComponentModel.DataAnnotations;

namespace formneo.core.Models.Lookup
{
	public class TenantLookupItem : formneo.core.Models.BaseEntity
	{
        public Guid CategoryId { get; set; }
        public TenantLookupCategory Category { get; set; }

		[StringLength(64)]
		public string Code { get; set; }

		[StringLength(256)]
		public string Name { get; set; }

		public string? NameLocalizedJson { get; set; }
		public int OrderNo { get; set; }
		public bool IsActive { get; set; } = true;
		[StringLength(128)]
		public string? ExternalKey { get; set; }
	}
}


