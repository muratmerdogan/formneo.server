using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;
using vesa.core.Repositories;

namespace vesa.repository.Repositories
{
    public class DepartmentsRepository : GenericRepository<Departments>, IDepartmentsRepository
    {
        public DepartmentsRepository(AppDbContext context) : base(context)
        {
        }

    }
}
