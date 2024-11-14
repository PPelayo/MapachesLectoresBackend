using MapachesLectoresBackend.Auth.Presentation.Middleware;
using MapachesLectoresBackend.Core.Domain.Services;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using MapachesLectoresBackend.Users.Domain.UseCase;
using MapachesLectoresBackend.Users.Presentation.Dtos;
using MapachesLectoresBackend.Users.Presentation.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Users.Presentation.Controller;


[ApiController]
[Route("[controller]")]
public class UserController(
    IHttpContextService httpContextService,
    CreateUserUseCase createUserUseCase,
    GetUserByIdUseCase getUserByIdUseCase
) : ControllerBase
{
    
    [Authenticated]
    [HttpGet]
    public async Task<IActionResult> GetCurrentUser(){
        var userUuid = httpContextService.UserUuid;


        return Ok();        
    }

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