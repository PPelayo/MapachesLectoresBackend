using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Utils;
using MapachesLectoresBackend.Requests.Domain.Model;

namespace MapachesLectoresBackend.Requests.Domain.UseCase
{
    public class GetRequestsCreateBookUseCase(
        IRepository<RequestCreateBook> repository,
        IRepository<Author> authorRepository,
        IRepository<Category> categoryRepository
        
    )
    {

        public async Task<PaginationResult<RequestCreateBook>> InvokeAsync(IPagintaion pagintaion)
        {
            var paginationQuery = pagintaion.ToQueryPagination();
            var result = (await repository.GetAsync(paginationQuery)).ToList();

            
            foreach (var request in result)
            {
                var apiPagination = new UserPagination();

                var authorsSpec = new GetByUuidsSpecification<Author>(request.AuthorsIds.ToHashSet());
                var authors = await authorRepository.GetAsync(apiPagination, authorsSpec);

                var categoriesSpec = new GetByUuidsSpecification<Category>(request.CategoriesIds.ToHashSet());
                var categories = await categoryRepository.GetAsync(apiPagination, categoriesSpec);

                request.Authors = authors.ToList();
                request.Categories = categories.ToList();
            }


            return result.ToPaginationResult(paginationQuery);
        }

    }
}
