using System.ComponentModel.DataAnnotations;
using MapachesLectoresBackend.Core.Domain.Model;

namespace MapachesLectoresBackend.Books.Domain.Model;

public class Author : BaseEntity
{
    public uint Id { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }
    [MaxLength(255)]
    public required string LastName { get; set; }
    
    
    public virtual ICollection<BooksAuthors> BooksAuthors { get; set; } = null!;
}