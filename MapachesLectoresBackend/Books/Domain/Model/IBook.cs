using MapachesLectoresBackend.Reviews.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace MapachesLectoresBackend.Books.Domain.Model
{
    public interface IBook
    {
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(99999)]
        public string Synopsis { get; set; }
        public DateTime PublishedDate { get; set; }

        [MaxLength(99999)]
        public string CoverUrl { get; set; }
        public uint NumberOfPages { get; set; }

        public uint PublisherId { get; set; }

    }
}
