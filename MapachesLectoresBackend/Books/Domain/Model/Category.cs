using System.ComponentModel.DataAnnotations;
using MapachesLectoresBackend.Core.Domain.Model;

namespace MapachesLectoresBackend.Books.Domain.Model;

public class Category : BaseEntity
{
    public uint Id { get; set; }
    [MaxLength(255)]
    public required string Description { get; set; }


    public virtual ICollection<BooksCategories> BooksCategories { get; set; } = null!;
}