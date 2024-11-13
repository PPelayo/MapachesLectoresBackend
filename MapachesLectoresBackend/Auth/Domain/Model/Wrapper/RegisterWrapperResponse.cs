using MapachesLectoresBackend.Users.Domain.Model;

namespace MapachesLectoresBackend.Auth.Domain.Model.Wrapper;

public record RegisterWrapperResponse(
    User User,
    JwtsWrapper Tokens
);
