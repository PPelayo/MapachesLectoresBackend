using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Core.Domain.Model.ResultPattern;


public interface IError
{
    string Message { get; }
    int StatusCode { get; }
    IActionResult ActionResult { get; }
}
