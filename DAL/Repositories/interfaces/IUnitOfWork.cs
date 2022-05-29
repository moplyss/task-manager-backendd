using DAL.Model;
using DAL.Repository;

namespace DAL.Repositories.Interfaces;

public interface IUnitOfWork
{
    public IRepository<Project> ProjectRepository { get; protected set; }

    public IRepository<Assignment> AssignmentRepository { get; protected set; }

    public void Save();
}
