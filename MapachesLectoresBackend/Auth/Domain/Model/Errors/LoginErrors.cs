using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Auth.Domain.Model.Errors;

public class LoginErrors : BaseError
{
    private LoginErrors(string msg, int statusCode) : base(msg, statusCode)
    {
    }
    
    public static LoginErrors EmailOrPasswordIncorrect_400() => new("Email or password incorrect", StatusCodes.Status400BadRequest);
}