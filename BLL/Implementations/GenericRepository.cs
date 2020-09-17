using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using DAL;

namespace BLL.Implementations {
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class {
        private readonly DartaDbContext context;

        
        public GenericRepository (DartaDbContext context) {
            this.context = context;

        }
        public async Task CreateAsync (TEntity entity) {

            await context.Set<TEntity> ().AddAsync (entity);
        }

        public async Task CreateRangeAsync (IEnumerable<TEntity> tentities) {

            await context.Set<TEntity> ().AddRangeAsync (tentities);
        }

        public async Task DeleteAsync (TEntity entity) {

            await Task.Run (() => context.Set<TEntity> ().Remove (entity));
        }

        public async Task DeleteRangeAsync (IEnumerable<TEntity> tentities) {

            await Task.Run (() => context.Set<TEntity> ().RemoveRange (tentities));
        }

        public async Task EditAsync (TEntity entity) {

            await Task.Run (() => context.Set<TEntity> ().Update (entity));

        }

        public async Task<IEnumerable<TEntity>> FilterAsync (Func<TEntity, bool> predicate) {

            var query = context.Set<TEntity> ().Where (predicate).AsQueryable ();
            return await Task.Run (() => query.ToList ());
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync () {

            return await Task.Run (() => context.Set<TEntity> ().ToList ());
        }

        public async Task<TEntity> GetById (object id) {
            return await context.Set<TEntity>().FindAsync(id);
        }
    }
}