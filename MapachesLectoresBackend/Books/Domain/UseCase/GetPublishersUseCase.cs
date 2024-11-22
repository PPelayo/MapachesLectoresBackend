using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Utils;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class GetPublishersUseCase(
    IRepository<Publisher> publisherRepository
)
{
    public async Task<PaginationResult<Publisher>> InvokeAsync(IPagintaion pagintaion, string? search)
    {
        ISpecification<Publisher>? spec = null;
        if(search != null)
            spec = new PublisherSpecifications.SearchByName(search);
        var queryPagination = pagintaion.ToQueryPagination();
        var results = await publisherRepository.GetAsync(queryPagination, spec);

        return results.ToPaginationResult(queryPagination);
    } 
}