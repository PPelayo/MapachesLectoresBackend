namespace MapachesLectoresBackend.Users.Presentation.Dtos;

public record CreateUserDto(
    string Name,
    string UserName,
    string Password
);