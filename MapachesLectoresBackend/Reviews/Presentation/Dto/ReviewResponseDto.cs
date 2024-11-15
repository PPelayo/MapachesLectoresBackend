using System.Text.Json.Serialization;
using MapachesLectoresBackend.Users.Presentation.Dtos;

namespace MapachesLectoresBackend.Reviews.Presentation.Dto;

public record ReviewResponseDto(
    string BookId,
    string Description,
    uint GeneralRating,
    DateTime PublishDateUtc,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    UserResponseDto? User = null
);