using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.Model.Error;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Books.Domain.UnitOfWork;
using MapachesLectoresBackend.Core.Domain.Extensions;
using MapachesLectoresBackend.Core.Domain.Model.Errors;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.UseCase;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class CreateBookUseCase(
    GetItemByUuidUseCase<Publisher> getPublisherByUuidUseCase,
    ICreateBookUnitOfWork unitOfWork
)
{
    
    private const int MaxAuthors = 1000;
    private const int MaxCategories = 1000;
    
    public async Task<DataResult<Book>> InvokeAsync(CreateBookRequestDto request)
    {
        var spec = new BookSpecifications.GetByName(request.Name);

        var posibleBook = await unitOfWork.BookRepository.GetFirstAsync(spec);

        if (posibleBook != null)
            return DataResult<Book>.CreateFailure(new ResourceAlreadyExists(nameof(Book)));

        var publisherResult = await getPublisherByUuidUseCase.InvokeAsync(request.PublisherId);
        if (publisherResult.IsFailure)
            return DataResult<Book>.CreateFailure(CreateBookErrors.PublisherNotFound_400());
        var publisher = publisherResult.SuccessResult.Data;
        
        if(!request.Authors.Any())
            return DataResult<Book>.CreateFailure(CreateBookErrors.AuthorNotFound_400());
        
        if(request.Authors.Count > MaxAuthors)
            return DataResult<Book>.CreateFailure(CreateBookErrors.ToManyAuthors_400());

        if (!request.Categories.Any())
            return DataResult<Book>.CreateFailure(CreateBookErrors.CategoriesNotFound_400());
        
        if(request.Categories.Count > MaxCategories)
            return DataResult<Book>.CreateFailure(CreateBookErrors.ToManyCategories_400());

        var authors = (await GetAuthors(request.Authors)).ToList();
        if(authors.Count == 0)
            return DataResult<Book>.CreateFailure(CreateBookErrors.AuthorNotFound_400());
        
        var categories = (await GetCategories(request.Categories)).ToList();
        if(categories.Count == 0)
            return DataResult<Book>.CreateFailure(CreateBookErrors.CategoriesNotFound_400());
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

    private async Task<IEnumerable<Author>> GetAuthors(ISet<Guid> authorIds)
    {
        var spec = new GetByUuidsSpecification<Author>(authorIds);
        var pagination = new UserPagination
        {
            Limit = MaxAuthors
        };
        var authors = await unitOfWork.AuthorRepository.GetAsync(pagination, spec);
        return authors;
    }

    private async Task<IEnumerable<Category>> GetCategories(ISet<Guid> categoriesIds)
    {
        var spec = new GetByUuidsSpecification<Category>(categoriesIds);
        var pagination = new UserPagination
        {
            Limit = MaxCategories
        };
        var categories = await unitOfWork.CategoriesRepository.GetAsync(pagination, spec);
        return categories;
    }
    
}