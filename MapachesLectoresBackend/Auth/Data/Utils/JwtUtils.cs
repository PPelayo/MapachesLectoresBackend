using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MapachesLectoresBackend.Auth.Domain.Model.Errors;
using MapachesLectoresBackend.Auth.Domain.Utils;
using MapachesLectoresBackend.Core.Domain.Extensions;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using Microsoft.IdentityModel.Tokens;

namespace MapachesLectoresBackend.Auth.Data.Utils;

public class JwtUtils : IJwtUtils
{
    public string GenerateToken(List<Claim> claims, int tokenLifeTimeInMinutes, string secret)
    {
        var credentials = new SigningCredentials(GetSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(tokenLifeTimeInMinutes),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }

    public DataResult<IEnumerable<Claim>> ValidateToken(string token, string secret)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            IssuerSigningKey = GetSecurityKey(secret),
            ValidateIssuer = false,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true
        };
        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;

            return DataResult<IEnumerable<Claim>>.CreateSuccess(jwtToken.Claims);
        }
        catch (SecurityTokenMalformedException ex)
        {
            return DataResult<IEnumerable<Claim>>.CreateFailure(JwtValidationErrors.InvalidToken_401());
        }
        catch (SecurityTokenExpiredException ex)
        {
            return DataResult<IEnumerable<Claim>>.CreateFailure(JwtValidationErrors.InvalidToken_401());
        }
        catch (SecurityTokenInvalidSignatureException ex)
        {
            return DataResult<IEnumerable<Claim>>.CreateFailure(JwtValidationErrors.InvalidToken_401());
        }
        catch (ArgumentException ex)
        {
            return DataResult<IEnumerable<Claim>>.CreateFailure(JwtValidationErrors.InvalidToken_401());
        }
        catch (Exception e)
        {
            return DataResult<IEnumerable<Claim>>.CreateFailure(e.CreateExceptionResult());
        }
    }

    private SymmetricSecurityKey GetSecurityKey(string secret)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    }
}