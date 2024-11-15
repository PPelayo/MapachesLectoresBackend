using MapachesLectoresBackend.Core.Domain.Model.Errors;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Users.Domain.Model;

namespace MapachesLectoresBackend.Users.Domain.UseCase;


public class GetUserByIdUseCase(
    IRepository<User> userRepository
) {


    public async Task<DataResult<User>> InvokeAsync(UuidVo uuid){

        var spec = new GetByUuidSpecification<User>(uuid.Value);
        var user = await userRepository.GetFirstAsync(spec);

        if(user == null)
            return DataResult<User>.CreateFailure(new NotFoundError());

        return DataResult<User>.CreateSuccess(user);
    }

}