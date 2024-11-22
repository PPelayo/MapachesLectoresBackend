namespace MapachesLectoresBackend.Books.Domain.Model.Dto;

public record BookWithReviewsAvarageDto(
    Book Book,
    double ReviewsAvarage,
    int ReviewsCount
);  