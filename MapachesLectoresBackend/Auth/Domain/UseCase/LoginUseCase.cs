using MapachesLectoresBackend.Auth.Domain.Model.Errors;
using MapachesLectoresBackend.Auth.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.UnitOfWork;
using MapachesLectoresBackend.Users.Domain.Model;
using MapachesLectoresBackend.Users.Domain.Specification;
using MapachesLectoresBackend.Users.Domain.UseCase;

namespace MapachesLectoresBackend.Auth.Domain.UseCase;

public class LoginUseCase(
    IRepository<User> userRepository
)
{
    public async Task<DataResult<JwtVo>> InvokeAsync(string email, string password)
    {
        var getUserSpec = new UserSpecifications.GetByUserName(email);
        var user = await userRepository.GetFirstAsync(getUserSpec);

        if (user == null)
            return DataResult<JwtVo>.CreateFailure(LoginErrors.EmailOrPasswordIncorrect_400());

        if (user.Password != password)
            throw new Exception("Invalid password");

        throw new NotImplementedException();
    }
}