using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Specification;

namespace MapachesLectoresBackend.Books.Domain.Specification;

public static class PublisherSpecifications {

    public sealed class GetByName(string name) 
        : BaseSpecification<Publisher>(entity => entity.Name.Trim().ToLower() == name.Trim().ToLower());


    public sealed class SearchByName(string name)
        : BaseSpecification<Publisher>(entity => entity.Name.ToLower().Trim().Contains(name.ToLower().Trim()));
}
