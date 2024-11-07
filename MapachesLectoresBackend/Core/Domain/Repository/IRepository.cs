using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Specification;

namespace MapachesLectoresBackend.Core.Domain.Repository;

public interface IRepository<T> where T : IEntity
{
    // public Task<IEnumerable<T>> GetAsync(ISpecification<T> spec, IPaginationParameter paginationParameter);
    
    public Task<int> CountAsync(ISpecification<T> spec);

    public Task<T?> GetFirstAsync(ISpecification<T> spec);
    
    public Task<T> InsertAsync(T entity);
    
    public Task InsertRangeAsync(IEnumerable<T> entities);

    public Task DeleteAsync(T entity);
    
    public Task DeleteBySpecAsync(ISpecification<T> spec);
    
    public Task<T?> GetByUuidAsync(string uuid);
    
    public Task<T> UpdateAsync(T entity);
    
    public Task UpdateRangeAsync(IEnumerable<T> entities); 
}