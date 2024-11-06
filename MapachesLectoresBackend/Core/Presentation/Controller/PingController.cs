using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Core.Presentation.Controller;


[ApiController]
public class PingController : ControllerBase
{
    
    [HttpGet("/ping")]
    public IActionResult Ping()
    {
        return Ok("pong");
    }
}