namespace MapachesLectoresBackend.Books.Domain.Model.Dto;

public record CreateBookRequestDto(
    string Name,
    string Synopsis,
    DateTime PublishedDate,
    uint NumberOfPages,
    Guid PublisherId,
    IEnumerable<Guid> Authors,
    IEnumerable<Guid> Categories
);
