using MapachesLectoresBackend.Books.Domain.UseCase;
using MapachesLectoresBackend.Books.Presentation.Mapper;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using MapachesLectoresBackend.Reviews.Domain.UseCase;
using MapachesLectoresBackend.Reviews.Presentation.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Books.Presentation.Controller;

[ApiController]
[Route("[controller]")]
public class BookController(
    GetBooksUseCase getBooksUseCase,
    GetBookByIdUseCase getBookByIdUseCase,
    GetReviewsFromBookUseCase getReviewsFromBookUseCase
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(
        [FromRoute] uint id    
    )
    {
        var result = await getBookByIdUseCase.InvokeAsync(id);

        return result.ActionResultHanlder(
            book => Ok(BaseResponse.CreateSuccess(StatusCodes.Status200OK, book.ToResponseDto())),
            error => error.ActionResult
        );
    }

    [HttpGet("{id}/reviews")]
    public async Task<IActionResult> GetReviews(
        [FromRoute] Guid id,
        [FromQuery] UserPagination pagination
    )
    {
        var result = await getReviewsFromBookUseCase.InvokeAsync(id, pagination);
        return result.ActionResultHanlder(
            reviews => Ok(BaseResponse.CreateSuccess(StatusCodes.Status200OK, reviews.Map(review => review.ToReviewResponseDto(review.User)))),
            error => error.ActionResult
        );
    }
}