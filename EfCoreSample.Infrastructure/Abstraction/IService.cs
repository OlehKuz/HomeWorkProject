using EfCoreSample.Doman.DTO;
using EfCoreSample.Doman.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EfCoreSample.Doman.Abstraction;
using EfCoreSample.Infrastructure.Services;
using EfCoreSample.Infrastructure.Services.Communication;
using EfCoreSample.Doman.Communication;

namespace EfCoreSample.Infrastructure.Abstraction
{
    public interface IService<TSource, TKey> where TSource : class
    { 
        Task<TSource> FindAsync(TKey key);

        Task<List<TSource>> GetAsync (Expression<Func<TSource, bool>> expression);
        Task<Response<TSource>> InsertAsync(TSource entity);

        Task<Response<TSource>>  UpdateRange(IEnumerable<TSource> entities);
           

        Task<Response<TSource>> Update(TSource entity);

        Task<Response<TSource>> DeleteAsync(TKey key);

        Task<Response<TSource>> DeleteAsync(TSource entity);
        Task<bool> AnyAsync(TKey key);
    }
}
