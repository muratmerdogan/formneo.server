using System.Data.SqlClient;

namespace formneo.api.Helper
{
    public class DbNameHelper
    {
        private readonly IConfiguration _configuration;

        public DbNameHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetDatabaseName()
        {
            string connectionString = _configuration.GetConnectionString("SqlServerConnection");

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string not found!");

            var builder = new SqlConnectionStringBuilder(connectionString);
            string dbName = builder.InitialCatalog;

            // Ortama göre açıklama ekle
            if (dbName.Equals("formneo_erp", StringComparison.OrdinalIgnoreCase))
                return "(Live system)";
            else if (dbName.Equals("formneo_erp_test", StringComparison.OrdinalIgnoreCase))
                return "(Test system)";
            else
                return dbName;
        }

    }
}
