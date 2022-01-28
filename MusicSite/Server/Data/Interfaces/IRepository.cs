using System.Linq.Expressions;

namespace MusicSite.Server.Data.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync(CancellationToken cancel);
        ValueTask<TEntity?> GetAsync(int id, CancellationToken cancel);
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancel);
        void Add(TEntity entity);
        void AddAll(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveAll(IEnumerable<TEntity> entities);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancel);

        Task<List<TEntity>> GetAllPagedAsync(int page, int recordsPerPage, CancellationToken cancel);
        Task<List<TEntity>> FindPagedAsync(Expression<Func<TEntity, bool>> predicate, int page, int recordsPerPage, CancellationToken cancel);
        
    }
}
