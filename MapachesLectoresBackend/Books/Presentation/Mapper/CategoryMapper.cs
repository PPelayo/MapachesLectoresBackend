using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Presentation.Dto;

namespace MapachesLectoresBackend.Books.Presentation.Mapper;

public static class CategoryMapper
{
    public static CategoryResponseDto ToCategoryResponseDto(this Category category)
    {
        return new CategoryResponseDto(
            category.Id,
            category.Description
        )
        {
            ItemUuid = category.ItemUuid,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
    }
}