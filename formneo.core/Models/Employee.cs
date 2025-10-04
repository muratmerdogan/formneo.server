using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Models
{
    public class Employee : BaseEntity
    {


        public string PersId { get; set; }

        public string? CitizenshipNumber { get; set; }

        public string Name { get; set; }


        [ForeignKey("Departments")]
        public Guid? DepartmentsId { get; set; }

        public virtual Departments Departments { get; set; }

        public string? ManagerPersId { get; set; }

        public string? SecondManagerPersId { get; set; }

        public string? Photo { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string City { get; set; }

        public DateTime BirthDate { get; set; }
        public DateTime OfficialBirthDate { get; set; }
 
        public DateTime Workstartdate { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? WorkendDate { get; set; }

        public string? Address { get; set; }

        public string? EmergencyContactPerson { get; set; }

        public string? EmergencyContactPhone { get; set; }

        public string? BankName { get; set; }

        public string? BankIBAN { get; set; }

        public string? BloodGroup { get; set; }

        public string? RelatedPerson { get; set; }

        public string? illness { get; set; }

        public List<EmpSalary>? empSalary { get; set; }

        public string? UserId { get; set; }

        public string? DeptCode{ get; set; }


    }

}
