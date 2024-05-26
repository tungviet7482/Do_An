using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Repository.Base
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : class
    {
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> DeleteRangeAsync(List<TEntity> entities);
    }
}
