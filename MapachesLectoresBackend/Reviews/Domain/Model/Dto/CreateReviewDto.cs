using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Reviews.Domain.Model.Vo;

namespace MapachesLectoresBackend.Reviews.Domain.Model.Dto;

public record CreateReviewDto(
    UuidVo UserId,
    UuidVo BookId,
    string Description,
    RatingVo GeneralRating
);