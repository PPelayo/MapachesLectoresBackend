using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Extensions;
using MapachesLectoresBackend.Core.Domain.Model.Errors;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.UnitOfWork;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class CreatePublisherUseCase(
    IGenericUnitOfWork<Publisher> publisherUnitOfWork
) {

    public async Task<DataResult<Publisher>> InvokeAsync(CreatePublisherRequestDto request){

        var spec = new PublisherSpecifications.GetByName(request.Name);

        var posiblePublisher = await publisherUnitOfWork.Repository.GetFirstAsync(spec);

        if(posiblePublisher != null)
            return DataResult<Publisher>.CreateFailure(new ResourceAlreadyExists(nameof(Publisher)));

        try{

            var publisherToInsert = new Publisher(){
                Name = request.Name
            };

            await publisherUnitOfWork.BeginTransaction();
            var publisherInserted = await publisherUnitOfWork.Repository.InsertAsync(publisherToInsert);
            await publisherUnitOfWork.Save();
            await publisherUnitOfWork.Commit();

            return DataResult<Publisher>.CreateSuccess(publisherInserted);
        } catch(Exception ex) {
            Console.WriteLine(ex.Message);
            await publisherUnitOfWork.Rollback();
            return DataResult<Publisher>.CreateFailure(ex.CreateExceptionResult());
        }

    }

}