using System.Security.Claims;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Auth.Domain.Utils;

public interface IJwtUtils
{
    string GenerateToken(List<Claim> claims, int tokenLifeTimeInMinutes, string secret);
    
    DataResult<IEnumerable<Claim>> ValidateToken(string token, string secret);
}