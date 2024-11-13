namespace MapachesLectoresBackend.Users.Presentation.Dtos;

public record UserResponseDto(
    string Name,
    string Email,
    string Role
);