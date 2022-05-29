using BLL.Interfaces;
using DAL.Repositories.Interfaces;
using DAL.Repository;
using Ninject;

namespace BLL;

public static class ServiceLocator
{
    private readonly static IKernel _kernel = new StandardKernel();

    static ServiceLocator()
    {
        _kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        _kernel.Bind<IAssignmentBl>().To<AssignmentBL>();
        _kernel.Bind<IProjectBl>().To<ProjectBL>();
    }

    public static T GetService<T>() => _kernel.Get<T>();
}
