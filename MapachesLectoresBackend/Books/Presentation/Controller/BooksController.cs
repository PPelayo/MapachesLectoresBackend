using MapachesLectoresBackend.Auth.Presentation.Middleware;
using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.UseCase;
using MapachesLectoresBackend.Books.Presentation.Dto;
using MapachesLectoresBackend.Books.Presentation.Mapper;
using MapachesLectoresBackend.Books.Presentation.Utils;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.Services;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using MapachesLectoresBackend.Reviews.Domain.UseCase;
using MapachesLectoresBackend.Reviews.Presentation.Dto;
using MapachesLectoresBackend.Reviews.Presentation.Mapper;
using MapachesLectoresBackend.Users.Domain.Model.Enums;
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
    CreateBookUseCase createBookUseCase,
    UploadImageBookUseCase uploadImageBookUseCase,
    GetReviewFromBookOfUserUseCase getReviewFromBookOfUserUseCase,
    DeleteBookUseCase deleteBookUseCase,
    UpdateBookUseCase updateBookUseCase
) : ControllerBase
{
    
    [HttpGet]
    [ProducesResponseType(typeof(BaseGenericResponse<PaginationResult<BookResponseDto>, string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBooks(
        [FromQuery] UserPagination pagination,
        [FromQuery] string? search,
        [FromQuery] string? order = null,
        [FromQuery] HashSet<string>? categories = null
    )
    {
        var bookOrder = BooksOrderEnum.Default;
        if(order != null)
        {
            var orderResult = BookOrderSerializer.Validate(order);
            if (orderResult.IsFailure)
                return orderResult.FailureResult.Error.ActionResult;

            bookOrder = orderResult.SuccessResult.Data;
        }

        var books = await getBooksUseCase.InvokeAsync(pagination, search, categories, bookOrder);
        var booksResponses = books.Map(bookWithReviewsAvarageDto =>
        {
            var categoriesOfBooks = bookWithReviewsAvarageDto.Book.BooksCategories.Select(bc => bc.Category);
            var authors = bookWithReviewsAvarageDto.Book.BooksAuthors.Select(ba => ba.Author);
            return bookWithReviewsAvarageDto.Book.ToResponseDto(
                categoriesOfBooks,
                authors,
                bookWithReviewsAvarageDto.Book.Publisher,
                reviewsCount: bookWithReviewsAvarageDto.ReviewsCount,
                reviewsAvarage: bookWithReviewsAvarageDto.ReviewsAvarage
            );
        });
        return Ok(
            BaseResponse.CreateSuccess(StatusCodes.Status200OK ,booksResponses)
        );
    }

    [HttpGet("{bookId}")]
    [ProducesResponseType(typeof(BaseGenericResponse<BookResponseDto, string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBook(
        [FromRoute] Guid bookId    
    )
    {
        var result = await getBookByUuidUseCase.InvokeAsync(bookId);

        return result.ActionResultHanlder(
            wrapper =>
            {
                var categories = wrapper.Book.BooksCategories.Select(bc => bc.Category);
                var authors = wrapper.Book.BooksAuthors.Select(ba => ba.Author);
                return Ok(
                    BaseResponse.CreateSuccess(StatusCodes.Status200OK,
                        wrapper.Book.ToResponseDto(categories, authors, wrapper.Book.Publisher, wrapper.ReviewsCount, wrapper.ReviewsAvarage)
                    ));
            },
            error => error.ActionResult
        );
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{bookId}")]
    [Authenticated]
    [CheckUserRole(UserRoleEnum.Admin)]
    public async Task<IActionResult> DeleteBook(
        [FromRoute] Guid bookId
    )
    {
        var result = await deleteBookUseCase.InvokeAsync(bookId);
        return result.ActionResultHanlder(
            _ => NoContent(),
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
    
    [Authenticated]
    [HttpPatch("{bookId}/cover")]
    public async Task<IActionResult> UpdateImage(
        [FromRoute] Guid bookId,
        [FromForm]IFormFile file
    )
    {
        var result = await uploadImageBookUseCase.InvokeAsync(file, bookId);

        return result.ActionResultHanlder(
            uri => Ok(BaseResponse.CreateSuccess(200, uri.AbsoluteUri)),
            error => error.ActionResult
        );
    }

    [HttpPut("{bookId}")]
    [Authenticated]
    [CheckUserRole(UserRoleEnum.Moderator, UserRoleEnum.Admin)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid bookId,
        [FromBody] CreateBookRequestDto request
    )
    {
        var result = await updateBookUseCase.InvokeAsync(bookId, request);
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
    [HttpGet("{bookId}/reviews/me")]
    public async Task<IActionResult> GetReviewOfUser(
        [FromRoute] Guid bookId
    )
    {
        var userId = contextService.Uuid;
        var result = await getReviewFromBookOfUserUseCase.InvokeAsync(bookId, userId.Value);
        return result.ActionResultHanlder(
            review => Ok(BaseResponse.CreateSuccess(StatusCodes.Status200OK, review.ToReviewResponseDto(review.User))),    
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