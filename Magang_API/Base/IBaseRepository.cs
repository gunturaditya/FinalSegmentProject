using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Magang_API.Base
{
    public interface IBaseRepository<TEntity, TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<TEntity?> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TKey key);
        Task<bool> IsExist(TKey key);
    }
}
