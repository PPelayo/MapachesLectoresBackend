using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Users.Domain.Model;

namespace MapachesLectoresBackend.Requests.Domain.Model
{
    public interface IRequest : IEntity
    {
        uint Id { get; set; }
        uint UserId { get; set; }
        User? User { get; set; }
    }
}
