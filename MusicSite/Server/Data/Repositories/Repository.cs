using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data.Interfaces;

namespace MusicSite.Server.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly MusicSiteServerContext Context;
        private readonly DbSet<TEntity> _entities;

        protected Repository(MusicSiteServerContext context)
        {
            Context = context;
            _entities = context.Set<TEntity>();
        }

        public void AddAll(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancel,
            Expression<Func<TEntity, bool>>? additionalPredicate = null)
        {
            var query = _entities.Where(predicate);
            if (additionalPredicate is null)
            {
                return query.ToListAsync(cancel);
            }
            return _entities.Where(additionalPredicate).ToListAsync(cancel);
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancel)
        {
            return _entities.ToListAsync(cancel);
        }

        public ValueTask<TEntity?> GetAsync(int id, CancellationToken cancel)
        {
            return _entities.FindAsync(new object?[] { id }, cancellationToken: cancel);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveAll(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancel)
        {
            return _entities.Where(predicate).AnyAsync(cancellationToken: cancel);
        }

        public abstract Task<List<TEntity>> GetAllPagedAsync(int page, int recordsPerPage, CancellationToken cancel);

        public abstract Task<List<TEntity>> FindPagedAsync(Expression<Func<TEntity, bool>> predicate, int page,
            int recordsPerPage, CancellationToken cancel,
            Expression<Func<TEntity, bool>>? additionalPredicate = null);
    }
}
