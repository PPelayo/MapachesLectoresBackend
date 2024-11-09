using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Presentation.Dto;

namespace MapachesLectoresBackend.Books.Presentation.Mapper;

public static class BookMapper
{
    public static BookResponseDto ToResponseDto(this Book book)
    {

        var categories = book.BooksCategories.Select(bc => bc.Category.ToCategoryResponseDto());
        var authors = book.BooksAuthors.Select(ba => ba.Author.ToResponseDto());
        return new BookResponseDto(
            Id: book.Id,
            Name: book.Name,
            Synopsis: book.Synopsis,
            PublishedDate: book.PublishedDate,
            CoverUrl: book.CoverUrl,
            NumberOfPages: book.NumberOfPages,
            PublisherId: book.PublisherId,
            Categories: categories,
            Authors: authors
        )
        {
            ItemUuid = book.ItemUuid,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt
        };
    }
}