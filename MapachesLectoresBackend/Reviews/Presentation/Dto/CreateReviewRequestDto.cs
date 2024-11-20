using System.ComponentModel.DataAnnotations;

namespace MapachesLectoresBackend.Reviews.Presentation.Dto;

public record CreateReviewRequestDto(
    [MaxLength(255)]
    string Title,
    [MaxLength(999)]
    string Description,
    [Range(0,5)]
    uint GeneralRating    
);