using MapachesLectoresBackend.Core.Data.Db;
using MapachesLectoresBackend.Core.Data.Specification;
using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Specification;
using Microsoft.EntityFrameworkCore;

namespace MapachesLectoresBackend.Core.Data.Repository
{
    public class BaseMongoRepository<T>(
        //MongoDbDatabase mongoDbDatabase
        MongoDbContext mongoDbContext
    ) : IRepository<T> where T : class, IEntity
    {
        //private readonly IMongoCollection<T> _mongoCollection = mongoDbDatabase.MongoDatabase.GetCollection<T>(typeof(T).Name);
        private readonly DbSet<T> _mongoCollection = mongoDbContext.Set<T>();

        private IQueryable<T> ApplySpecification(ISpecification<T>? spec)
        {
            if (spec != null)
            {
                return SpecificationEvaluator<T>.GetQuery(_mongoCollection.AsQueryable(), spec);
            }

            return _mongoCollection
                .AsQueryable();
        }

        public Task<int> CountAsync(ISpecification<T> spec)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBySpecAsync(ISpecification<T> spec)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(System.Linq.Expressions.Expression<Func<IQueryable<T>, IQueryable<TResult>>> query, ISpecification<T>? spec = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TResult>> ExecuteQueryAsync<TResult>(System.Linq.Expressions.Expression<Func<IQueryable<T>, IQueryable<TResult>>> query, IPagintaion pagination, ISpecification<T>? spec = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAsync(IPagintaion paginationParameter, ISpecification<T>? spec = null)
        {
            var query = ApplySpecification(spec);
            return await query
                .Take(paginationParameter.Limit)
                .Skip(paginationParameter.Offset)
                .ToListAsync();
                
        }

        public Task<T?> GetByUuidAsync(string uuid)
        {
            throw new NotImplementedException();
        }

        public async Task<T?> GetFirstAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);
            var result = await query.FirstOrDefaultAsync();
            if (result == default(T))
                return null;

            return result;
        }

        public async Task<T> InsertAsync(T entity)
        {
            await _mongoCollection.AddAsync(entity);
            return entity;
        }

        public Task InsertRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            return mongoDbContext.SaveChangesAsync();
        }
    }
}
