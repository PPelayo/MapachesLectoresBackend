using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.Model.Error;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Extensions;
using MapachesLectoresBackend.Core.Domain.Model.Errors;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.UnitOfWork;
using MapachesLectoresBackend.Core.Domain.UseCase;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class CreateBookUseCase(
    IGenericUnitOfWork<Book> bookUnitOfWork,
    GetItemByUuidUseCase<Publisher> getPublisherByUuidUseCase,
    IRepository<Author> authorRepository,
    GetItemByUuidUseCase<Category> getCategoryByUuidUseCase
)
{
    public async Task<DataResult<Book>> InvokeAsync(CreateBookRequestDto request)
    {
        var spec = new BookSpecifications.GetByName(request.Name);

        var posibleBook = await bookUnitOfWork.Repository.GetFirstAsync(spec);

        if (posibleBook != null)
            return DataResult<Book>.CreateFailure(new ResourceAlreadyExists(nameof(Book)));

        var publisherResult = await getPublisherByUuidUseCase.InvokeAsync(request.PublisherId);
        if (publisherResult.IsFailure)
            return DataResult<Book>.CreateFailure(CreateBookErrors.PublisherNotFound_400());
        var publisher = publisherResult.SuccessResult.Data;
        
        
        try
        {
            var bookToInsert = new Book()
            {
                Name = request.Name,
                Synopsis = request.Synopsis,
                PublishedDate = request.PublishedDate,
                NumberOfPages = request.NumberOfPages,
                CoverUrl = "",
                PublisherId = publisher.Id
            };

            await bookUnitOfWork.BeginTransaction();
            var bookInserted = await bookUnitOfWork.Repository.InsertAsync(bookToInsert);
            await bookUnitOfWork.Save();
            await bookUnitOfWork.Commit();

            return DataResult<Book>.CreateSuccess(bookInserted);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await bookUnitOfWork.Rollback();
            return DataResult<Book>.CreateFailure(ex.CreateExceptionResult());
        }
    }

    // private async Task<IEnumerable<Author>> GetAuthors(IEnumerable<Guid> authorIds)
    // {
    //     
    // }
    
}