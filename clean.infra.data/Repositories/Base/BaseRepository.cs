using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clean.domain.core.Models;
using clean.domain.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace clean.infra.data.Repositories.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private DbSet<TEntity> DbSet { get; }

        public BaseRepository(CleanContext context)
        {
            DbSet = context.Set<TEntity>();

        }

        public IQueryable<TEntity> GetAllQuery => DbSet.AsQueryable().AsNoTracking();

        public IQueryable<TEntity> GetAllQueryTracking => DbSet.AsQueryable();

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await DbSet.AddAsync(entity);
            return result.Entity;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public async Task<List<TEntity>> ListAllAsync()
        {
            return await DbSet.ToListAsync();
        }
    }
}
