using MapachesLectoresBackend.Core.Domain.Model.Vo;

namespace MapachesLectoresBackend.Auth.Domain.Model.Vo;

public record JwtVo(string Jwt) : ValueObject<string>(Jwt)
{
    public string Jwt { get; init; } = Jwt;
}