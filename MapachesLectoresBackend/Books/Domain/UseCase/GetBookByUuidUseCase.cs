using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Model.Errors;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.UseCase;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class GetBookByUuidUseCase(
    IRepository<Book> bookRepository
)
{
    public async Task<DataResult<BookWithReviewsAvarageDto>> InvokeAsync(Guid bookId)
    {
        var includesSpec = new BookSpecifications.IncludesAuthors()
            .And(new BookSpecifications.IncludesCategories())
            .And(new BookSpecifications.IncludesPublisher());

        var book = (await bookRepository.ExecuteQueryAsync(query =>
                query
                    .Where(entity => entity.ItemUuid == bookId.ToString())
                    .Select(entity =>
                        new BookWithReviewsAvarageDto(
                            entity,
                            entity.Reviews.Any() 
                                ? entity.Reviews.Average(review => (double?)review.GeneralRating) ?? 0 
                                : 0,
                            entity.Reviews.Count
                        )
                    ),
            includesSpec
        )).FirstOrDefault();
        
        return book != null
            ? DataResult<BookWithReviewsAvarageDto>.CreateSuccess(book)
            : DataResult<BookWithReviewsAvarageDto>.CreateFailure(new NotFoundError());
    }
}