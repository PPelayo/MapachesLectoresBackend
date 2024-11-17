using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Model.Errors;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Specification;

namespace MapachesLectoresBackend.Core.Domain.UseCase;

public class GetItemByUuidUseCase<T>(
    IRepository<T> repository    
) where T : class, IEntity
{
    public async Task<DataResult<T>> InvokeAsync(Guid uuid, ISpecification<T>? aditionalSpec = null)
    {
        ISpecification<T> spec = new GetByUuidSpecification<T>(uuid);
        if(aditionalSpec != null)
            spec = spec.And(aditionalSpec);
        
        var item = await repository.GetFirstAsync(spec);

        return item == null
            ? DataResult<T>.CreateFailure(new NotFoundError())
            : DataResult<T>.CreateSuccess(item);
    } 
}