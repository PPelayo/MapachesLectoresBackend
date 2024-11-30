using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Specification;

namespace MapachesLectoresBackend.Books.Domain.Specification;

public static class AuthorSpecifications {

    public sealed class GetByName(string name) 
        : BaseSpecification<Author>(entity => entity.Name.Trim().ToLower() == name.Trim().ToLower());


    public sealed class GetByLastName(string lastName) 
        : BaseSpecification<Author>(entity => entity.LastName.Trim().ToLower() == lastName.Trim().ToLower());

    public sealed class SearchByNameAndLastName(string search)
        : BaseSpecification<Author>(entity => 
           (entity.Name + " " + entity.LastName).Trim().ToLower().Contains(search.Trim().ToLower()));
    
    public sealed class GetByBookId(uint bookId)
        : BaseSpecification<Author>(entity => entity.BooksAuthors.Any(ba => ba.BookId == bookId));
}
