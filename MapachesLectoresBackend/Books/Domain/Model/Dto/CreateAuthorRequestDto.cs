namespace MapachesLectoresBackend.Books.Domain.Model.Dto;

public record CreateAuthorRequestDto(
    string Name,
    string LastName
);