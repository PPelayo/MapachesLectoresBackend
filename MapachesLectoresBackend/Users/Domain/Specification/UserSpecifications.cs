using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Users.Domain.Model;

namespace MapachesLectoresBackend.Users.Domain.Specification;

public static class UserSpecifications
{
    public sealed class GetByUserName(string userName)
        : BaseSpecification<User>(entity => entity.UserName == userName);
}