using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Books.Domain.Model.Error;

public class CreateBookErrors : BaseError
{
    private CreateBookErrors(string msg, int statusCode) : base(msg, statusCode)
    {
    }
    
    public static CreateBookErrors PublisherNotFound_400() => new("Publisher not found", 400);
    public static CreateBookErrors AuthorNotFound_400() => new("Authors not found", 400);
    public static CreateBookErrors CategoriesNotFound_400() => new("Categories not found", 400);
}