using System.ComponentModel.DataAnnotations;
using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Users.Domain.Model;

namespace MapachesLectoresBackend.Reviews.Domain.Model;

public class Review : BaseEntity {

    // public uint Id { get; set; }
    
    [MaxLength(50)]
    public required string UserId { get; set; }
    
    [MaxLength(50)]
    public required string BookId { get; set; }

    [MaxLength(255)]
    public required string Title { get; set; }
    
    [MaxLength(999)]
    public required string Description { get; set; }

    [Range(0, 5)]
    public uint GeneralRating { get; set; }

    public DateTime PublishDate { get; set; }


    public virtual User User { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;
}
