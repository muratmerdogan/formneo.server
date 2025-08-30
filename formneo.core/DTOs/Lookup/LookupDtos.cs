using System;

namespace vesa.core.DTOs.Lookup
{
	public class LookupCategoryDto
	{
		public Guid Id { get; set; }
		public string Key { get; set; }
		public string Description { get; set; }
		public bool IsTenantScoped { get; set; }
		public bool IsReadOnly { get; set; }
		public Guid? ModuleId { get; set; }
	}

	public class LookupModuleDto
	{
		public Guid Id { get; set; }
		public string Key { get; set; }
		public string Name { get; set; }
		public bool IsTenantScoped { get; set; }
		public bool IsReadOnly { get; set; }
	}

	public class LookupCategoryWithItemsDto
	{
		public Guid Id { get; set; }
		public string Key { get; set; }
		public string Description { get; set; }
		public bool IsTenantScoped { get; set; }
		public bool IsReadOnly { get; set; }
		public System.Collections.Generic.List<LookupItemDto> Items { get; set; }
	}

	public class LookupTreeDto
	{
		public Guid ModuleId { get; set; }
		public string ModuleKey { get; set; }
		public string ModuleName { get; set; }
		public System.Collections.Generic.List<LookupCategoryWithItemsDto> Categories { get; set; }
	}

	public class LookupItemDto
	{
		public Guid Id { get; set; }
		public Guid CategoryId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string? NameLocalizedJson { get; set; }
		public int OrderNo { get; set; }
		public bool IsActive { get; set; }
		public string? ExternalKey { get; set; }
	}
}


