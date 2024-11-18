using MapachesLectoresBackend.Core.Presentation.Dtos;

namespace MapachesLectoresBackend.Books.Presentation.Dto;


public record PublisherResponseDto(
    string Name
) : BaseDtoResponse;