using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.Service;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Books.Domain.UnitOfWork;
using MapachesLectoresBackend.Core.Domain.Extensions;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.UseCase;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class UpdateBookUseCase(
    BookValidationService bookValidationService,
    ICreateBookUnitOfWork unitOfWork,
    GetItemByUuidUseCase<Book> getItemByUuidUseCase)
{
    public async Task<DataResult<Book>> InvokeAsync(Guid bookId, CreateBookRequestDto request)
    {
        var validationResult = await bookValidationService.ValidateBookRequestAsync(request);
        
        if(validationResult.IsFailure)
            return DataResult<Book>.CreateFailure(validationResult.FailureResult.Error);

        var (publisher, authors, categories) = validationResult.SuccessResult.Data;
        
        var bookResult = await getItemByUuidUseCase.InvokeAsync(bookId);
        if(bookResult.IsFailure)
            return DataResult<Book>.CreateFailure(bookResult.FailureResult.Error);
        
        var book = bookResult.SuccessResult.Data;
        
        try
        {
            await unitOfWork.BeginTransaction();

            book.Name = request.Name;
            book.Synopsis = request.Synopsis;
            book.PublishedDate = request.PublishedDate;
            book.NumberOfPages = request.NumberOfPages;
            book.PublisherId = publisher.Id;
            book.BooksAuthors = [];
            book.BooksCategories = [];
            var bookInserted = await unitOfWork.BookRepository.UpdateAsync(book);
            
            await DeleteAuthors(book.Id);
            await DeleteCategories(book.Id);
            await unitOfWork.Save();
            var bookAuthors = authors.Select(author => new BooksAuthors()
            {
                AuthorId = author.Id,
                Book = book
            });

            var bookCategories = categories.Select(category => new BooksCategories()
            {
                CategoryId = category.Id,
                Book = book
            });

            
            await unitOfWork.BookCategoriesRepository.InsertRangeAsync(bookCategories);
            await unitOfWork.BookAuthorsRepository.InsertRangeAsync(bookAuthors);
            await unitOfWork.Save();
            await unitOfWork.Commit();

            return DataResult<Book>.CreateSuccess(bookInserted);
        }
        catch (Exception ex)
        {
            await unitOfWork.Rollback();
            return DataResult<Book>.CreateFailure(ex.CreateExceptionResult());
        }
    }

    private async Task DeleteAuthors(uint bookId)
    {
        var spec = new BookAuthorSpecifications.GetByBookId(bookId);
        await unitOfWork.BookAuthorsRepository.DeleteBySpecAsync(spec);
    }
    private async Task DeleteCategories(uint bookId)
    {
        var spec = new BookCategoriesSpecifications.GetByBookId(bookId);
        await unitOfWork.BookCategoriesRepository.DeleteBySpecAsync(spec);
    }

}