namespace MapachesLectoresBackend.Books.Domain.Model.Dto;

public record CreateBookRequestDto(
    string Name,
    string Synopsis,
    DateTime PublishedDate,
    uint NumberOfPages,
    Guid PublisherId,
    ISet<Guid> Authors,
    ISet<Guid> Categories
);
