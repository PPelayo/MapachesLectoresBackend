using MapachesLectoresBackend.Core.Presentation.Dtos;

namespace MapachesLectoresBackend.Books.Presentation.Dto;

public record BookResponseDto(
    uint Id,
    string Name,
    string Synopsis,
    DateTime PublishedDate,
    string CoverUrl,
    uint NumberOfPages,
    uint PublisherId,
    IEnumerable<CategoryResponseDto> Categories,
    IEnumerable<AuthorResponseDto> Authors
) : BaseDtoResponse;