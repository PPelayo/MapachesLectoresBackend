using MapachesLectoresBackend.Auth.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Users.Domain.Model;

namespace MapachesLectoresBackend.Users.Domain.UseCase;

public class CreateUserUseCase(
    IRepository<User> userRepository    
)
{
    public Task<DataResult<JwtVo>> InvokeAsync(User user)
    {
        
    }   
}