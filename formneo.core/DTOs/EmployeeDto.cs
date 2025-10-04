using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.DTOs
{
    public class EmployeeDto : BaseListDto
    {

        public Guid id { get; set; }

        public string PersId { get; set; }
        public string citizenshipNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string? DepartmentId { get; set; }

        public string? ManagerPersId { get; set; }
        public string? Photo { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime OfficialBirthDate { get; set; }
    
        public DateTime Workstartdate { get; set; }
        public string? RelatedPerson { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public string? illness { get; set; }

        public DateTime? WorkEndDate { get; set; }

        public string? EmergencyContactPerson { get; set; }

        public string? EmergencyContactPhone { get; set; }

        public string? BankName { get; set; }

        public string? BankIBAN { get; set; }

        public string? BloodGroup { get; set; }

        public List<EmpSalaryDto>? empsalary { get; set; }



    }
}
