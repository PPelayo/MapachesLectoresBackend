using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Books.Domain.Model.Error;

public class CreateAuthorErrors : BaseError
{
    private CreateAuthorErrors(string msg, int statusCode) : base(msg, statusCode)
    {
    }

    public static CreateAuthorErrors AuthorAlreadyExists_400() 
        => new CreateAuthorErrors("The author with this name already exists", StatusCodes.Status400BadRequest);
}