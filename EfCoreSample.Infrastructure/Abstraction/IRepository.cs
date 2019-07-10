
using EfCoreSample.Doman.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EfCoreSample.Infrastructure.Abstraction
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        Task<TEntity> FindAsync(TKey key);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> InsertAsync(TEntity item);
        Task<bool> IsExistAsync(TKey key);
        void UpdateRange(IEnumerable<TEntity> items);
        TEntity Update(TEntity item);
        bool Remove(TEntity item);
        bool Remove(TKey key);
        List<object> FindRelated(TKey key);
    }
}
