using System.ComponentModel.DataAnnotations;
using MapachesLectoresBackend.Core.Domain.Model;

namespace MapachesLectoresBackend.Books.Domain.Model;

public class Publisher : BaseEntity
{
    public uint Id { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }
    
    public virtual ICollection<Book> Books { get; set; } = null!;
}