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
    
    public static CreateBookErrors ToManyAuthors_400() => new("A book can have a maximum of 1000 authors.", 400);
    public static CreateBookErrors ToManyCategories_400() => new("A book can have a maximum of 1000 categories.", 400);
}