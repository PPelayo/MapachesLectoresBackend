using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Utils;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class GetBooksUseCase(
    IRepository<Book> bookRepository     
)
{
    public async Task<PaginationResult<Book>> InvokeAsync(IPagintaion pagintaion)
    {
        var books = await bookRepository.GetAsync(pagintaion.ToQueryPagination());

        return books.ToPaginationResult(pagintaion.ToQueryPagination());
    }
}