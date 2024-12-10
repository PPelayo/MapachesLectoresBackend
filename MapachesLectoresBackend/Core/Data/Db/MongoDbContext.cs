using MapachesLectoresBackend.Requests.Data.Db;
using MapachesLectoresBackend.Requests.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace MapachesLectoresBackend.Core.Data.Db
{
    public class MongoDbContext(DbContextOptions options) : DbContext(options)
    {

        public DbSet<RequestCreateBook> RequestCreateBook { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            RequestCreateBookModelCreating.OnModelCreating(modelBuilder);
        }
    }
}
