using System.Data.Entity.Migrations;

namespace OTAnswerService.DataAccess
{
    public class EfMigrationConfiguration : DbMigrationsConfiguration<EfDataContext>
    {
        public EfMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
#if DEBUG
            AutomaticMigrationDataLossAllowed = true;
#endif
        }
    }
}