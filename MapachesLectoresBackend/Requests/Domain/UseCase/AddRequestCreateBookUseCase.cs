using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Core.Domain.Model.Errors;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.UseCase;
using MapachesLectoresBackend.Requests.Domain.Model;

namespace MapachesLectoresBackend.Requests.Domain.UseCase
{
    public class AddRequestCreateBookUseCase(
        IRepository<RequestCreateBook> repository,
        IRepository<Publisher> publisherRepository     
    )
    {
        public async Task<DataResult<RequestCreateBook>> InvokeAsync(CreateBookRequestDto createBookRequestDto)
        {
            var publisher = await publisherRepository.GetByUuidAsync(createBookRequestDto.PublisherId.ToString());

            if (publisher == null)
                return DataResult<RequestCreateBook>.CreateFailure(new NotFoundError());

            var request = new RequestCreateBook()
            {
                Name = createBookRequestDto.Name,
                Synopsis = createBookRequestDto.Synopsis,
                NumberOfPages = createBookRequestDto.NumberOfPages,
                PublishedDate = createBookRequestDto.PublishedDate,
                PublisherId = publisher.Id,
                CoverUrl = ""
            };

            var insertedRequest = await repository.InsertAsync(request);

            return DataResult<RequestCreateBook>.CreateSuccess(insertedRequest);
        }
    }
}
