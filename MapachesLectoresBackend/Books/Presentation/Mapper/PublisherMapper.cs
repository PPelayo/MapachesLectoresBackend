using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Presentation.Dto;

namespace MapachesLectoresBackend.Books.Presentation.Mapper;


public static class PublisherMapper {


    public static PublisherResponseDto ToResponseDto(this Publisher publisher){
        
        return new PublisherResponseDto(
            publisher.Name
        ) {
            ItemUuid = publisher.ItemUuid,
            CreatedAt = publisher.CreatedAt,
            UpdatedAt = publisher.UpdatedAt
        };

    }
}