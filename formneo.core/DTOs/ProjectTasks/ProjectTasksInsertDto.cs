using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.ProjectTasks
{
    public class ProjectTasksInsertDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int? taskId { get; set; }
        public Guid ProjectId { get; set; }
        public int? Duration { get; set; }
        public int? Progress { get; set; }
        public string? Predecessor { get; set; }
        public string? ParentId { get; set; }
        public bool? Milestone { get; set; }
        public string? Notes { get; set; }
        public bool? IsManual { get; set; }
        public List<TaskUsersDto>? Users { get; set; }
       
    }
    public class TaskUsersDto
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public int? Unit { get; set; }
       
    }
}
