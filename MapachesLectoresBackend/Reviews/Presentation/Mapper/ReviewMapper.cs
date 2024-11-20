using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Reviews.Domain.Model;
using MapachesLectoresBackend.Reviews.Domain.Model.Dto;
using MapachesLectoresBackend.Reviews.Domain.Model.Vo;
using MapachesLectoresBackend.Reviews.Presentation.Dto;
using MapachesLectoresBackend.Users.Domain.Model;
using MapachesLectoresBackend.Users.Presentation.Mappers;

namespace MapachesLectoresBackend.Reviews.Presentation.Mapper;

public static class ReviewMapper
{
    public static CreateReviewDto ToCreateReviewDto(this CreateReviewRequestDto request, UuidVo userId, UuidVo bookId)
    {
        return new CreateReviewDto(
            Title: request.Title,
            userId,
            bookId ,
            request.Description,
            new RatingVo(request.GeneralRating)
        );
    }
    
    public static ReviewResponseDto ToReviewResponseDto(this Review review, User? user = null)
    {
        return new ReviewResponseDto(
            review.BookId,
            review.Title,
            review.Description,
            review.GeneralRating,
            review.PublishDate,
            user?.ToResponseDto()
        )
        {
            ItemUuid = review.ItemUuid,
            CreatedAt = review.CreatedAt,
            UpdatedAt = review.UpdatedAt
        };
    }
}