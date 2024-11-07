using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Auth.Domain.Model.Errors;

public class JwtValidationErrors : BaseError
{
    private JwtValidationErrors(string msg, int statusCode) : base(msg, statusCode)
    {
    }
    
    public static JwtValidationErrors InvalidToken_401() => new("Invalid token", 401);
}