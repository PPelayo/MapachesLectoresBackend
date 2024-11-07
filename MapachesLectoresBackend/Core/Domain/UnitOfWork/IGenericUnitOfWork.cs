using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Repository;

namespace MapachesLectoresBackend.Core.Domain.UnitOfWork;

public interface IGenericUnitOfWork<T> : IUnitOfWork where T : IEntity
{
    public IRepository<T> Repository { get; }
}