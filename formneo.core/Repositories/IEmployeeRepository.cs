using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs;
using formneo.core.Models;

namespace formneo.core.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        //Task<Employee> GetEmployeeWitPersId(string persId);

        //Task<List<Employee>> GetAllRelationTable();
    }
}
