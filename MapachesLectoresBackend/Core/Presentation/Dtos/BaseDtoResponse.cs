namespace MapachesLectoresBackend.Core.Presentation.Dtos;

public record BaseDtoResponse
{
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public required string ItemUuid { get; init; }
}