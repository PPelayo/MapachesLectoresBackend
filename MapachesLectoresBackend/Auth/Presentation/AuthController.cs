using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Auth.Presentation;


[ApiController]
[Route("[controller]")]
public class AuthController(
) : ControllerBase
{
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        
        
        return Ok();
    }
}