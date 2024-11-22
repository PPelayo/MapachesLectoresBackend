using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Presentation.Dto;

namespace MapachesLectoresBackend.Books.Presentation.Mapper;

public static class BookMapper
{
    public static BookResponseDto ToResponseDto(this Book book, IEnumerable<Category>? categories = null, IEnumerable<Author>? authors = null, Publisher? publisher = null, int? reviewsCount = null, double? reviewsAvarage = null)
    {
        IEnumerable<CategoryResponseDto>? categoryResponseDtos = null;
        IEnumerable<AuthorResponseDto>? authorsResponseDtos = null;
        if(categories != null)
            categoryResponseDtos = categories.Select(cat => cat.ToCategoryResponseDto());
        if(authors != null)
            authorsResponseDtos = authors.Select(author => author.ToResponseDto());
        
        return new BookResponseDto(
            Id: book.Id,
            Name: book.Name,
            Synopsis: book.Synopsis,
            PublishedDate: book.PublishedDate,
            CoverUrl: book.CoverUrl,
            NumberOfPages: book.NumberOfPages,
            PublisherId: book.PublisherId,
            Categories: categoryResponseDtos,
            Authors: authorsResponseDtos,
            Publisher: publisher?.ToResponseDto(),
            ReviewsAvarage: reviewsAvarage,
            ReviewsCount: reviewsCount
        )
        {
            ItemUuid = book.ItemUuid,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt
        };
    }
}