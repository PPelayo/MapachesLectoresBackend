using System.ComponentModel.DataAnnotations;
using MapachesLectoresBackend.Core.Domain.Model;

namespace MapachesLectoresBackend.Books.Domain.Model;

public class Book : BaseEntity
{
    public uint Id { get; set; }
    
    [MaxLength(255)]
    public required string Name { get; set; }
    
    [MaxLength(99999)]
    public required string Synopsis { get; set; }
    public DateTime PublishedDate { get; set; }
    
    [MaxLength(99999)]
    public required string CoverUrl { get; set; }
    public uint NumberOfPages { get; set; }
    
    public uint PublisherId { get; set; }
    public virtual Publisher Publisher { get; set; } = null!;
    

    public virtual ICollection<BooksCategories> BooksCategories { get; set; } = null!;
    public virtual ICollection<BooksAuthors> BooksAuthors { get; set; } = null!;
}
