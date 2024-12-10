using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Model;
using MapachesLectoresBackend.Users.Domain.Model;

namespace MapachesLectoresBackend.Requests.Domain.Model
{
    public class RequestCreateBook : BaseEntity, IRequest, IBook
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User? User { get; set; } = null;

        //Informacion del libro
        public required string Name { get; set; }
        public required string Synopsis { get; set; }
        public required DateTime PublishedDate { get; set; }
        public required string CoverUrl { get; set; }
        public required uint NumberOfPages { get; set; }
        public required uint PublisherId { get; set; }

        public ICollection<Guid> AuthorsIds { get; set; } = [];
        public ICollection<Guid> CategoriesIds { get; set; } = [];

        public virtual ICollection<Author>? Authors { get; set; } = null;
        public virtual ICollection<Category>? Categories { get; set; } = null;

        public virtual Publisher? Publisher { get; set; } = null;
    }
}
