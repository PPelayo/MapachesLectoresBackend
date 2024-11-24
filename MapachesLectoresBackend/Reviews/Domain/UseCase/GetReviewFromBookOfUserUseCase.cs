using MapachesLectoresBackend.Core.Domain.Model.Errors;
using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;
using MapachesLectoresBackend.Core.Domain.Repository;
using MapachesLectoresBackend.Reviews.Domain.Model;
using MapachesLectoresBackend.Reviews.Domain.Specification;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Reviews.Domain.UseCase;

public class GetReviewFromBookOfUserUseCase(
    IRepository<Review> reviewRepository
    )
{
    public async Task<DataResult<Review>> InvokeAsync(Guid bookId, Guid userId)
    {
        var spec = new ReviewSpecifications.GetByBookId(bookId)
            .And(new ReviewSpecifications.GetByUserId(userId))
            .And(new ReviewSpecifications.IncludesUser());

        var review = await reviewRepository.GetFirstAsync(spec);

        return review == null
            ? DataResult<Review>.CreateFailure(new NotFoundError())
            : DataResult<Review>.CreateSuccess(review);
    }   
}