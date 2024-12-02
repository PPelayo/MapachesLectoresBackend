using MapachesLectoresBackend.Users.Domain.Model;

namespace MapachesLectoresBackend.Requests.Domain.Model
{
    public class PublisherRequest
    {
        public uint Id { get; set; }
        public uint UserId { get; set; }
        public uint Type { get; set; }
        public virtual User? User { get; set; } = null;
    }
}
