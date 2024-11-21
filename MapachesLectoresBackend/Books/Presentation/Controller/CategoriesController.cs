using MapachesLectoresBackend.Books.Domain.UseCase;
using MapachesLectoresBackend.Books.Presentation.Dto;
using MapachesLectoresBackend.Books.Presentation.Mapper;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Books.Presentation.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController(
        GetCategoriesUseCase getCategoriesUseCase    
    ) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(typeof(BaseGenericResponse<PaginationResult<CategoryResponseDto>, string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories(
            [FromQuery] UserPagination pagination,
            [FromQuery] string? search
        )
        {

            var results = await getCategoriesUseCase.InvokeAsync(pagination, search);

            return Ok(BaseResponse.CreateSuccess(
                StatusCodes.Status200OK ,results.Map(cat => cat.ToCategoryResponseDto())
              
            ));
        }

    }
}
 