
namespace MapachesLectoresBackend.Auth.Presentation.Dto;

public record RegisterUserRequest(
    string Name,
    string Email,
    string Password
);