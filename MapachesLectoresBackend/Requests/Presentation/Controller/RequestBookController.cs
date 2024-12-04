using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Requests.Domain.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Requests.Presentation.Controller
{
    [ApiController]
    public class RequestBookController(
        GetRequestsCreateBookUseCase getRequestsCreateBookUseCase    
        
    ) : ControllerBase
    {

        [HttpGet("/create")]
        public async Task<IActionResult> GetCreateRequest(
             [FromQuery] UserPagination pagination    
        )
        {
            var result = getRequestsCreateBookUseCase.InvokeAsync(pagination);

            return Ok(result);
        }

    }
}
