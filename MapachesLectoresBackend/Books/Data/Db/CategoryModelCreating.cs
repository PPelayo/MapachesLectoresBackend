using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Data.Db;
using Microsoft.EntityFrameworkCore;

namespace MapachesLectoresBackend.Books.Data.Db;

public class CategoryModelCreating
{
    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyBaseEntityConfig<Category>();
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("category");
            entity.HasKey(e => e.Id);
        });
    }
}