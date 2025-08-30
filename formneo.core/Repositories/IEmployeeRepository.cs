using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs;
using vesa.core.Models;

namespace vesa.core.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        //Task<Employee> GetEmployeeWitPersId(string persId);

        //Task<List<Employee>> GetAllRelationTable();
    }
}
