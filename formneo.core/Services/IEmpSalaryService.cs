using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs;
using formneo.core.Models;

namespace formneo.core.Services
{
  
        public interface IEmpSalaryService : IService<EmpSalary>
        {
            Task<CustomResponseDto<List<EmployeeCustomDto>>> GetProductsWithCategory();


        }
    
}
