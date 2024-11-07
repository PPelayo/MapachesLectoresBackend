using MapachesLectoresBackend.Core.Domain.Utils;

namespace MapachesLectoresBackend.Core.Domain.Model;

public class BaseEntity : IEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string ItemUuid { get; set; } = UuidGenerator.Generate();
}