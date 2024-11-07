using MapachesLectoresBackend.Core.Data.Db;
using MapachesLectoresBackend.Core.Data.Specification;
using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Specification;
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
}