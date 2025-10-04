using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.repository.Configurations
{
    internal class EmpSalaryConfiguration : IEntityTypeConfiguration<EmpSalary>
    {
        public void Configure(EntityTypeBuilder<EmpSalary> builder)
        {

            builder.HasKey(e => new { e.EmployeeId,e.StartDate,e.EndDate });



            builder.ToTable("EmpSalary");

        }
    }
}
