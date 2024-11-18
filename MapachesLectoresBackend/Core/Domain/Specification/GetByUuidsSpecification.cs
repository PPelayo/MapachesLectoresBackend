using MapachesLectoresBackend.Core.Domain.Model;

namespace MapachesLectoresBackend.Core.Domain.Specification;

public sealed class GetByUuidsSpecification<T>
    : BaseSpecification<T> where T : class, IEntity
{
    public GetByUuidsSpecification(ISet<Guid> uuids)
    {
        var stringUuids = uuids.Select(u => u.ToString()).ToHashSet();
        
        SetCriteria(entity => stringUuids.Contains(entity.ItemUuid));
    }
}
