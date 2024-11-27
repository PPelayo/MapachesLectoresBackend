using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.UseCase;
using MapachesLectoresBackend.Users.Domain.Model;

namespace MapachesLectoresBackend.Users.Domain.UseCase;


public class GetUserByIdUseCase(
    GetItemByUuidUseCase<User> getItemByUuidUseCase
) {


    public Task<DataResult<User>> InvokeAsync(UuidVo uuid){
        return getItemByUuidUseCase.InvokeAsync(uuid.Value);
    }

}