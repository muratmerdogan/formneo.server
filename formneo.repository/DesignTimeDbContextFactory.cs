using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace vesa.repository
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Design-time için configuration oluştur
            // API projesindeki appsettings.json dosyasını kullan
            var apiProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "formneo.api");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(apiProjectPath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            // DbContextOptions oluştur
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("PostgreSqlConnection");
            
            optionsBuilder.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name);
            });

            // Design-time için gerekli servisleri mock'la
            var httpContextAccessor = new MockHttpContextAccessor();
            var tenantContext = new MockTenantContext();

            return new AppDbContext(optionsBuilder.Options, httpContextAccessor, configuration, tenantContext);
        }
    }

    // Mock servisler - Design-time için
    public class MockHttpContextAccessor : Microsoft.AspNetCore.Http.IHttpContextAccessor
    {
        public Microsoft.AspNetCore.Http.HttpContext HttpContext { get; set; } = null!;
    }

    public class MockTenantContext : vesa.core.Services.ITenantContext
    {
        public Guid? CurrentTenantId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000001"); // Default tenant
        public Guid? CurrentCompanyId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000002"); // Default company
        public Guid? CurrentPlantId { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000003"); // Default plant
    }
}
