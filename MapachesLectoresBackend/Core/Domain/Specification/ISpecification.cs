using System.Linq.Expressions;
using MapachesLectoresBackend.Core.Domain.Model;
using Microsoft.EntityFrameworkCore.Query;

namespace MapachesLectoresBackend.Core.Domain.Specification;

public interface ISpecification<T> where T: IEntity
{
    bool IsSatisfiedBy(T entity);

    Expression<Func<T, bool>>? Criteria { get; }
    List<Expression<Func<T, object>>>? Includes { get; }
        
    public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>? ThenIncludes { get; } 
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }

    bool IsDistinct { get; }
        
    Expression<Func<T, object>>? DistinctBy { get; }
    
    bool IsSpliting { get; }
        

    ISpecification<T> And(ISpecification<T> spec);
    ISpecification<T> Or(ISpecification<T> spec);
}