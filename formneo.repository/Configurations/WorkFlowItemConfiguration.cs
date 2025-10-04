using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.repository.Configurations
{
    internal class WorkflowItemConfiguration : IEntityTypeConfiguration<WorkflowItem>
    {
        public void Configure(EntityTypeBuilder<WorkflowItem> builder)
        {





            builder.ToTable("WorkflowItem");

        }
    }
}
