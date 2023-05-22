using Magang_API.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Magang_API.Base
{
    public class BaseRepository<TEntity, TKey, TContext> : IBaseRepository<TEntity, TKey>
    where TEntity : class
    where TContext : MyContexts
    {
        protected TContext _context;

        public BaseRepository(TContext context)
        {
            _context = context;
        }
       virtual public async Task<int> DeleteAsync(TKey key)
        {
            var entity = await GetByIdAsync(key);
            if (entity is null)
            {
                return 0;
            }

            _context.Set<TEntity>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

       virtual public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

       virtual public async Task<TEntity?> InsertAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

       virtual public async Task<bool> IsExist(TKey key)
        {
            bool result = await _context.Set<TEntity>().FindAsync(key) is not null;
            _context.ChangeTracker.Clear();
            return result;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
