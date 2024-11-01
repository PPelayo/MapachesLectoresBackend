namespace MapachesLectoresBackend.Core.Domain.Model;

public interface IEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string ItemUuid { get; set; }
}