using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.Model.Error;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Books.Domain.UnitOfWork;
using MapachesLectoresBackend.Core.Domain.Model.Errors;
using MapachesLectoresBackend.Core.Domain.Model.Pagination;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.UseCase;

namespace MapachesLectoresBackend.Books.Domain.Service;

public class BookValidationService(
    GetItemByUuidUseCase<Publisher> getPublisherByUuidUseCase,
    IRepository<Book> bookRepository,
    IRepository<Author> authorRepository,
    IRepository<Category> categoryRepository
)
{
    private const int MaxAuthors = 1000;
    private const int MaxCategories = 1000;

    public async Task<DataResult<(Publisher, IEnumerable<Author>, IEnumerable<Category>)>> ValidateBookRequestAsync(CreateBookRequestDto request)
    {
        var spec = new BookSpecifications.GetByName(request.Name);

        var posibleBook = await bookRepository.GetFirstAsync(spec);
        
        if(posibleBook != null)
            return DataResult<(Publisher, IEnumerable<Author>, IEnumerable<Category>)>.CreateFailure(new ResourceAlreadyExists(nameof(Book)));

        var publisherResult = await getPublisherByUuidUseCase.InvokeAsync(request.PublisherId);
        if (publisherResult.IsFailure)
            return DataResult<(Publisher, IEnumerable<Author>, IEnumerable<Category>)>.CreateFailure(CreateBookErrors.PublisherNotFound_400());

        var publisher = publisherResult.SuccessResult.Data;

        if (!request.Authors.Any())
            return DataResult<(Publisher, IEnumerable<Author>, IEnumerable<Category>)>.CreateFailure(CreateBookErrors.AuthorNotFound_400());

        if (request.Authors.Count > MaxAuthors)
            return DataResult<(Publisher, IEnumerable<Author>, IEnumerable<Category>)>.CreateFailure(CreateBookErrors.ToManyAuthors_400());

        if (!request.Categories.Any())
            return DataResult<(Publisher, IEnumerable<Author>, IEnumerable<Category>)>.CreateFailure(CreateBookErrors.CategoriesNotFound_400());

        if (request.Categories.Count > MaxCategories)
            return DataResult<(Publisher, IEnumerable<Author>, IEnumerable<Category>)>.CreateFailure(CreateBookErrors.ToManyCategories_400());

        var authors = (await GetAuthors(request.Authors)).ToList();
        if (authors.Count == 0)
            return DataResult<(Publisher, IEnumerable<Author>, IEnumerable<Category>)>.CreateFailure(CreateBookErrors.AuthorNotFound_400());

        var categories = (await GetCategories(request.Categories)).ToList();
        if (categories.Count == 0)
            return DataResult<(Publisher, IEnumerable<Author>, IEnumerable<Category>)>.CreateFailure(CreateBookErrors.CategoriesNotFound_400());

        return DataResult<(Publisher, IEnumerable<Author>, IEnumerable<Category>)>.CreateSuccess((publisher, authors, categories));
    }

    private async Task<IEnumerable<Author>> GetAuthors(ISet<Guid> authorIds)
    {
        var spec = new GetByUuidsSpecification<Author>(authorIds);
        var pagination = new UserPagination
        {
            Limit = MaxAuthors
        };
        var authors = await authorRepository.GetAsync(pagination, spec);
        return authors;
    }

    private async Task<IEnumerable<Category>> GetCategories(ISet<Guid> categoriesIds)
    {
        var spec = new GetByUuidsSpecification<Category>(categoriesIds);
        var pagination = new UserPagination
        {
            Limit = MaxCategories
        };
        var categories = await categoryRepository.GetAsync(pagination, spec);
        return categories;
    }
}