using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Utils;
using MapachesLectoresBackend.Reviews.Domain.Model;
using MapachesLectoresBackend.Reviews.Domain.Specification;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class GetBooksUseCase(
    IRepository<Book> bookRepository,
    IRepository<Review> reviewRepository
)
{
    public async Task<PaginationResult<BookWithReviewsAvarageDto>> InvokeAsync(
        IPagintaion pagintaion,
        string? search = null,
        ISet<string>? categories = null,
        BooksOrderEnum booksOrder = BooksOrderEnum.Default
    )
    {
        var spec = new BookSpecifications.IncludesAuthors()
            .And(new BookSpecifications.IncludesCategories())
            .And(new BookSpecifications.IncludesPublisher());


        if (search != null)
        {
            var searchSpec = new BookSpecifications.SearchByName(search)
                .Or(new BookSpecifications.SearchByAuthor(search));
            
            spec = spec.And(searchSpec);
        }

        if(categories != null)
            spec = spec.And(new BookSpecifications.GetByCategoriesNames(categories));

        spec = spec.And(new BookSpecifications.OrderBy(booksOrder));

        var paginationQuery = pagintaion.ToQueryPagination();

        var books = (await bookRepository.GetAsync(paginationQuery, spec)).ToList();
        
        var reviewSpec = new ReviewSpecifications.GetByBookIds(books.Select(b => b.ItemUuid).ToHashSet());
        var reviewsAvg = await reviewRepository.ExecuteQueryAsync(query =>
                query.GroupBy(review => review.Book)
                    .Select((group) =>
                        new BookWithReviewsAvarageDto(group.Key, group.Average(review => review.GeneralRating),
                            group.Count())
                    ), reviewSpec
        );

        var bookWithReviews = books.Select(book =>
        {
            var reviews = reviewsAvg.FirstOrDefault(r => r.Book.ItemUuid == book.ItemUuid);
            return new BookWithReviewsAvarageDto(book, reviews?.ReviewsAvarage ?? 0, reviews?.ReviewsCount ?? 0);
        });

        return bookWithReviews.ToPaginationResult(paginationQuery);
    }
}