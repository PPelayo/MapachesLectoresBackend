namespace MapachesLectoresBackend.Core.Domain.UnitOfWork;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    Task BeginTransaction();
    Task Commit();
    Task Rollback();
    Task<int> Save();
}