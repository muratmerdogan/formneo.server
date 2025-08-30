using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.Models.BudgetManagement
{
    public class BudgetAdminUser : BaseEntity
    {
        public string UserName { get; set; }

        public string Mail { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsDoProxy { get; set; }

        public string ProxyUser { get; set; }

        public AdminLevel AdminLevel { get; set; }

    }

    public enum AdminLevel
    {
        [Description("Admin")]
        Admin = 1,
        [Description("PowerUser")]
        PowerUser = 2
    }
}
