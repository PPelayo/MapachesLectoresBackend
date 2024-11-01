namespace MapachesLectoresBackend.Core.Domain.Model;

public class BaseEntity : IEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required string ItemUuid { get; set; }
}