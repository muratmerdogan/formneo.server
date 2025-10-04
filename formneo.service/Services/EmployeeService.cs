using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;
using formneo.repository.Repositories;
using formneo.repository.UnitOfWorks;

namespace formneo.service.Services
{
    public class EmployeeService  : Service<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmpSalaryRepository _empSalaryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IGenericRepository<Employee> repository, IUnitOfWork unitOfWork, IMapper mapper, IEmployeeRepository employeeRepository,IEmpSalaryRepository empsalaryRepository) : base(repository, unitOfWork)
        {
             _mapper = mapper;
            _employeeRepository = employeeRepository;

          

               _unitOfWork = unitOfWork;
            _empSalaryRepository = empsalaryRepository;
        }

        public async Task<CustomResponseDto<List<EmployeeDto>>> GetAllRelationTable()
        {

            var list = _employeeRepository.GetAll(); ;

            var ss = _employeeRepository.GetAll().Include(e => e.empSalary).ToList();


            var dto = _mapper.Map<List<EmployeeDto>>(ss);

            return CustomResponseDto<List<EmployeeDto>>.Success(200, dto);
        }

        //public async Task<CustomResponseDto<EmployeeDto>> GetEmployeeWitPersId(string persId)
        //{
        //    var result = await _employeeRepository.GetEmployeeWitPersId(persId);

        //    var dto = _mapper.Map<EmployeeDto>(result);

        //    return CustomResponseDto<EmployeeDto>.Success(200, dto);
        //}

        public async Task<CustomResponseDto<EmployeeDto>> SaveAll(EmployeeDto emp)
        {
            var obj = _mapper.Map<Employee>(emp);
      
            EmpSalary salar = new EmpSalary();
            //salar.EmployeeId = obj.Id;
            //salar.StartDate = DateTime.Now;
            //salar.EndDate = DateTime.MaxValue;
            //salar.Salary = emp.Salary;

            await _employeeRepository.AddAsync(obj);
            await _empSalaryRepository.AddAsync(salar);


            await _unitOfWork.CommitAsync();

            var dto = _mapper.Map<EmployeeDto>(obj);
            return CustomResponseDto<EmployeeDto>.Success(200, dto);

        }

        public async Task<CustomResponseDto<EmployeeDto>> UpdateAll(EmployeeDto emp)
        {
            var obj = _mapper.Map<Employee>(emp);

            //EmpSalary salar = new EmpSalary();
            //salar.EmployeeId = obj.Id;
            //salar.StartDate = DateTime.Now;
            //salar.EndDate = DateTime.MaxValue;
            //salar.Salary = emp.Salary;
            

            //emp.ManagerPersId = "12";
            //_employeeRepository.Update(obj);
            //await _empSalaryRepository.AddAsync(salar);


            await _unitOfWork.CommitAsync();

            var dto = _mapper.Map<EmployeeDto>(obj);
            return CustomResponseDto<EmployeeDto>.Success(200, dto);

        }

        
    }
}
