using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Extensions;
using MapachesLectoresBackend.Core.Domain.Model.Enums;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Services;
using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.UnitOfWork;
using MapachesLectoresBackend.Core.Domain.UseCase;

namespace MapachesLectoresBackend.Books.Domain.UseCase
{
    public class UploadImageBookUseCase(
        IImagenService imagenService,
        IGenericUnitOfWork<Book> bookUnitOfWork,
        GetItemByUuidUseCase<Book> getItemByUuidUseCase
    )
    {
        public async Task<DataResult<Uri>> InvokeAsync(IFormFile formFile, Guid bookId)
        {
            var bookResult = await getItemByUuidUseCase.InvokeAsync(bookId);
            if (bookResult.IsFailure)
                return DataResult<Uri>.CreateFailure(bookResult.FailureResult.Error);

            var book = bookResult.SuccessResult.Data;

            using (var stream = new MemoryStream()) 
            { 
                await formFile.CopyToAsync(stream);
                stream.Position = 0;
                try
                {
                    var url = await imagenService.UploadImageAsync(stream, ImagenTypesEnum.Book, bookId.ToString());

                    book.CoverUrl = url.AbsoluteUri;

                    await bookUnitOfWork.Save();
                    await bookUnitOfWork.Commit();

                    return DataResult<Uri>.CreateSuccess(url);

                } catch(Exception ex)
                {
                    Console.WriteLine($"Error guardando la imagen del libro {bookId}: {ex.Message}");
                    await bookUnitOfWork.Rollback();
                    return DataResult<Uri>.CreateFailure(ex.CreateExceptionResult());
                }

            }
        }

    }
}
