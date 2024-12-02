using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Users.Domain.Model;

namespace MapachesLectoresBackend.Requests.Domain.Model
{
    public class RequestCreateBook : BaseEntity, IRequest, IBook
    {
        public uint Id { get; set; }
        public uint UserId { get; set; }
        public virtual User? User { get; set; } = null;

        //Informacion del libro
        public required string Name { get; set; }
        public required string Synopsis { get; set; }
        public required DateTime PublishedDate { get; set; }
        public required string CoverUrl { get; set; }
        public required uint NumberOfPages { get; set; }
        public required uint PublisherId { get; set; }
    }
}
