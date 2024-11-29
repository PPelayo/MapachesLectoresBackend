using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Extensions;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.UnitOfWork;
using MapachesLectoresBackend.Core.Domain.UseCase;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class DeleteBookUseCase(
    IGenericUnitOfWork<Book> unitOfWork,
    GetItemByUuidUseCase<Book> getItemByUuidUseCase
)
{
    public async Task<DataResult<object>> InvokeAsync(Guid uuid)
    {
        var bookResult = await getItemByUuidUseCase.InvokeAsync(uuid);
        if (bookResult.IsFailure)
            return DataResult<object>.CreateFailure(bookResult.FailureResult.Error);

        var book = bookResult.SuccessResult.Data;
        
        try
        {
            await unitOfWork.BeginTransaction();
            await unitOfWork.Repository.DeleteAsync(book);
            await unitOfWork.Save();
            await unitOfWork.Commit();

            return DataResult<object>.CreateSuccess(new { });
        } catch (Exception e)
        {
            await unitOfWork.Rollback();
            return DataResult<object>.CreateFailure(e.CreateExceptionResult());
        }
    }
}