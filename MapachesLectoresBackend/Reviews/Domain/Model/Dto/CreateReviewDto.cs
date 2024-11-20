using System.ComponentModel.DataAnnotations;
using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Reviews.Domain.Model.Vo;

namespace MapachesLectoresBackend.Reviews.Domain.Model.Dto;

public record CreateReviewDto(
    [MaxLength(255)]
    string Title,
    UuidVo UserId,
    UuidVo BookId,
    string Description,
    RatingVo GeneralRating
);