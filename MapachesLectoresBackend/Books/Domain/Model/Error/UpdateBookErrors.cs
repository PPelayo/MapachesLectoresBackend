using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Books.Domain.Model.Error;

public class UpdateBookErrors : BaseError
{
    private UpdateBookErrors(string msg, int statusCode) : base(msg, statusCode)
    {
    }
    
    public static UpdateBookErrors BookWitdhThisNameAlreadyExists_409() => new("El libro con este nombre ya existe", StatusCodes.Status409Conflict);
}