using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Books.Domain.Model.Dto;
using MapachesLectoresBackend.Books.Domain.Model.Error;
using MapachesLectoresBackend.Books.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.Extensions;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.UnitOfWork;

namespace MapachesLectoresBackend.Books.Domain.UseCase;

public class CreateAuthorUseCase(
    IGenericUnitOfWork<Author> authorUnitOfWork
) {

    public async Task<DataResult<Author>> InvokeAsync(CreateAuthorRequestDto request){

        var spec = new AuthorSpecifications.GetByName(request.Name)
        .And(new AuthorSpecifications.GetByLastName(request.LastName));

        var posibleAuthor = await authorUnitOfWork.Repository.GetFirstAsync(spec);

        if(posibleAuthor != null)
            return DataResult<Author>.CreateFailure(CreateAuthorErrors.AuthorAlreadyExists_400());

        try {

            var authorToCreate = new Author(){
                Name = request.Name,
                LastName = request.LastName
            };

            await authorUnitOfWork.BeginTransaction();
            var authorCreated = await authorUnitOfWork.Repository.InsertAsync(authorToCreate);
            await authorUnitOfWork.Save();
            await authorUnitOfWork.Commit();


            return DataResult<Author>.CreateSuccess(authorCreated);
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
            await authorUnitOfWork.Rollback();
            return DataResult<Author>.CreateFailure(ex.CreateExceptionResult());
        }
    }

}