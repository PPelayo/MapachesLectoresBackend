
using System.Security.Claims;
using MapachesLectoresBackend.Auth.Domain.Model.Vo;
using MapachesLectoresBackend.Auth.Domain.Model.Wrapper;
using MapachesLectoresBackend.Auth.Domain.Utils;
using MapachesLectoresBackend.Core.Domain.Model.Exceptions;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Users.Domain.Model.Enums;

namespace MapachesLectoresBackend.Auth.Domain.Service;


public class JwtService(IJwtUtils jwtUtils, IConfiguration configuration)
{
    private const string UserIdClaim = "UserId";
    private const string UserRoleClaim = "Role";

    public JwtVo GenerateAccessToken(string UserId, UserRoleEnum userRole){
        var claims = new List<Claim>(){
            new(UserIdClaim, UserId),
            new(UserRoleClaim, ((int)userRole).ToString())
        };

        var accessTokenSecret = configuration[EnvConstants.AccessJwtSecret] ?? throw new InvalidEnvException();
        var lifeTime = int.Parse(configuration[EnvConstants.AccessTokenLifeTimeInMinutes] ?? throw new InvalidEnvException());
        var token = jwtUtils.GenerateToken(claims, lifeTime, accessTokenSecret);

        return new JwtVo(token);
    }

    public JwtVo GenerateRefreshToken(string UserId, UserRoleEnum userRole){
        var claims = new List<Claim>(){
            new(UserIdClaim, UserId),
            new(UserRoleClaim, ((int)userRole).ToString())
        };

        var accessTokenSecret = configuration[EnvConstants.RefreshJwtSecret]!;
        var lifeTime = int.Parse(configuration[EnvConstants.RefreshTokenLifeTimeInMinutes]!);
        var token = jwtUtils.GenerateToken(claims, lifeTime, accessTokenSecret);

        return new JwtVo(token);
    }

    public DataResult<JwtDeconstructedWrapper> ValidateAccessToken(string token){
        var secret = configuration[EnvConstants.AccessJwtSecret] ?? throw new InvalidEnvException();
        return ValidateToken(token, secret);
    }

        public DataResult<JwtDeconstructedWrapper> ValidateRefreshToken(string token){
        var secret = configuration[EnvConstants.RefreshJwtSecret] ?? throw new InvalidEnvException();
        return ValidateToken(token, secret);
    }

    private DataResult<JwtDeconstructedWrapper> ValidateToken(string token, string secret){
        var claimsResult = jwtUtils.ValidateToken(token, secret);
        if(claimsResult.IsFailure)
            return DataResult<JwtDeconstructedWrapper>.CreateFailure(claimsResult.FailureResult.Error);

        var claims = claimsResult.SuccessResult.Data.ToList();
        var userId = claims.FirstOrDefault(c => c.Type == UserIdClaim)?.Value!;
        var role = claims.FirstOrDefault(c => c.Type == UserRoleClaim)?.Value!;
        var roleEnum = Enum.Parse<UserRoleEnum>(role);

        return DataResult<JwtDeconstructedWrapper>.CreateSuccess(new JwtDeconstructedWrapper(userId, roleEnum));
    }
}