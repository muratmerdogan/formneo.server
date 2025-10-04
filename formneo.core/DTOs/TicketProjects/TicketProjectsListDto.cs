using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs.DepartmentUserDto;
using formneo.core.Models;

namespace formneo.core.DTOs.TicketProjects
{
    public class TicketProjectsListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? SubProjectName { get; set; }
        public string? Description { get; set; }
        public string? Risks { get; set; }
        public string? ReportsUrl { get; set; }
        public bool? IsActive { get; set; }
        public Guid? WorkCompanyId { get; set; }
        public WorkCompany? WorkCompany { get; set; }
        // proje sorumlusuc 
        [ForeignKey("UserApp")]
        public string? ManagerId { get; set; }
        public virtual UserApp? Manager { get; set; }

        // proje çalışanları
        public List<UserAppDto>? Users { get; set; }
        public List<string>? UserIds { get; set; }
        public string? UserIdsJoin { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? ProjectCategoryId { get; set; }
        public ProjectCategoriesListDto? ProjectCategory { get; set; }
        public string? CopiedProjectId { get; set; }
        public bool? IsUserCopied { get; set; }
    }

    public class ChangedTaskListDto
    {
        public Guid? TaskId { get; set; }
        public string? TaskName { get; set; }
        public int? Progress { get; set; }
        public string? ProjectName { get; set; }
        public string? CompanyName { get; set; }
        public string? ManagerName { get; set; }    
        public string? ChangeType { get; set; }
        public DateTime? DateOfChange { get; set; }
        public List<string>? AssignUserIds { get; set; }
        public List<UserAppDtoOnlyNameId>? AssignUsers { get; set; }
    }
}
