using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs;
using formneo.core.Models;

namespace formneo.core.Services
{

    public interface IEmployeeService : IService<Employee>
    {

        Task<CustomResponseDto<List<EmployeeDto>>> GetAllRelationTable();
        Task<CustomResponseDto<EmployeeDto>> SaveAll(EmployeeDto dto);
        Task<CustomResponseDto<EmployeeDto>> UpdateAll(EmployeeDto dto);
///*        Task<CustomResponseDto<EmployeeDto>> GetEmployeeWitPersId(string persId)*/;


        

    }
    
}
