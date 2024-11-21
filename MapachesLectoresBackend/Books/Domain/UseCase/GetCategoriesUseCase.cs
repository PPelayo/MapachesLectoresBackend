using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Utils;

namespace MapachesLectoresBackend.Books.Domain.UseCase
{
    public class GetCategoriesUseCase(
        IRepository<Category> categoryRepository    
    )
    {

        public async Task<PaginationResult<Category>> InvokeAsync(IPagintaion pagination, string? search)
        {
            ISpecification<Category>? spec = null;

            if (search != null)
                spec = new CategorySpecifications.SearchByName(search);

            var paginationQuery = pagination.ToQueryPagination();

            var results = await categoryRepository.GetAsync(paginationQuery, spec);

            return results.ToPaginationResult(paginationQuery);
        }

    }
}
