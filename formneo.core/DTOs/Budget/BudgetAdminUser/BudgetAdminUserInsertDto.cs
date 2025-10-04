using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.BudgetManagement;

namespace formneo.core.DTOs.Budget.BudgetAdminUser
{
    public class BudgetAdminUserInsertDto
    {

        public string UserName { get; set; }

        public string Mail { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsDoProxy { get; set; }

        public string ProxyUser { get; set; }

        public AdminLevel AdminLevel { get; set; }

    }
}
