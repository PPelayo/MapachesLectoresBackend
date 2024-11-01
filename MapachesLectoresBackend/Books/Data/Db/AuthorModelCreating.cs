using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Data.Db;
using Microsoft.EntityFrameworkCore;

namespace MapachesLectoresBackend.Books.Data.Db;

public class AuthorModelCreating
{
    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyBaseEntityConfig<Author>();
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("author");
            entity.HasKey(e => e.Id);
        });
    }
}