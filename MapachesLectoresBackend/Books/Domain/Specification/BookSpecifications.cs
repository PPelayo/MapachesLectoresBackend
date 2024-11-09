using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Specification;
using Microsoft.EntityFrameworkCore;

namespace MapachesLectoresBackend.Books.Domain.Specification;

public static class BookSpecifications
{
    public sealed class IncludesAuthors : BaseSpecification<Book>
    {
        public IncludesAuthors()
        {
            AddThenIncludes(q => 
                q
                    .Include(b => b.BooksAuthors)
                    .ThenInclude(ba => ba.Author)
                );
        }
    }
    
    public sealed class IncludesCategories : BaseSpecification<Book>
    {
        public IncludesCategories()
        {
            AddThenIncludes(q => 
                q
                    .Include(b => b.BooksCategories)
                    .ThenInclude(ba => ba.Category)
            );
        }
    }

    public sealed class GetById(uint id) : BaseSpecification<Book>(entity => entity.Id == id);
}