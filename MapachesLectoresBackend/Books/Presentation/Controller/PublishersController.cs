using MapachesLectoresBackend.Auth.Presentation.Middleware;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.UseCase;
using MapachesLectoresBackend.Books.Presentation.Mapper;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Books.Presentation.Controller;

[ApiController]
[Route("[controller]")]
public class PublishersController(

    CreatePublisherUseCase createPublisherUseCase
) : ControllerBase {


    [Authenticated]
    [HttpPost]
    public async Task<IActionResult> CreatePublisher(
        [FromBody] CreatePublisherRequestDto request
    ) {

        var result = await createPublisherUseCase.InvokeAsync(request);

        return result.ActionResultHanlder(
            publisher => Created("", BaseResponse.CreateSuccess(StatusCodes.Status201Created, publisher.ToResponseDto())),
            error => error.ActionResult
        );
    }

}