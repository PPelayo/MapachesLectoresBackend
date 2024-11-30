using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Specification;

namespace MapachesLectoresBackend.Books.Domain.Specification;

public static class BookCategoriesSpecifications
{
    public sealed class GetByBookId(uint bookId) 
        : BaseSpecification<BooksCategories>(entity => entity.BookId == bookId);
}