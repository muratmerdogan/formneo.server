using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
    public class EmpSalaryRepository : GenericRepository<EmpSalary>, IEmpSalaryRepository
    {
        public EmpSalaryRepository(AppDbContext context) : base(context)
        {
        }

        //public async Task<List<Employee>> GetProductsWitCategory()
        //{

        //    return await _context.Employees.Include(x => x.Name).ToListAsync();
        //}
    }
}
