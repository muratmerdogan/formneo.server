using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Models
{
    public class Departments : BaseEntity
    {
        public string Code { get; set; }

        public string DepartmentText { get; set; }

        public List<UserApp>? employess { get; set; }

    }
}
