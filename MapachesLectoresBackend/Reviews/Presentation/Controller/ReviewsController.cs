using MapachesLectoresBackend.Core.Domain.Services;
using MapachesLectoresBackend.Reviews.Domain.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Reviews.Presentation.Controller;

[ApiController]
[Route("[controller]")]
public class ReviewsController(IHttpContextService contextService ,CreateReviewUseCase createReviewUseCase) : ControllerBase
{
}