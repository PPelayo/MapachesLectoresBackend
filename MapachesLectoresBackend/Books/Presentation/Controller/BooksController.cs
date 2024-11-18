using MapachesLectoresBackend.Auth.Presentation.Middleware;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.UseCase;
using MapachesLectoresBackend.Books.Presentation.Mapper;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.Services;
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
    GetBookByUuidUseCase getBookByUuidUseCase,
    GetReviewsFromBookUseCase getReviewsFromBookUseCase,
    CreateReviewUseCase createReviewUseCase,
    CreateBookUseCase createBookUseCase
) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> GetBooks(
        [FromQuery] UserPagination pagination,
        [FromQuery] string? search  
    )
    {
        var books = await getBooksUseCase.InvokeAsync(pagination, search);
        var booksResponses = books.Map(book =>
        {
            var categories = book.BooksCategories.Select(bc => bc.Category);
            var authors = book.BooksAuthors.Select(ba => ba.Author);
            return book.ToResponseDto(categories, authors, book.Publisher);
        });
        return Ok(
            BaseResponse.CreateSuccess(StatusCodes.Status200OK ,booksResponses)
        );
    }

    [HttpGet("{bookId}")]
    public async Task<IActionResult> GetBook(
        [FromRoute] Guid bookId    
    )
    {
        var result = await getBookByUuidUseCase.InvokeAsync(bookId);

        return result.ActionResultHanlder(
            book =>
            {
                var categories = book.BooksCategories.Select(bc => bc.Category);
                var authors = book.BooksAuthors.Select(ba => ba.Author);
                return Ok(BaseResponse.CreateSuccess(StatusCodes.Status200OK, book.ToResponseDto(categories, authors, book.Publisher)));
            },
            error => error.ActionResult
        );
    }

    [Authenticated]
    [HttpPost]
    public async Task<IActionResult> CreateBook(
        [FromBody] CreateBookRequestDto request
    )
    {
        var result = await createBookUseCase.InvokeAsync(request);
        return result.ActionResultHanlder(
            book => Ok(BaseResponse.CreateSuccess(StatusCodes.Status200OK, book.ToResponseDto())),
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
            reviews => 
                Ok(BaseResponse.CreateSuccess(StatusCodes.Status200OK, reviews.Map(review => review.ToReviewResponseDto(review.User)))),
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