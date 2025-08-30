using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.repository.Configurations
{
    internal class WorkFlowConfiguration : IEntityTypeConfiguration<WorkflowHead>
    {
        public void Configure(EntityTypeBuilder<WorkflowHead> builder)
        {





  

            builder.ToTable("WorkflowHead");

        }
    }
}
