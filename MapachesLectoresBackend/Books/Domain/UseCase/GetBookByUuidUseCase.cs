using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.UseCase;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class GetBookByUuidUseCase(
    GetItemByUuidUseCase<Book> getItemByUuidUseCase
)
{
    public Task<DataResult<Book>> InvokeAsync(Guid bookId)
    {
        var includesSpec = new BookSpecifications.IncludesAuthors()
            .And(new BookSpecifications.IncludesCategories())
            .And(new BookSpecifications.IncludesPublisher());
        
        return getItemByUuidUseCase.InvokeAsync(bookId, includesSpec);
    }
}