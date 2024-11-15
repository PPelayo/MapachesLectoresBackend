using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Reviews.Domain.Model.Vo;

namespace MapachesLectoresBackend.Reviews.Domain.Model.Dto;

public record CreateReviewDto(
    UserUuidVo UserId,
    UserUuidVo BookId,
    string Description,
    RatingVo GeneralRating
);