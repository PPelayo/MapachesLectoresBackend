using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Auth.Domain.Model.Errors;

public class RegisterUserErrors : BaseError
{
    private RegisterUserErrors(string msg, int statusCode) : base(msg, statusCode)
    {
    }

    public static RegisterUserErrors UserAlredyExists_400() => new("User alredy exists", StatusCodes.Status400BadRequest);
}