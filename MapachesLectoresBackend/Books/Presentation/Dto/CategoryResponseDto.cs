using MapachesLectoresBackend.Core.Presentation.Dtos;

namespace MapachesLectoresBackend.Books.Presentation.Dto;

public record CategoryResponseDto(
    uint Id,
    string Description
) : BaseDtoResponse;