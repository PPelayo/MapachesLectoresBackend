using MapachesLectoresBackend.Core.Data.Db;
using MapachesLectoresBackend.Core.Data.Specification;
using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Specification;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace MapachesLectoresBackend.Core.Data.Repository
{
    public class BaseMongoRepository<T>(MongoDbDatabase mongoDbDatabase) : IRepository<T> where T : class, IEntity
    {
        private readonly IMongoCollection<T> _mongoCollection = mongoDbDatabase.MongoDatabase.GetCollection<T>(nameof(T));


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

        public Task<IEnumerable<T>> GetAsync(IPagintaion paginationParameter, ISpecification<T>? spec = null)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByUuidAsync(string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetFirstAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);
            return query.FirstOrDefaultAsync();
        }

        public async Task<T> InsertAsync(T entity)
        {
            await _mongoCollection.InsertOneAsync(entity);
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
    }
}
