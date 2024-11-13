using MapachesLectoresBackend.Auth.Domain.Model.Errors;
using MapachesLectoresBackend.Auth.Domain.Model.Wrapper;
using MapachesLectoresBackend.Auth.Domain.Service;
using MapachesLectoresBackend.Auth.Domain.Utils;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Users.Domain.Model;
using MapachesLectoresBackend.Users.Domain.Model.Enums;
using MapachesLectoresBackend.Users.Domain.Specification;

namespace MapachesLectoresBackend.Auth.Domain.UseCase;

public class LoginUseCase(
    IRepository<User> userRepository,
    JwtService jwtService
)
{
    public async Task<DataResult<JwtsWrapper>> InvokeAsync(string email, string password)
    {
        var getUserSpec = new UserSpecifications.GetByUserName(email);
        var user = await userRepository.GetFirstAsync(getUserSpec);

        if (user == null)
            return DataResult<JwtsWrapper>.CreateFailure(LoginErrors.EmailOrPasswordIncorrect_400());

        if (!PasswordEncryptor.ValidatePassword(user, password))
            return DataResult<JwtsWrapper>.CreateFailure(LoginErrors.EmailOrPasswordIncorrect_400());

        var accessToken = jwtService.GenerateAccessToken(user.ItemUuid, (UserRoleEnum)user.Role);
        var refreshToken = jwtService.GenerateRefreshToken(user.ItemUuid, (UserRoleEnum)user.Role);

        var wrapper = new JwtsWrapper(accessToken.Value, refreshToken.Value);

        return DataResult<JwtsWrapper>.CreateSuccess(wrapper);
    }
}