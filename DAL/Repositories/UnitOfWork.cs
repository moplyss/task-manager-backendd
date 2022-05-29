using DAL.Model;
using DAL.Repositories.Interfaces;

namespace DAL.Repository;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly ApplicationDbContext _context = new();
    private bool _disposed = false;

    public IRepository<Project> ProjectRepository { get; set; } 

    public IRepository<Assignment> AssignmentRepository { get; set; }

    public UnitOfWork()
    {
        ProjectRepository = new Repository<Project>(_context);
        AssignmentRepository = new Repository<Assignment>(_context);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _context.Dispose();

        _disposed = true;
    }
}