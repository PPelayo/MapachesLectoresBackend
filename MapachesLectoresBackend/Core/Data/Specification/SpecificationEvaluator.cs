using System.Linq.Expressions;
using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Specification;
using Microsoft.EntityFrameworkCore;

namespace MapachesLectoresBackend.Core.Data.Specification;

public class SpecificationEvaluator<TEntity> where TEntity : class, IEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
    {
        var query = inputQuery.AsQueryable();

        if(specification.Criteria != null)
            query = query.Where(specification.Criteria);

        if (specification.Includes is { Count: > 0 })
        {
            query = specification.Includes
                .Aggregate(query,
                    (current, include) => current.Include(include));
        }

        if (specification.ThenIncludes is { Count: > 0 })
        {
            query  =specification.ThenIncludes
                .Aggregate(query,
                    (current, include) => include(current));
        }

        //var q = specification.Includes
        //    .Aggregate(query,
        //    (current, include) => current.Include(include));

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        } else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }
        else
        {
            //Por defecto para evitar problemas ordenamos por row create en caso de que no nos digan lo contrario
            query = query.OrderBy(entity => entity.CreatedAt);
        }

        if (specification.IsDistinct)
        {
            query = query.Distinct();
        }

        if(specification.DistinctBy != null)
        {
            query = query.EfDistinctBy(specification.DistinctBy);
        }

        if (specification.IsSpliting)
        {
            query = query.AsSplitQuery();
        }
        
        return query;

    }
    
}

public static class LinqExpressions
{
    public static IQueryable<TSource> EfDistinctBy<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, object>> property)
    {
        return source.GroupBy(property).Select(x => x.First());
    }
}