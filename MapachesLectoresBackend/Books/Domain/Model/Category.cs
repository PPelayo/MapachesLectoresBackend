using MapachesLectoresBackend.Core.Domain.Model;

namespace MapachesLectoresBackend.Books.Domain.Model;

public class Category : BaseEntity
{
    public uint Id { get; set; }
    public required string Description { get; set; }


    public virtual BooksCategories BooksCategories { get; set; } = null!;
}