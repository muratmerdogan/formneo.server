using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.Ticket;

namespace formneo.core.Models
{
    public class UserApp : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public bool isSystemAdmin { get; set; }
        public bool canSsoLogin { get; set; }
        public bool isBlocked { get; set; }
        public bool? isTestData { get; set; }
        public bool vacationMode { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? LastLoginIp { get; set; }

        public string? profileInfo { get; set; }
        public string? Title { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Location { get; set; }
        public string? FacebookUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? LinkedinUrl { get; set; }
        public string? photo { get; set; }

        public string? Department { get; set; }

        public string? SAPDepartmentText { get; set; }
        public string? SAPPositionText { get; set; }

        [ForeignKey("TicketDepartment")]
        public Guid? TicketDepartmentId { get; set; }
        public virtual TicketDepartment? TicketDepartment { get; set; }


        [ForeignKey("WorkCompany")]
        public Guid? WorkCompanyId { get; set; }
        public virtual WorkCompany? WorkCompany { get; set; }

        // tenant-bazlı alanlar UserTenant'a taşındı
        public string? ResetPasswordCode { get; set; }
        public DateTime? ResetCodeExpiry { get; set; }
        // tenant-bazlı alanlar UserTenant'a taşındı


        [ForeignKey("Positions")]
        public Guid? PositionId { get; set; }
        public virtual Positions? Positions { get; set; }
        public virtual List<DepartmentUser> DepartmentUsers { get; set; } = new List<DepartmentUser>();

        public UserLevel UserLevel { get; set; }



    }
    public enum UserLevel
    {
        [Description("Junior")]
        junior = 1,
        [Description("Mid")]
        mid = 2,
        [Description("Senior")]
        senior = 3,
    }

}
