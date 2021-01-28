using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clean.domain.core.Models;

namespace clean.domain.Interfaces.Repositories.Base
{
    public interface IBaseRepository <TEntity> where TEntity:BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> GetAllQuery { get; }
        IQueryable<TEntity> GetAllQueryTracking{ get; }
        void Update(TEntity entity);
        Task<List<TEntity>> ListAllAsync();
    }
}
