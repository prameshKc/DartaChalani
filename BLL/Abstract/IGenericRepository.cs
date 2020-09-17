using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Abstract {
    public interface IGenericRepository<TEntity> where TEntity : class {
        Task CreateAsync (TEntity entity);
        Task CreateRangeAsync (IEnumerable<TEntity> tentities);
        Task DeleteAsync (TEntity entity);
        Task DeleteRangeAsync (IEnumerable<TEntity> tentities);
        Task EditAsync (TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync ();
        Task<TEntity> GetById (object id);
        Task<IEnumerable<TEntity>> FilterAsync (Func<TEntity, bool> predicate);
    }
}