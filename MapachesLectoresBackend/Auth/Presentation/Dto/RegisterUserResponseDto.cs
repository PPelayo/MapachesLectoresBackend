using MapachesLectoresBackend.Auth.Domain.Model.Wrapper;
using MapachesLectoresBackend.Users.Presentation.Dtos;

namespace MapachesLectoresBackend.Auth.Presentation.Dto;


public record RegisterUserResponseDto(
    UserResponseDto User,
    JwtsWrapper Tokens
);