using MapachesLectoresBackend.Auth.Domain.UseCase;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Auth.Presentation;


[ApiController]
[Route("[controller]")]
public class AuthController(
    LoginUseCase loginUseCase
) : ControllerBase
{
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await loginUseCase.InvokeAsync(request.Email, request.Password);

        return result.ActionResultHanlder(
            wrapper => Ok(BaseResponse.CreateSuccess(StatusCodes.Status200OK, wrapper)),
            error => error.ActionResult        
        );
    }
}