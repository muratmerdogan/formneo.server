using System;

namespace formneo.core.DTOs.ProjectTeamMember
{
	public class ProjectTeamMemberListDto
	{
		public Guid Id { get; set; }
		public Guid ProjectId { get; set; }
		public string UserId { get; set; }
		public string? UserName { get; set; }
		public string? FullName { get; set; }
		public string? Role { get; set; }
		public bool IsActive { get; set; }
	}

	public class ProjectTeamMemberInsertDto
	{
		public Guid ProjectId { get; set; }
		public string UserId { get; set; }
		public string? Role { get; set; }
	}

	public class ProjectTeamMemberUpdateDto
	{
		public Guid Id { get; set; }
		public string? Role { get; set; }
		public bool IsActive { get; set; }
	}
}


