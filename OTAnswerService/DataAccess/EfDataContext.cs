using System.Data.Entity;
using OTAnswerService.Entities;

namespace OTAnswerService.DataAccess
{
    public class EfDataContext : DbContext
    {

        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            EfConnectionFactory.Enable(true);
#if DEBUG
            Database.SetInitializer(new CreateDatabaseIfNotExists<EfDataContext>());
#endif
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<EfDataContext, EfMigrationConfiguration>());
        }
    }
}