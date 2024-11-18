using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Utils;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class GetBooksUseCase(
    IRepository<Book> bookRepository     
)
{
    public async Task<PaginationResult<Book>> InvokeAsync(IPagintaion pagintaion, string? search = null)
    {
        var spec = new BookSpecifications.IncludesAuthors()
            .And(new BookSpecifications.IncludesCategories())
            .And(new BookSpecifications.IncludesPublisher());

        if(search != null)
            spec = spec.And(new BookSpecifications.SearchByName(search));
        
        var books = await bookRepository.GetAsync(pagintaion.ToQueryPagination(), spec);

        return books.ToPaginationResult(pagintaion.ToQueryPagination());
    }
}