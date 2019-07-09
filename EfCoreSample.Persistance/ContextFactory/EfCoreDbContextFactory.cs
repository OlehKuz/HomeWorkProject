using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace EfCoreSample.Persistance
{
    public class EfCoreDbContextFactory : IDesignTimeDbContextFactory<EfCoreSampleDbContext>
    {
        public EfCoreSampleDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional:true)
                .AddUserSecrets<EfCoreDbContextFactory>()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<EfCoreSampleDbContext>();
            optionsBuilder.UseMySql(configuration["ConnectionStrings:LocalConnection"]);

            return new EfCoreSampleDbContext(optionsBuilder.Options);
        }

    }
}
