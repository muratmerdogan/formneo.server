using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.BudgetManagement;

namespace vesa.core.DTOs.Budget.BudgetAdminUser
{
    public class BudgetAdminUserListDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Mail { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsDoProxy { get; set; }

        public string ProxyUser { get; set; }

        public AdminLevel AdminLevel { get; set; }

        public string adminLevelText { get; set; }



        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
