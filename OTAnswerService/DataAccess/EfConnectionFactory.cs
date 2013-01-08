using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace OTAnswerService.DataAccess
{
    /// <remarks>
    /// https://github.com/csainty/AppHarbify.Tools
    /// </remarks>
    public class EfConnectionFactory : IDbConnectionFactory
    {
        private const string ConnectionStringAppSetting = "OTAnswerService";

        private readonly string connectionString;

        public static void Enable(bool enableMars = false)
        {
            var connectionString = ConfigurationManager.AppSettings[ConnectionStringAppSetting];
            if (!string.IsNullOrEmpty(connectionString))
            {
                Database.DefaultConnectionFactory = new EfConnectionFactory(connectionString, enableMars);
            }
        }

        public EfConnectionFactory(string connectionString, bool enableMars)
        {
            this.connectionString = connectionString;

            if (enableMars && !this.connectionString.Contains("MultipleActiveResultSets=True"))
            {
                this.connectionString += (this.connectionString.EndsWith(";") ? "" : ";") +
                    "MultipleActiveResultSets=True;";
            }
        }

        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            var connection = DbProviderFactories
                .GetFactory("System.Data.SqlClient")
                .CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }
    }
}
