using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.repository.Seeds
{
    internal class MainClientSeed : IEntityTypeConfiguration<MainClient>
    {
        public void Configure(EntityTypeBuilder<MainClient> builder)
        {


            string currentClient = "77df6fbd-4160-4cea-8f24-96564b54e5ac";

            string currentCompany = "1bf2fc2e-0e25-46a8-aa96-8f1480331b5b";

            string currentPlant = "0779dd43-6047-400d-968d-e6f1b0c3b286";

            builder.HasData(
                new MainClient { Id = new Guid(currentClient), Name = "formneo", Email = "info@formneo.com", IsActive = true, PhoneNumber = "5069112452" });


            //builder.HasData(
            //   new Departments { Guid = Guid.NewGuid().ToString(), Client = "00", Company = "01", Plant = "01", DepartmentId = "1",  DepartmentText = "Yönetim" });


        }
    }
}
