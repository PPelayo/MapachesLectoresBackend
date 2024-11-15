using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Utils;
using MapachesLectoresBackend.Reviews.Domain.Model;
using MapachesLectoresBackend.Reviews.Domain.Specification;

namespace MapachesLectoresBackend.Reviews.Domain.UseCase;

public class GetReviewsFromBookUseCase(IRepository<Review> reviewRepository)
{
    public async Task<DataResult<PaginationResult<Review>>> InvokeAsync(Guid bookId, IPagintaion pagintaion)
    {
        var spec = new ReviewSpecifications.GetByBookId(bookId)
            .And(new ReviewSpecifications.IncludesUser());

        var queryPagination = pagintaion.ToQueryPagination();
        var result = await reviewRepository.GetAsync(queryPagination, spec);

        return DataResult<PaginationResult<Review>>.CreateSuccess(result.ToPaginationResult(queryPagination));
    }
}