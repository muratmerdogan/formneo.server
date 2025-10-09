using System;

namespace formneo.core.DTOs.ProjectTask
{
	public class ProjectTaskInsertDto
	{
		public Guid ProjectId { get; set; }
		public Guid? CustomerId { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public int Status { get; set; }
		public string? AssigneeId { get; set; }
	}
}


