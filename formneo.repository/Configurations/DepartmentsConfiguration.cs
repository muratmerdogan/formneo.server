using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;
using System.Reflection.Emit;
using System.ComponentModel.DataAnnotations.Schema;

namespace formneo.repository.Configurations
{
    internal class DepartmentsConfiguration : IEntityTypeConfiguration<Departments>
    {
        public void Configure(EntityTypeBuilder<Departments> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DepartmentText).IsRequired().HasMaxLength(100);
       




            //    builder
            //.HasIndex(a => a.Guid)
            //.IsUnique();

            builder.ToTable("Departments");



        }
    }
}
