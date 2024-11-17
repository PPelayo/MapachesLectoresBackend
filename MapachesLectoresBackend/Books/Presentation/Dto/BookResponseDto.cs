using System.Text.Json.Serialization;
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
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    IEnumerable<CategoryResponseDto>? Categories,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    IEnumerable<AuthorResponseDto>? Authors
) : BaseDtoResponse;