using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Utils;

namespace MapachesLectoresBackend.Books.Domain.UseCase
{
    public class GetAuthorsUseCase(
        IRepository<Author> authorRepository
    )
    {

        public async Task<PaginationResult<Author>> InvokeAsync(IPagintaion pagination, string? search = null)
        {
            ISpecification<Author>? spec = null;
            if(search != null)
                spec = new AuthorSpecifications.SearchByNameAndLastName(search);

            var paginationQuery = pagination.ToQueryPagination();
            var results = await authorRepository.GetAsync(paginationQuery, spec);

            return results.ToPaginationResult(paginationQuery);
        }

    }
}
