using MapachesLectoresBackend.Auth.Presentation.Middleware;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.UseCase;
using MapachesLectoresBackend.Books.Presentation.Mapper;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Books.Presentation.Controller;


[ApiController]
[Route("[controller]")]
public class AuthorsController(
    CreateAuthorUseCase createAuthorUseCase
) : ControllerBase {

    [Authenticated]
    [HttpPost]
    public async Task<IActionResult> CreateAuthor(
        [FromBody] CreateAuthorRequestDto request
    ){
        var result = await createAuthorUseCase.InvokeAsync(request);

        return result.ActionResultHanlder(
            author => Created("", BaseResponse.CreateSuccess(StatusCodes.Status201Created, author.ToResponseDto())),
            error => error.ActionResult
        );
    }

}