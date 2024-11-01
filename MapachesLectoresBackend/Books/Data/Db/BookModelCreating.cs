using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Data.Db;
using Microsoft.EntityFrameworkCore;

namespace MapachesLectoresBackend.Books.Data.Db;

public class BookModelCreating
{
    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyBaseEntityConfig<Book>();
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("book");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.CoverUrl)
                .HasColumnType("TEXT");
            
            entity.Property(e => e.Synopsis)
                .HasColumnType("TEXT")
                .IsRequired();
        });
        
        modelBuilder.ApplyBaseEntityConfig<BooksCategories>();
        modelBuilder.Entity<BooksCategories>(entity =>
        {
            entity.ToTable("books_categories");
            entity.HasKey(e => new { e.BookId, e.CategoryId });
            
            entity.HasOne(e => e.Book)
                .WithMany(e => e.BooksCategories)
                .HasForeignKey(e => e.BookId);
            
            entity.HasOne(e => e.Category)
                .WithMany(e => e.BooksCategories)
                .HasForeignKey(e => e.CategoryId);
        });
    }
}