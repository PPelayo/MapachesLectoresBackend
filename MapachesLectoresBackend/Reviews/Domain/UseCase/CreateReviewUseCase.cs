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
using MapachesLectoresBackend.Reviews.Domain.Specification;
using MapachesLectoresBackend.Users.Domain.Model;
using Microsoft.IdentityModel.Tokens;

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
        
        var reviewExists = await ValidateIfExistsReview(reviewDto.UserId, reviewDto.BookId);
        if(reviewExists != null)
            return DataResult<Review>.CreateFailure(reviewExists);
        
        var review = new Review()
        {
            Description = reviewDto.Description,
            GeneralRating = reviewDto.GeneralRating.Value,
            UserId = reviewDto.UserId.Value.ToString(),
            BookId = reviewDto.BookId.Value.ToString(),
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

    private async Task<IError?> ValidateIfExistsSources(UuidVo id, UuidVo bookId)
    {
        var bookSpec = new GetByUuidSpecification<Book>(bookId.Value);
        var book = await bookRepository.GetFirstAsync(bookSpec);
        if (book == null)
            return CreateReviewErrors.InvalidBookId_404();
        
        var userSpec = new GetByUuidSpecification<User>(id.Value);
        var user = await userRepository.GetFirstAsync(userSpec);
        
        return user == null 
            ? CreateReviewErrors.InvalidUserId_404() 
            : null;
    }

    private async Task<IError?> ValidateIfExistsReview(UuidVo userId, UuidVo bookId)
    {
        var spec = new ReviewSpecifications.GetByBookId(bookId.Value)
            .And(new ReviewSpecifications.GetByUserId(userId.Value));

        var review = await unitOfWorkReview.Repository.GetFirstAsync(spec);

        return review == null
            ? null
            : CreateReviewErrors.UserAlreadyCommented_403();

    }
}