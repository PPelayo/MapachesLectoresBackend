using MapachesLectoresBackend.Auth.Domain.Model.Errors;
using MapachesLectoresBackend.Auth.Domain.Model.Wrapper;
using MapachesLectoresBackend.Auth.Domain.Service;
using MapachesLectoresBackend.Auth.Domain.Utils;
using MapachesLectoresBackend.Core.Domain.Extensions;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.UnitOfWork;
using MapachesLectoresBackend.Users.Domain.Model;
using MapachesLectoresBackend.Users.Domain.Model.Enums;
using MapachesLectoresBackend.Users.Domain.Specification;

namespace MapachesLectoresBackend.Auth.Domain.UseCase;


public class RegisterUserUseCase(
    IGenericUnitOfWork<User> userUnitOfWork,
    JwtService jwtService
)
{
    public async Task<DataResult<RegisterWrapperResponse>> InvokeAsync(
        string userName,
        string nombre,
        string password
    )
    {
        if (await IsUserAlreadyRegisteredAsync(userName))
            return DataResult<RegisterWrapperResponse>.CreateFailure(RegisterUserErrors.UserAlredyExists_400());

        try
        {
            await userUnitOfWork.BeginTransaction();
            var user = await CreateUserAndSave(userName, nombre, password);
            await userUnitOfWork.Save();
            await userUnitOfWork.Commit();

            var accessToken = jwtService.GenerateAccessToken(user.ItemUuid, (UserRoleEnum)user.Role);
            var refreshToken = jwtService.GenerateRefreshToken(user.ItemUuid, (UserRoleEnum)user.Role);

            var wrapper = new JwtsWrapper(accessToken.Value, refreshToken.Value);

            return DataResult<RegisterWrapperResponse>.CreateSuccess(new RegisterWrapperResponse(user, wrapper));
        }
        catch (Exception e)
        {
            await userUnitOfWork.Rollback();
            return DataResult<RegisterWrapperResponse>.CreateFailure(e.CreateExceptionResult());
        }
    }

    private async Task<bool> IsUserAlreadyRegisteredAsync(string userName)
    {
        var specEmail = new UserSpecifications.GetByUserName(userName);
        var countOfUsers = await userUnitOfWork.Repository.CountAsync(specEmail);

        return countOfUsers > 0;
    }


    private async Task<User> CreateUserAndSave(
        string userName,
        string nombre,
        string password
    )
    {
        var user = new User
        {
            UserName = userName,
            Name = nombre,
            Password = password,
            Role = (int)UserRoleEnum.Regular
        };

        var encryptedPass = PasswordEncryptor.Encrypt(user, password);
        user.Password = encryptedPass;

        return await userUnitOfWork.Repository.InsertAsync(user);
    }
}