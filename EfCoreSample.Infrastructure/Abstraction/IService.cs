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
using EfCoreSample.Doman;

namespace EfCoreSample.Infrastructure.Abstraction
{
    public interface IService<TSource, TKey> where TSource : class
    { 
        Task<TSource> FindAsync(TKey key);
        // Doesnt allow me to use "params string[]" instead of filtering params
        List<TSource> Get(string sort,
            int? pageNumber, int? pageSize,  string status, string title, string startTime, string endTime);
        Task<Response<TSource>> InsertAsync(TSource entity);

        Task<Response<TSource>>  UpdateRange(IEnumerable<TSource> entities);
           

        Task<Response<TSource>> Update(TSource entity);

        Task<Response<TSource>> DeleteAsync(TKey key);

        Task<Response<TSource>> DeleteAsync(TSource entity);
        List<Typ> GetRelated<Typ>(TKey key);
        Task<bool> AnyAsync(TKey key);
    }
}
