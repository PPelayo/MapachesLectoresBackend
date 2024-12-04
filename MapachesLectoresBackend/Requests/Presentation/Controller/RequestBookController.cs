using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Requests.Domain.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Requests.Presentation.Controller
{
    [ApiController]
    public class RequestBookController(
        GetRequestsCreateBookUseCase getRequestsCreateBookUseCase,
        AddRequestCreateBookUseCase addRequestCreateBookUseCase
        
    ) : ControllerBase
    {

        [HttpGet("create")]
        public async Task<IActionResult> GetCreateRequest(
             [FromQuery] UserPagination pagination    
        )
        {
            var result = getRequestsCreateBookUseCase.InvokeAsync(pagination);

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> InsertCreateRequest(
            [FromBody] CreateBookRequestDto createBookRequestDto    
        )
        {
            var result = await addRequestCreateBookUseCase.InvokeAsync(createBookRequestDto);

            return Ok(result);
        }

    }
}
