using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.repository.Seeds
{
    internal class EmployeeSeed : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

            //builder.HasData(
            //    new Employee { Id = Guid.NewGuid(), Client = "00", Company = "01", Plant = "01", PersId = "14", Name = "Murat", Surname = "Merdoğan", Email = "murat.merdogan@vesacons.com", City = "Eskişehir"  },
            //    new Employee { Id = Guid.NewGuid(), Client = "00", Company = "01", Plant = "01", PersId = "24", Name = "Muhammed", Surname = "Kadan", Email = "murat.merdogan@vesacons.com", City = "Eskişehir",  },
            //    new Employee { Id = Guid.NewGuid(), Client = "00", Company = "01", Plant = "01", PersId = "43", Name = "Bolat", Surname = "Çiftçi", Email = "murat.merdogan@vesacons.com", City = "Eskişehir" });


            //builder.HasData(
            //   new Departments { Guid = Guid.NewGuid().ToString(), Client = "00", Company = "01", Plant = "01", DepartmentId = "1",  DepartmentText = "Yönetim" });


        }
    }
}
