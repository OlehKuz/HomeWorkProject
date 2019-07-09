using EfCoreSample.Infrastructure.Abstraction;
using EfCoreSample.Infrastructure.Repository;
using EfCoreSample.Persistance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace EfCoreSample.Infrastructure.Services
{
    public class UnitOfWork:IUnitOfWork { 
        public async Task CompleteAsync()
        {
            using (var dbContext = new EfCoreDbContextFactory().CreateDbContext(new string[] { }))
                await dbContext.SaveChangesAsync();
        }
    }
}
