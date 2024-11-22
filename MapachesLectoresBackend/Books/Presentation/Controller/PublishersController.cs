using MapachesLectoresBackend.Auth.Presentation.Middleware;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.UseCase;
using MapachesLectoresBackend.Books.Presentation.Dto;
using MapachesLectoresBackend.Books.Presentation.Mapper;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Books.Presentation.Controller;

[ApiController]
[Route("[controller]")]
public class PublishersController(
    GetPublishersUseCase getPublishersUseCase,
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

    [HttpGet]
    [ProducesResponseType(typeof(BaseGenericResponse<PaginationResult<PublisherResponseDto>, string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPublishers(
        [FromQuery] UserPagination pagination,
        [FromQuery] string? search
    )
    {
        var results = await getPublishersUseCase.InvokeAsync(pagination, search);

        return Ok(
            BaseResponse.CreateSuccess(StatusCodes.Status200OK,results.Map(publisher => publisher.ToResponseDto()))
        );
    }

}