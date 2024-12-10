using System.Linq.Expressions;
using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Specification;

namespace MapachesLectoresBackend.Core.Domain.Repository;

public interface IRepository<T> where T : IEntity
{
    public Task<IEnumerable<T>> GetAsync(IPagintaion paginationParameter, ISpecification<T>? spec = null);
    
    public Task<int> CountAsync(ISpecification<T> spec);

    public Task<T?> GetFirstAsync(ISpecification<T> spec);
    
    public Task<T> InsertAsync(T entity);
    
    public Task InsertRangeAsync(IEnumerable<T> entities);

    public Task DeleteAsync(T entity);
    
    public Task DeleteBySpecAsync(ISpecification<T> spec);
    
    public Task<T?> GetByUuidAsync(string uuid);
    
    public Task<T> UpdateAsync(T entity);
    
    public Task UpdateRangeAsync(IEnumerable<T> entities);

    public Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(
        Expression<Func<IQueryable<T>, IQueryable<TResult>>> query, ISpecification<T>? spec = null);

    public Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(
        Expression<Func<IQueryable<T>, IQueryable<TResult>>> query, IPagintaion pagination, ISpecification<T>? spec = null);

    public Task SaveAsync();
}