using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.BudgetManagement;
using formneo.core.Models;
using formneo.core.DTOs.Clients;

namespace formneo.core.DTOs.Budget.PeriodUserDto
{
    public class BudgetPeriodUserListDto
    {


        public BudgetPeriodListDto BudgetPeriod { get; set; }

        public Guid Id { get; set; }

        [Required]
        public string BudgetPeriodCode { get; set; }

        public string UserName { get; set; }

        public RequestType requestType { get; set; }

        public Permission permission { get; set; }

        public ProcessType processType { get; set; }

        public string requestTypeText { get; set; }

        public string permissiontypeText { get; set; }

        public string processtypeText { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? nameSurname { get; set; }


    }
}
