﻿using System.ComponentModel.DataAnnotations;

namespace MapachesLectoresBackend.Reviews.Presentation.Dto;

public record CreateReviewRequestDto(
    string BookId,
    string Description,
    [Range(0,5)]
    uint GeneralRating    
);