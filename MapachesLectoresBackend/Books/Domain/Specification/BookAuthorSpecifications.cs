using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Specification;

namespace MapachesLectoresBackend.Books.Domain.Specification;

public static class BookAuthorSpecifications
{
    public sealed class GetByBookId(uint bookId) 
        : BaseSpecification<BooksAuthors>(entity => entity.BookId == bookId);
}