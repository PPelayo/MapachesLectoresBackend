using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Reviews.Domain.Model.Error;

public class CreateReviewErrors : BaseError
{
    private CreateReviewErrors(string msg, int statusCode) : base(msg, statusCode)
    {
    }
    
    public static CreateReviewErrors InvalidUserId_400() => new("El id del usuario no es válido", 400);
    public static CreateReviewErrors InvalidBookId_400() => new("El id del libro no es válido", 400);
}