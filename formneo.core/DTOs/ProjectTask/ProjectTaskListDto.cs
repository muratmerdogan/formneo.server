using System;

namespace formneo.core.DTOs.ProjectTask
{
	public class ProjectTaskListDto
	{
		public Guid Id { get; set; }
		public Guid ProjectId { get; set; }
		public Guid? CustomerId { get; set; }
		public string? CustomerName { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public int Status { get; set; }
		public string StatusText { get; set; }
		public string? AssigneeId { get; set; }
		public string? AssigneeName { get; set; }
	}
}


