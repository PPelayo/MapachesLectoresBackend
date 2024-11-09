using MapachesLectoresBackend.Core.Presentation.Dtos;

namespace MapachesLectoresBackend.Books.Presentation.Dto;

public record AuthorResponseDto(
    uint Id,
    string Name,
    string LastName
) : BaseDtoResponse;