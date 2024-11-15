using MapachesLectoresBackend.Books.Domain.Model;
using MapachesLectoresBackend.Core.Domain.Extensions;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Core.Domain.UnitOfWork;
using MapachesLectoresBackend.Reviews.Domain.Model;
using MapachesLectoresBackend.Reviews.Domain.Model.Dto;
using MapachesLectoresBackend.Reviews.Domain.Model.Error;
using MapachesLectoresBackend.Users.Domain.Model;

namespace MapachesLectoresBackend.Reviews.Domain.UseCase;

public class CreateReviewUseCase(
    IGenericUnitOfWork<Review> unitOfWorkReview,
    IRepository<Book> bookRepository,
    IRepository<User> userRepository
)
{
    public async Task<DataResult<Review>> InvokeAsync(CreateReviewDto reviewDto)
    {
        var validateErrors = await ValidateIfExistsSources(reviewDto.UserId, reviewDto.BookId);
        if(validateErrors != null)
            return DataResult<Review>.CreateFailure(validateErrors);
        
        
        var review = new Review()
        {
            Description = reviewDto.Description,
            GeneralRating = reviewDto.GeneralRating.Value,
            UserId = reviewDto.UserId.Value,
            BookId = reviewDto.BookId.Value,
            PublishDate = DateTime.UtcNow
        };

        var reviewInserted = await unitOfWorkReview.Repository.InsertAsync(review);
        try
        {
            await unitOfWorkReview.Save();
            
            return DataResult<Review>.CreateSuccess(reviewInserted);
        } catch (Exception e)
        {
            return DataResult<Review>.CreateFailure(e.CreateExceptionResult());
        }
    }

    private async Task<IError?> ValidateIfExistsSources(UserUuidVo userId, UserUuidVo bookId)
    {
        var bookSpec = new GetByUuidSpecification<Book>(bookId.Value);
        var book = await bookRepository.GetFirstAsync(bookSpec);
        if (book == null)
            return CreateReviewErrors.InvalidBookId_400();
        
        var userSpec = new GetByUuidSpecification<User>(userId.Value);
        var user = await userRepository.GetFirstAsync(userSpec);
        
        return user == null 
            ? CreateReviewErrors.InvalidUserId_400() 
            : null;
    }
}