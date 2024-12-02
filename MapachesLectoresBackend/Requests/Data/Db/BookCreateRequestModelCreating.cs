using MapachesLectoresBackend.Core.Data.Db;
using MapachesLectoresBackend.Requests.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace MapachesLectoresBackend.Requests.Data.Db
{
    public static class BookCreateRequestModelCreating
    {

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestCreateBook>(entity =>
            {
                entity.ApplyBaseEntityConfig();
                entity.ToTable("request_create_book");
                entity.HasKey(bcr => bcr.Id);
            });
        }
    }
}
