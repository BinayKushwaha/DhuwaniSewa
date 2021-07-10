using DhuwaniSewa.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Database.Repository
{
    public class RepositoryService<TEntity, TKey> : BaseRepository, IRepositoryService<TEntity, TKey> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        public RepositoryService(ApplicationContext applicationContext) : base(applicationContext)
        {
            _dbSet = DataContext.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} entiy must not be null.");
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async void AddRangeAsync(List<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentException($"{nameof(entities)} entities must not null.");
            await _dbSet.AddRangeAsync(entities);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(Delete)} entity must not be null");
            _dbSet.Remove(entity);
        }

        public void DeleteRange(List<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentException($"{nameof(DeleteRange)} entities must not null.");
            _dbSet.RemoveRange(entities);
        }

        public async Task<TEntity> GetAync(Expression<Func<TEntity, bool>> expression=null)
        {
            return expression==null? await _dbSet.FirstOrDefaultAsync(): await _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<List<TEntity>> GetAllAsync(bool asNoTRacking = false)
        {
            return asNoTRacking ? await _dbSet.AsNoTracking().ToListAsync() : await _dbSet.ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(TKey Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(entity)} entity must not be null.");
            _dbSet.Update(entity);
            return entity;
        }


    }
}
