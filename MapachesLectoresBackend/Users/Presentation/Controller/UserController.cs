using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Users.Presentation.Controller;


[ApiController]
public class UserController : ControllerBase
{
    
    [HttpPost]
    public Task<IActionResult> CreateUser()
    {
        throw new NotImplementedException();
    }
}