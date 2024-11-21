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

            entity.HasData(DefaultData());
        });
    }

    private static IEnumerable<Category> DefaultData()
    {
        return
        [
            new Category { Id = 1, Description = "Terror", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174000" },
            new Category { Id = 2, Description = "Romance", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174001" },
            new Category { Id = 3, Description = "Suspense", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174002" },
            new Category { Id = 4, Description = "Policiaca", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174003" },
            new Category { Id = 5, Description = "Ciencia Ficción", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174004" },
            new Category { Id = 6, Description = "Fantasía", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174005" },
            new Category { Id = 7, Description = "Aventuras", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174006" },
            new Category { Id = 8, Description = "Misterio", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174007" },
            new Category { Id = 9, Description = "Drama", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174008" },
            new Category { Id = 10, Description = "Comedia", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174009" },
            new Category { Id = 11, Description = "Historia", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174010" },
            new Category { Id = 12, Description = "Biografía", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174011" },
            new Category { Id = 13, Description = "Autoayuda", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174012" },
            new Category { Id = 14, Description = "Cocina", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174013" },
            new Category { Id = 15, Description = "Viajes", CreatedAt = DateTime.Parse("2024-01-01"), UpdatedAt = DateTime.Parse("2024-01-01"), ItemUuid = "123e4567-e89b-12d3-a456-426614174014" },
        ];
    }
}