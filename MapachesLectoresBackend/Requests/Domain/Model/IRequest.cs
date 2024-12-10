using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Users.Domain.Model;

namespace MapachesLectoresBackend.Requests.Domain.Model
{
    public interface IRequest : IEntity
    {
        Guid UserId { get; set; }
        User? User { get; set; }
    }
}
