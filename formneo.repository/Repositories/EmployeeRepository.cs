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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        //public async Task<List<Employee>> GetAllRelationTable()
        //{
        //    var employee = await _context.Employees.ToListAsync();

        //    return employee;
        //}

        //public async Task<Employee> GetEmployeeWitPersId(string persId)
        //{
        //    var employee = await _context.Employees.Include(e=>e.empSalary).FirstOrDefaultAsync(x => x.PersId == persId);
        //    return employee;

        //}

    }
}
