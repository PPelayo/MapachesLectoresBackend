using MapachesLectoresBackend.Core.Domain.Model;

namespace MapachesLectoresBackend.Core.Domain.Specification;


public sealed class GetByUuidSpecification<T>(string uuid)
    : BaseSpecification<T>(entity => entity.ItemUuid == uuid) where T : class, IEntity
{
    public GetByUuidSpecification(Guid uuid)
        : this(uuid.ToString())
    {
    }
}