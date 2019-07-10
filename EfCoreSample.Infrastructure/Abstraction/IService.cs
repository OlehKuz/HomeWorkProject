
using System.Collections.Generic;
using System.Threading.Tasks;
using EfCoreSample.Doman.Communication;

namespace EfCoreSample.Infrastructure.Abstraction
{
    public interface IService<TSource, TKey> where TSource : class
    { 
        Task<TSource> FindAsync(TKey key);
        IEnumerable<TSource> Get(string filter1, string filter2, string filter3, string filter4);
        Task<Response<TSource>> InsertAsync(TSource entity);
        Task<Response<TSource>> Update(TSource entity);
        Task<Response<TSource>> DeleteAsync(TKey key);
        Task<Response<TSource>> DeleteAsync(TSource entity);
        List<Typ> GetRelated<Typ>(TKey key);
        Task<bool> AnyAsync(TKey key);
    }
}
