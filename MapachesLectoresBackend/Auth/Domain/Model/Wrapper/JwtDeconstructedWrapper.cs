using MapachesLectoresBackend.Users.Domain.Model.Enums;

namespace MapachesLectoresBackend.Auth.Domain.Model.Wrapper;

public record JwtDeconstructedWrapper(
    string userId,
    UserRoleEnum role
);