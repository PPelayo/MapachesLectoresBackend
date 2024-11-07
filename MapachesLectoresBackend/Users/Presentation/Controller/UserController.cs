using MapachesLectoresBackend.Core.Presentation.Dtos;
using MapachesLectoresBackend.Users.Domain.UseCase;
using MapachesLectoresBackend.Users.Presentation.Dtos;
using MapachesLectoresBackend.Users.Presentation.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Users.Presentation.Controller;


[ApiController]
[Route("[controller]")]
public class UserController(
    CreateUserUseCase createUserUseCase    
) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserDto createUserDto    
    )
    {
        var result = await createUserUseCase.InvokeAsync(createUserDto.ToUser());

        return result.ActionResultHanlder(
            user =>
            {
                var response = BaseResponse.CreateSuccess(StatusCodes.Status201Created, user);
                return Created("", response);
            },
            error => error.ActionResult
        );
    }
}