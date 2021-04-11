using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DoIt.Infrastructure.Data
{
    public class DoItDbContextFactory:IDesignTimeDbContextFactory<DoItDbContext>
    {
        public DoItDbContext CreateDbContext(string[] args)
        {
            const string connectionString = "Host=localhost;Database=DoItDb;Uid=postgres;Pwd=Parola.1";
            var dbContextBuilder = new DbContextOptionsBuilder<DoItDbContext>();
            dbContextBuilder
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();

            return new DoItDbContext(dbContextBuilder.Options);
        }
    }
}