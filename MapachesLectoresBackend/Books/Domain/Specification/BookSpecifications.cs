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

    public sealed class IncludesPublisher : BaseSpecification<Book>{

        public IncludesPublisher(){
            AddInclude(entity => entity.Publisher);
        }
    }

    public sealed class GetById(uint id) : BaseSpecification<Book>(entity => entity.Id == id);

    /// <summary>
    /// Permite recuperar un libro cuya descripcion contenga la pasada por parametro
    /// </summary>
    /// <param name="name"></param>
    public sealed class SearchByName(string name)
        //No usar StringComparation por que no es un metodo apto para SQL Linq
        : BaseSpecification<Book>(entity => entity.Name.ToLower().Contains(name.ToLower()));

    /// <summary>
    /// Permite recuperar un libro cuya descripcion exacta coincida con la pasada por parametro
    /// </summary>
    /// <param name="name"></param>
    public sealed class GetByName(string name)
        : BaseSpecification<Book>(entity => entity.Name.Trim().ToLower() == name.Trim().ToLower());
}