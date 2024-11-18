using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.UnitOfWork;

namespace MapachesLectoresBackend.Books.Domain.UnitOfWork;

public interface ICreateBookUnitOfWork : IUnitOfWork
{
    public IRepository<Book> BookRepository { get; }
    
    public IRepository<BooksAuthors> BookAuthorsRepository { get; }
    public IRepository<Author> AuthorRepository { get; }
    public IRepository<Category> CategoriesRepository { get; }
    public IRepository<BooksCategories> BookCategoriesRepository { get; }
    
}