using MapachesLectoresBackend.Core.Data.Db;
using MapachesLectoresBackend.Requests.Domain.Model;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MapachesLectoresBackend.Requests.Data.Db
{
    public static class RequestCreateBookModelCreating
    {

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestCreateBook>(entity =>
            {
                entity.ToCollection(nameof(RequestCreateBook));
                entity.ApplyBaseEntityConfig();
                entity.Ignore(r => r.Authors);
                entity.Ignore(r => r.Categories);
                entity.Ignore(r => r.User);
                entity.Ignore(r => r.Publisher);

            });
        }
    }
}
