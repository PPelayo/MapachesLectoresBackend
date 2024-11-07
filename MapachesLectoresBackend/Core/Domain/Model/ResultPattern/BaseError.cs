using MapachesLectoresBackend.Core.Presentation.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

public class BaseError : IError
{
    public string Message { get; }
    public int StatusCode { get; }
    
    public IActionResult ActionResult { get; }

    public BaseError(string msg, int statusCode)
    {
        Message = msg;
        StatusCode = statusCode;
        ActionResult = new ObjectResult(CreateResponse()) { StatusCode = StatusCode };
    }
    
    private BaseResponse CreateResponse()
    {
        return BaseResponse.CreateError(StatusCode, Message);
    }
}