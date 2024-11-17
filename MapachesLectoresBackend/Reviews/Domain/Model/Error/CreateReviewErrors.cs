using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Reviews.Domain.Model.Error;

public class CreateReviewErrors : BaseError
{
    private CreateReviewErrors(string msg, int statusCode) : base(msg, statusCode)
    {
    }
    
    public static CreateReviewErrors InvalidUserId_404() => new("El id del usuario no es válido", 404);
    public static CreateReviewErrors InvalidBookId_404() => new("El id del libro no es válido", 404);
    
    public static CreateReviewErrors UserAlreadyCommented_403() => new("El usuario ya ha comentado este libro", 403);
    public static CreateReviewErrors CommentOrRatingEmpty_400() => new("El comentario no puede estar vacio y la valoración debe ser entre 1 y 5 inclusives", 400);
}