using MapachesLectoresBackend.Books.Domain.UseCase;
using MapachesLectoresBackend.Books.Presentation.Mapper;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Books.Presentation.Controller;

[ApiController]
[Route("[controller]")]
public class BookController(
    GetBooksUseCase getBooksUseCase,
    GetBookByIdUseCase getBookByIdUseCase
) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> GetBooks(
        [FromQuery] UserPagination pagination    
    )
    {
        var books = await getBooksUseCase.InvokeAsync(pagination);
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
}