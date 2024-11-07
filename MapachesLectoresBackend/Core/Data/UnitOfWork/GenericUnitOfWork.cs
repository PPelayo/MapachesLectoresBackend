using MapachesLectoresBackend.Core.Data.Db;
using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.UnitOfWork;

namespace MapachesLectoresBackend.Core.Data.UnitOfWork;

public class GenericUnitOfWork<T>(
    MapachesDbContext dbContext,
    IRepository<T> repository
) : BaseUnitOfWork(dbContext), IGenericUnitOfWork<T> where T : IEntity
{
    public IRepository<T> Repository { get; } = repository;
}