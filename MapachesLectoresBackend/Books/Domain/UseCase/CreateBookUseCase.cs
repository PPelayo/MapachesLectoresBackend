using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.Service;
using MapachesLectoresBackend.Books.Domain.UnitOfWork;
using MapachesLectoresBackend.Core.Domain.Extensions;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class CreateBookUseCase(
    BookValidationService bookValidationService,
    ICreateBookUnitOfWork unitOfWork
)
{
    
    public async Task<DataResult<Book>> InvokeAsync(CreateBookRequestDto request)
    {
        var validationResult = await bookValidationService.ValidateBookRequestAsync(request);
        
        if(validationResult.IsFailure)
            return DataResult<Book>.CreateFailure(validationResult.FailureResult.Error);

        var (_, publisher, authors, categories) = validationResult.SuccessResult.Data;
        
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
            
            
            var bookAuthors = authors.Select(author => new BooksAuthors()
            {
                AuthorId = author.Id,
                Book = bookToInsert
            });

            var bookCategories = categories.Select(category => new BooksCategories()
            {
                CategoryId = category.Id,
                Book = bookToInsert
            });

            
            await unitOfWork.BeginTransaction();
            var bookInserted = await unitOfWork.BookRepository.InsertAsync(bookToInsert);
            await unitOfWork.BookCategoriesRepository.InsertRangeAsync(bookCategories);
            await unitOfWork.BookAuthorsRepository.InsertRangeAsync(bookAuthors);
            await unitOfWork.Save();
            await unitOfWork.Commit();

            return DataResult<Book>.CreateSuccess(bookInserted);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await unitOfWork.Rollback();
            return DataResult<Book>.CreateFailure(ex.CreateExceptionResult());
        }
    }
    
}