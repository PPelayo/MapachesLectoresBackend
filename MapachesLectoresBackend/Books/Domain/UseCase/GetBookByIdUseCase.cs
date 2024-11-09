using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Model.Error;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Repository;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class GetBookByIdUseCase(IRepository<Book> bookRepository)
{
    public async Task<DataResult<Book>> InvokeAsync(uint id)
    {
        var spec = new BookSpecifications.IncludesAuthors()
            .And(new BookSpecifications.IncludesCategories())
            .And(new BookSpecifications.GetById(id));
        
        var book = await bookRepository.GetFirstAsync(spec);

        return book == null 
            ? DataResult<Book>.CreateFailure(GetBookError.NotFound_404(id)) 
            : DataResult<Book>.CreateSuccess(book);
    }
}