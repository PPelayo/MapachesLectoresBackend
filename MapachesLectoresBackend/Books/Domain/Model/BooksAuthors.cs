using MapachesLectoresBackend.Core.Domain.Model;

namespace MapachesLectoresBackend.Books.Domain.Model;

public class BooksAuthors : BaseEntity
{
    public uint BookId { get; set; }
    public virtual Book Book { get; set; } = null!;
    
    public uint AuthorId { get; set; }
    public virtual Author Author { get; set; } = null!;
}