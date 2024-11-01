using MapachesLectoresBackend.Core.Domain.Model;

namespace MapachesLectoresBackend.Books.Domain.Model;

public class BooksCategories : BaseEntity
{
    public uint BookId { get; set; }
    public virtual Book Book { get; set; } = null!;
    
    public uint CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
}