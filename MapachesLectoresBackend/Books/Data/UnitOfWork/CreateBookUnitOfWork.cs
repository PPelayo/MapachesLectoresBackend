using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.UnitOfWork;
using MapachesLectoresBackend.Core.Data.Db;
using MapachesLectoresBackend.Core.Data.UnitOfWork;
using MapachesLectoresBackend.Core.Domain.Repository;

namespace MapachesLectoresBackend.Books.Data.UnitOfWork;

public class CreateBookUnitOfWork(
    MapachesDbContext dbContext,
    IRepository<Book> bookRepository,
    IRepository<BooksAuthors> bookAuthorsRepository,
    IRepository<BooksCategories> bookCategoriesRepository,
    IRepository<Author> authorRepository,
    IRepository<Category> categoryRepository
) : BaseUnitOfWork(dbContext), ICreateBookUnitOfWork
{
    public IRepository<Book> BookRepository => bookRepository;
    public IRepository<BooksAuthors> BookAuthorsRepository => bookAuthorsRepository;
    public IRepository<Author> AuthorRepository => authorRepository;
    public IRepository<Category> CategoriesRepository => categoryRepository;
    public IRepository<BooksCategories> BookCategoriesRepository => bookCategoriesRepository;
}