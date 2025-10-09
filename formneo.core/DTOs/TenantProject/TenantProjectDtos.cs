using System;
using System.Collections.Generic;

namespace formneo.core.DTOs.TenantProject
{
	public class TenantProjectInsertDto
	{
		public string Name { get; set; }
		public string? Description { get; set; }
		public Guid? CustomerId { get; set; }
		public bool IsPrivate { get; set; }
		public List<Guid>? ParentProjectIds { get; set; }
		public List<string>? ManagerIds { get; set; }
	}

	public class TenantProjectUpdateDto : TenantProjectInsertDto
	{
		public Guid Id { get; set; }
	}

	public class TenantProjectListDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public Guid? CustomerId { get; set; }
		public string? CustomerName { get; set; }
		public bool IsPrivate { get; set; }
	}
}


