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
    internal class DepartmentsSeed : IEntityTypeConfiguration<Departments>
    {
        public void Configure(EntityTypeBuilder<Departments> builder)
        {

            //builder.HasData(
            //    new Departments { Code = "0012", Client = "00", Company = "01", Plant = "01", Id = Guid.NewGuid(), DepartmentText = "Yazılım" },
            //    new Departments { Code = "0015", Client = "00", Company = "01", Plant = "01", Id = Guid.NewGuid(), DepartmentText = "Danışmanlık" },
            //    new Departments { Code = "0016", Client = "00", Company = "01", Plant = "01", Id = Guid.NewGuid(), DepartmentText = "SF" });


            //builder.HasData(
            //   new Departments { Guid = Guid.NewGuid().ToString(), Client = "00", Company = "01", Plant = "01", DepartmentId = "1",  DepartmentText = "Yönetim" });


        }
    }
}
