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
    double? ReviewsAvarage = null,
    
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    int? ReviewsCount = null,
    
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    IEnumerable<CategoryResponseDto>? Categories = null,
    
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    IEnumerable<AuthorResponseDto>? Authors = null,
    
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    PublisherResponseDto? Publisher = null
    
) : BaseDtoResponse;