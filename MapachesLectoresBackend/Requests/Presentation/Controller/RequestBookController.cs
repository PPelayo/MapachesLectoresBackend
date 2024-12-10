using MapachesLectoresBackend.Auth.Presentation.Middleware;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Services;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using MapachesLectoresBackend.Requests.Domain.UseCase;
using MapachesLectoresBackend.Users.Domain.Model.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Requests.Presentation.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class RequestBookController(
        IHttpContextService httpContextService,
        GetRequestsCreateBookUseCase getRequestsCreateBookUseCase,
        AddRequestCreateBookUseCase addRequestCreateBookUseCase
        
    ) : ControllerBase
    {

        [Authenticated]
        [CheckUserRole(UserRoleEnum.Admin, UserRoleEnum.Moderator)]
        [HttpGet("create")]
        public async Task<IActionResult> GetCreateRequest(
             [FromQuery] UserPagination pagination    
        )
        {
            var result = await getRequestsCreateBookUseCase.InvokeAsync(pagination);

            return Ok(result);
        }

        [Authenticated]
        [HttpPost("create")]
        public async Task<IActionResult> InsertCreateRequest(
            [FromBody] CreateBookRequestDto createBookRequestDto    
        )
        {
            var userId = httpContextService.Uuid;
            var result = await addRequestCreateBookUseCase.InvokeAsync(createBookRequestDto, userId.Value);

            return result.ActionResultHanlder(
                request => Created("",BaseResponse.CreateSuccess(201, request)),
                error => error.ActionResult
            );
        }

    }
}
