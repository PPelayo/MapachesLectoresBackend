using System.Linq.Expressions;
using MapachesLectoresBackend.Core.Data.Db;
using MapachesLectoresBackend.Core.Data.Specification;
using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace MapachesLectoresBackend.Core.Data.Repository;

public class BaseRepository<T>(MapachesDbContext dbContext) : IRepository<T> where T : class, IEntity
{
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();
    
    private IQueryable<T> ApplySpecification(ISpecification<T>? spec)
    {
        if (spec != null)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec);
        }

        return _dbSet
            .AsQueryable();
    }
    
    
    public async Task<IEnumerable<T>> GetAsync(IPagintaion pagintaion, ISpecification<T>? spec = null)
    {
        return await ApplySpecification(spec)
            .Skip(pagintaion.Offset)
            .Take(pagintaion.Limit)
            .ToListAsync();
    }
    
    public Task<int> CountAsync(ISpecification<T> spec)
    {
        return ApplySpecification(spec)
            .CountAsync();
    }

    public Task<T?> GetFirstAsync(ISpecification<T> spec)
    {
        return ApplySpecification(spec)
            .FirstOrDefaultAsync();
    }

    public async Task<T> InsertAsync(T entity)
    {
        var currentTime = DateTimeUtils.GetDateTimeUtcWithMiliseconds();
        entity.CreatedAt = currentTime;
        entity.UpdatedAt = currentTime;
        return (await _dbSet.AddAsync(entity)).Entity;
    }

    public async Task InsertRangeAsync(IEnumerable<T> entities)
    {
        var currentTime = DateTimeUtils.GetDateTimeUtcWithMiliseconds();
        var entitiesList = entities as T[] ?? entities.ToArray();
        foreach (var entity in entitiesList)
        {
            entity.CreatedAt = currentTime;
            entity.UpdatedAt = currentTime;
        }

        await _dbSet.AddRangeAsync(entitiesList);
    }

    public Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteBySpecAsync(ISpecification<T> spec)
    {
        return ApplySpecification(spec)
            .ExecuteDeleteAsync();
    }

    public Task<T?> GetByUuidAsync(string uuid)
    {
        return _dbSet.FirstOrDefaultAsync(x => x.ItemUuid == uuid);
    }

    public Task<T> UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTimeUtils.GetDateTimeUtcWithMiliseconds();
        return Task.FromResult(_dbSet.Update(entity).Entity);
    }

    public Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        var entitiesList = entities as T[] ?? entities.ToArray();
        var currentTime = DateTimeUtils.GetDateTimeUtcWithMiliseconds();
        foreach (var entity in entitiesList)
        {
            entity.UpdatedAt = currentTime;
        }

        _dbSet.UpdateRange(entitiesList);
        return Task.CompletedTask;
    }
    
    public async Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(
        Expression<Func<IQueryable<T>, IQueryable<TResult>>> query, ISpecification<T>? spec = null)
    {
        var querySpec = ApplySpecification(spec);
        var resultQuery = query.Compile().Invoke(querySpec);
        return await resultQuery.ToListAsync();
    }

    public async Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(Expression<Func<IQueryable<T>, IQueryable<TResult>>> query, IPagintaion pagination, ISpecification<T>? spec = null)
    {
        var querySpec = ApplySpecification(spec)
            .Skip(pagination.Offset)
            .Take(pagination.Limit);
        var resultQuery = query.Compile().Invoke(querySpec);
        return await resultQuery.ToListAsync();
    }
}