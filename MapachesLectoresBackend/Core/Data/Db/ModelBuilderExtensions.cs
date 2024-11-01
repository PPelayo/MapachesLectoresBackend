using MapachesLectoresBackend.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MapachesLectoresBackend.Core.Data.Db;

public static class ModelBuilderExtensions
{
    public static void ApplyBaseEntityConfig<T>(this ModelBuilder modelBuilder) where T : class, IEntity
    {
        modelBuilder.Entity<T>(entity =>
        {
            entity.ApplyBaseEntityConfig();
        });
    }

    public static void ApplyBaseEntityConfig<T>(this EntityTypeBuilder<T> builder) where T : class, IEntity
    {
        builder.HasIndex(e => e.ItemUuid)
            .IsUnique();
        
        builder.Property(e => e.CreatedAt)
            .HasColumnType("DATETIME(3)")
            .IsRequired()
            .HasDefaultValueSql("NOW(3))");
        
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("DATETIME(3)")
            .IsRequired()
            .HasDefaultValueSql("NOW(3))"); 
    }
}