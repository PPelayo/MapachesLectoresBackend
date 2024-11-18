using MapachesLectoresBackend.Auth.Presentation.Middleware;
using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Books.Domain.UseCase;
using MapachesLectoresBackend.Books.Presentation.Mapper;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.Services;
using MapachesLectoresBackend.Core.Domain.UseCase;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using MapachesLectoresBackend.Reviews.Domain.UseCase;
using MapachesLectoresBackend.Reviews.Presentation.Dto;
using MapachesLectoresBackend.Reviews.Presentation.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Books.Presentation.Controller;

[ApiController]
[Route("[controller]")]
public class BooksController(
    IHttpContextService contextService,
    GetBooksUseCase getBooksUseCase,
    GetItemByUuidUseCase<Book> getItemByUuidUseCase,
    GetReviewsFromBookUseCase getReviewsFromBookUseCase,
    CreateReviewUseCase createReviewUseCase
) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> GetBooks(
        [FromQuery] UserPagination pagination,
        [FromQuery] string? search  
    )
    {
        var books = await getBooksUseCase.InvokeAsync(pagination, search);
        return Ok(
            BaseResponse.CreateSuccess(StatusCodes.Status200OK ,books.Map(book => book.ToResponseDto()))
        );
    }

    [HttpGet("{bookId}")]
    public async Task<IActionResult> GetBook(
        [FromRoute] Guid bookId    
    )
    {
        var includesSpec = new BookSpecifications.IncludesAuthors()
            .And(new BookSpecifications.IncludesCategories())
            .And(new BookSpecifications.IncludesPublisher());
        
        var result = await getItemByUuidUseCase.InvokeAsync(bookId, includesSpec);

        return result.ActionResultHanlder(
            book =>
            {
                var categories = book.BooksCategories.Select(bc => bc.Category);
                var authors = book.BooksAuthors.Select(ba => ba.Author);
                return Ok(BaseResponse.CreateSuccess(StatusCodes.Status200OK, book.ToResponseDto(categories, authors)));
            },
            error => error.ActionResult
        );
    }

    [HttpGet("{bookId}/reviews")]
    public async Task<IActionResult> GetReviews(
        [FromRoute] Guid bookId,
        [FromQuery] UserPagination pagination
    )
    {
        var result = await getReviewsFromBookUseCase.InvokeAsync(bookId, pagination);
        return result.ActionResultHanlder(
            reviews => Ok(BaseResponse.CreateSuccess(StatusCodes.Status200OK, reviews.Map(review => review.ToReviewResponseDto(review.User)))),
            error => error.ActionResult
        );
    }
    
    
    [Authenticated]
    [HttpPost("{bookId}/reviews")]
    public async Task<IActionResult> PostReview(
        [FromRoute] Guid bookId,
        [FromBody] CreateReviewRequestDto request
        )
    {
        var userId = contextService.Uuid;
        
        var result = await createReviewUseCase.InvokeAsync(request.ToCreateReviewDto(userId, new UuidVo(bookId)));
    
        return result.ActionResultHanlder(
            review => Created("", BaseResponse.CreateSuccess(StatusCodes.Status201Created ,review.ToReviewResponseDto(review.User))),
            error => error.ActionResult
        );
    }
}