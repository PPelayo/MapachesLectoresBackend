using MapachesLectoresBackend.Books.Data.Db;
using MapachesLectoresBackend.Books.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace MapachesLectoresBackend.Core.Data.Db;

public class MapachesDbContext(DbContextOptions<MapachesDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BooksCategories> BooksCategories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        BookModelCreating.OnModelCreating(modelBuilder);
        
        CategoryModelCreating.OnModelCreating(modelBuilder);
    }

}