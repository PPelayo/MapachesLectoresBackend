using MapachesLectoresBackend.Books.Domain.UseCase;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Books.Presentation.Controller;

[ApiController]
[Route("[controller]")]
public class BookController(
    GetBooksUseCase getBooksUseCase    
) : ControllerBase
{
    public async Task<IActionResult> GetBooks(
        [FromQuery] UserPagination pagination    
    )
    {
        var books = await getBooksUseCase.InvokeAsync(pagination);

        return Ok(books);
    }
}