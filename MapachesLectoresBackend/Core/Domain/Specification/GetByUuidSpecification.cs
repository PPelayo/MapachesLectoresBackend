using MapachesLectoresBackend.Core.Domain.Model;

namespace MapachesLectoresBackend.Core.Domain.Specification;


public sealed class GetByUuidSpecification<T>(string Uuid) 
    : BaseSpecification<T>(entity => entity.ItemUuid == Uuid) where T: class, IEntity;