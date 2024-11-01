using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Data.Db;
using Microsoft.EntityFrameworkCore;

namespace MapachesLectoresBackend.Books.Data.Db;

public class PublisherModelCreating
{
    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyBaseEntityConfig<Publisher>();
        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.ToTable("publisher");
            entity.HasKey(e => e.Id);

            entity.HasMany(p => p.Books)
                .WithOne(b => b.Publisher)
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(b => b.PublisherId)
                .HasPrincipalKey(p => p.Id);
        });
    }
}