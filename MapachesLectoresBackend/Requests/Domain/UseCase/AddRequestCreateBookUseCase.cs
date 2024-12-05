using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.Service;
using MapachesLectoresBackend.Core.Domain.Model.Errors;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.UseCase;
using MapachesLectoresBackend.Requests.Domain.Model;

namespace MapachesLectoresBackend.Requests.Domain.UseCase
{
    public class AddRequestCreateBookUseCase(
        IRepository<RequestCreateBook> repository,
        BookValidationService bookValidationService
    )
    {
        public async Task<DataResult<RequestCreateBook>> InvokeAsync(CreateBookRequestDto createBookRequestDto)
        {
            var validationResult = await bookValidationService.ValidateBookRequestAsync(createBookRequestDto);

            if (validationResult.IsFailure)
                return DataResult<RequestCreateBook>.CreateFailure(validationResult.FailureResult.Error);

            var (_, publisher, authors, categories) = validationResult.SuccessResult.Data;

            var request = new RequestCreateBook()
            {
                Id = Guid.NewGuid(),
                Name = createBookRequestDto.Name,
                Synopsis = createBookRequestDto.Synopsis,
                NumberOfPages = createBookRequestDto.NumberOfPages,
                PublishedDate = createBookRequestDto.PublishedDate,
                PublisherId = publisher.Id,
                CoverUrl = "",
                AuthorsIds = authors.Select(author => Guid.Parse(author.ItemUuid)).ToList(),
                CategoriesIds = categories.Select(category => Guid.Parse(category.ItemUuid)).ToList(),
            };

            var insertedRequest = await repository.InsertAsync(request);

            return DataResult<RequestCreateBook>.CreateSuccess(insertedRequest);
        }
    }
}
