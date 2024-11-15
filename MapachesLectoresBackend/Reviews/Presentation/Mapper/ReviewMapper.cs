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
    public static CreateReviewDto ToCreateReviewDto(this CreateReviewRequestDto request, UserUuidVo userId)
    {
        return new CreateReviewDto(
            userId,
            new UserUuidVo(request.BookId) ,
            request.Description,
            new RatingVo(request.GeneralRating)
        );
    }
    
    public static ReviewResponseDto ToReviewResponseDto(this Review review, User? user = null)
    {
        return new ReviewResponseDto(
            review.BookId,
            review.Description,
            review.GeneralRating,
            review.PublishDate,
            user?.ToResponseDto()
        );
    }
}