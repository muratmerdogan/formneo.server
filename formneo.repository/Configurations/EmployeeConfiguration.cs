using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;
using System.Reflection.Emit;
using System.ComponentModel.DataAnnotations.Schema;

namespace vesa.repository.Configurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {


            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

       

    

            //    builder
            //.HasIndex(a => a.Guid)
            //.IsUnique();

            builder.ToTable("Employee");



        }
    }
}
