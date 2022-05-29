using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> Filter(Func<TEntity, bool> predicate) => _dbSet.Where(item => predicate(item));

    public IQueryable<TEntity> Read() => _dbSet;

    public void Create(TEntity item) => _dbSet.Add(item);

    public void Update(TEntity item) => _dbSet.Update(item);
    
    public void Delete(TEntity item) => _dbSet.Remove(item);

    public TEntity? Find(Func<TEntity, bool> predicate) => _dbSet.FirstOrDefault(predicate);
}