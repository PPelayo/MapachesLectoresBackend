using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Presentation.Dto;

namespace MapachesLectoresBackend.Books.Presentation.Mapper;

public static class AuthorMapper
{
    public static AuthorResponseDto ToResponseDto(this Author author)
    {
        return new AuthorResponseDto(
            Id: author.Id,
            Name: author.Name,
            LastName: author.LastName
        )
        {
            ItemUuid = author.ItemUuid,
            CreatedAt = author.CreatedAt,
            UpdatedAt = author.UpdatedAt
        };
    }
}