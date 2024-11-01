using MapachesLectoresBackend.Books.Data.Db;
using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Users.Data.Db;
using MapachesLectoresBackend.Users.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace MapachesLectoresBackend.Core.Data.Db;

public class MapachesDbContext(DbContextOptions<MapachesDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BooksCategories> BooksCategories { get; set; }
    
    public DbSet<Author> Authors { get; set; }
    public DbSet<BooksAuthors> BooksAuthors { get; set; }
    
    public DbSet<Publisher> Publishers { get; set; }
    
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        BookModelCreating.OnModelCreating(modelBuilder);
        
        CategoryModelCreating.OnModelCreating(modelBuilder);
        
        AuthorModelCreating.OnModelCreating(modelBuilder);
        
        PublisherModelCreating.OnModelCreating(modelBuilder);
        
        UserModelCreating.OnModelCreating(modelBuilder);
    }

}