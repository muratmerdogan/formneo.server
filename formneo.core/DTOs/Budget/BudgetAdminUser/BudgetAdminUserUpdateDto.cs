using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.BudgetManagement;

namespace vesa.core.DTOs.Budget.BudgetAdminUser
{
    public class BudgetAdminUserUpdateDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Mail { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsDoProxy { get; set; }

        public string ProxyUser { get; set; }

        public AdminLevel AdminLevel { get; set; }

    }
}
