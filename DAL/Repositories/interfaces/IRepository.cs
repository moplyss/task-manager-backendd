namespace DAL.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    public IQueryable<TEntity> Filter(Func<TEntity, bool> predicate);
    
    public IQueryable<TEntity> Read();

    public void Create(TEntity item);

    public void Update(TEntity item);

    public void Delete(TEntity item);

    public TEntity? Find(Func<TEntity, bool> predicate);
}
