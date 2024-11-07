using System.Linq.Expressions;
using MapachesLectoresBackend.Core.Domain.Model;
using Microsoft.EntityFrameworkCore.Query;

namespace MapachesLectoresBackend.Core.Domain.Specification;

public abstract class BaseSpecification<T> : ISpecification<T> where T : IEntity
{


    public Expression<Func<T, bool>>? Criteria { get; private set; }

    public List<Expression<Func<T, object>>>? Includes { get; protected set; }

    public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>? ThenIncludes { get; protected set; }
        
    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
        
    public bool IsDistinct { get; private set; }

    public Expression<Func<T, object>>? DistinctBy { get; private set; }

    public bool IsSpliting { get; private set; } = false;


    protected BaseSpecification()
    {
    }

    protected BaseSpecification(Expression<Func<T, bool>> predicate)
    {
        Criteria = predicate;
    }

    protected void SetCriteria(Expression<Func<T, bool>> predicate)
    {
        Criteria = predicate;
    } 

    public bool IsSatisfiedBy(T entity)
    {
        var predicate = Criteria?.Compile();
        return predicate?.Invoke(entity) ?? false;
    }

    protected void AddThenIncludes(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
    {
        if (ThenIncludes == null)
        {
            ThenIncludes = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> { includeExpression };
            return;
        }
        ThenIncludes.Add(includeExpression);
    }
        
    protected void AddInclude(Expression<Func<T, object>> predicate)
    {
        if (Includes == null)
        {
            Includes = new List<Expression<Func<T, object>>> { predicate };
            return;
        }
        Includes.Add(predicate);
    }

    protected void ApplyOrderBy(Expression<Func<T, object>> predicate)
    {
        OrderBy = predicate;
    }
        
    protected void ApplyOrderByDescending(Expression<Func<T, object>> predicate)
    {
        OrderByDescending = predicate;
    }
        
    protected void Distincted()
    {
        IsDistinct = true;
    }
        
    protected void DistinctedBy(Expression<Func<T, object>> predicate)
    {
        DistinctBy = predicate;
    }

    protected void ActiveSpliting()
    {
        IsSpliting = true;
    }
    
    public ISpecification<T> And(ISpecification<T> spec)
    {
        return new AndSpecification<T>(this, spec);
    }

    public ISpecification<T> Or(ISpecification<T> spec)
    {
        return new OrSpecification<T>(this, spec);
    }
}