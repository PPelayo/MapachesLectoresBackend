using MapachesLectoresBackend.Auth.Presentation.Middleware;
using MapachesLectoresBackend.Core.Domain.Services;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using MapachesLectoresBackend.Reviews.Domain.UseCase;
using MapachesLectoresBackend.Reviews.Presentation.Dto;
using MapachesLectoresBackend.Reviews.Presentation.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Reviews.Presentation.Controller;

[ApiController]
[Route("[controller]")]
public class ReviewController(IHttpContextService contextService ,CreateReviewUseCase createReviewUseCase) : ControllerBase
{
    [Authenticated]
    [HttpPost]
    public async Task<IActionResult> PostReview([FromBody] CreateReviewRequestDto request)
    {
        var userId = contextService.Uuid;
        
        var result = await createReviewUseCase.InvokeAsync(request.ToCreateReviewDto(userId));

        return result.ActionResultHanlder(
            review => Created("", BaseResponse.CreateSuccess(StatusCodes.Status201Created ,review.ToReviewResponseDto(review.User))),
            error => error.ActionResult
        );
    }
}