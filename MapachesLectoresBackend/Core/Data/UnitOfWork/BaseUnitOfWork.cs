using MapachesLectoresBackend.Core.Data.Db;
using MapachesLectoresBackend.Core.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;

namespace MapachesLectoresBackend.Core.Data.UnitOfWork;

public class BaseUnitOfWork(
    MapachesDbContext dbContext    
) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    
    public void Dispose()
    {
        dbContext.Dispose();
        _transaction?.Dispose();
        _transaction = null;
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await dbContext.DisposeAsync();
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        GC.SuppressFinalize(this);
    }

    public async Task BeginTransaction()
    {
        _transaction ??= await dbContext.Database.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task Rollback()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task<int> Save()
    {
        return await dbContext.SaveChangesAsync();
    }
}