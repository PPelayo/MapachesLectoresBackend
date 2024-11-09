using MapachesLectoresBackend.Auth.Domain.Utils;
using MapachesLectoresBackend.Core.Domain.Extensions;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.UnitOfWork;
using MapachesLectoresBackend.Users.Domain.Model;
using MapachesLectoresBackend.Users.Domain.Model.Error;
using MapachesLectoresBackend.Users.Domain.Specification;

namespace MapachesLectoresBackend.Users.Domain.UseCase;

public class CreateUserUseCase(
    IGenericUnitOfWork<User> userUnitOfWork
)
{
    public async Task<DataResult<User>> InvokeAsync(User user)
    {
        var getUserSpec = new UserSpecifications.GetByUserName(user.Name);
        var userOnDb = await userUnitOfWork.Repository.GetFirstAsync(getUserSpec);

        if (userOnDb != null)
            return DataResult<User>.CreateFailure(CreateUserErrors.UserNameAlredyExists_400());

        try
        {
            user.Password = PasswordEncryptor.Encrypt(user, user.Password);
            await userUnitOfWork.BeginTransaction();
            var userCreated = await userUnitOfWork.Repository.InsertAsync(user);
            await userUnitOfWork.Save();
            await userUnitOfWork.Commit();

            return DataResult<User>.CreateSuccess(userCreated);
        }
        catch (Exception e)
        {
            await userUnitOfWork.Rollback();
            return DataResult<User>.CreateFailure(e.CreateExceptionResult());
        }
    }
}