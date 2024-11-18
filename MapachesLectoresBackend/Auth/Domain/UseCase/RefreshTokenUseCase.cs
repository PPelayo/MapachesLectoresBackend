using MapachesLectoresBackend.Auth.Domain.Model.Errors;
using MapachesLectoresBackend.Auth.Domain.Model.Vo;
using MapachesLectoresBackend.Auth.Domain.Service;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Users.Domain.Model.Enums;
using MapachesLectoresBackend.Users.Domain.UseCase;

namespace MapachesLectoresBackend.Auth.Domain.UseCase;

public class RefreshTokenUseCase(
    JwtService jwtService,
    GetUserByIdUseCase getUserByIdUseCase
)
{
    public async Task<DataResult<JwtVo>> InvokeAsync(JwtVo refreshToken)
    {
        var result = jwtService.ValidateRefreshToken(refreshToken.Value);
        if (result.IsFailure)
            return DataResult<JwtVo>.CreateFailure(result.FailureResult.Error);
        var jwtData = result.SuccessResult.Data;
        var userResult = await getUserByIdUseCase.InvokeAsync(new UuidVo(jwtData.userId));
        if(userResult.IsFailure)
            return DataResult<JwtVo>.CreateFailure(new UnauthorizedError());
        
        var user = userResult.SuccessResult.Data;
        var userRole = (UserRoleEnum) user.Role;
        
        var accessToken = jwtService.GenerateAccessToken(user.ItemUuid, userRole);

        return DataResult<JwtVo>.CreateSuccess(accessToken);
    }
}