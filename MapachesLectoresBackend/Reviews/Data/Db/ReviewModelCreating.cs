using MapachesLectoresBackend.Core.Data.Db;
using MapachesLectoresBackend.Reviews.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace MapachesLectoresBackend.Reviews.Data.Db;

public static class ReviewModelCreating {

    public static void OnModelCreating (ModelBuilder modelBuilder) {

        modelBuilder.Entity<Review>(entity => {
            entity.ApplyBaseEntityConfig();
            entity.ToTable("review");

            entity.HasKey(e => e.Id);

            entity.HasOne(r => r.Book)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(r => r.BookId)
                .HasPrincipalKey(b => b.ItemUuid);

            entity.HasOne(r => r.User)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .HasPrincipalKey(u => u.ItemUuid)
                .HasForeignKey(r => r.UserId);
        });

    }

}
