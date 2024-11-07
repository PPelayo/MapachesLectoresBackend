using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Users.Domain.Model.Error;

public class CreateUserErrors : BaseError
{
    private CreateUserErrors(string msg, int statusCode) : base(msg, statusCode)
    {
    }

    public static CreateUserErrors UserNameAlredyExists_400() =>
        new CreateUserErrors("User name already exists", StatusCodes.Status400BadRequest);
}