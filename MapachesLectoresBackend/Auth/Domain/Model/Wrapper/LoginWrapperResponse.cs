using MapachesLectoresBackend.Users.Domain.Model;

namespace MapachesLectoresBackend.Auth.Domain.Model.Wrapper;

public record LoginWrapperResponse(User User, JwtsWrapper Tokens);