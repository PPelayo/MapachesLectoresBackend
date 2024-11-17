using MapachesLectoresBackend.Core.Domain.Specification;
using MapachesLectoresBackend.Reviews.Domain.Model;

namespace MapachesLectoresBackend.Reviews.Domain.Specification;

public static class ReviewSpecifications
{
    public sealed class GetByBookId(Guid bookId) 
        : BaseSpecification<Review>(entity => entity.BookId == bookId.ToString());
    
    public sealed class GetByUserId(Guid userId) 
        : BaseSpecification<Review>(entity => entity.UserId == userId.ToString());

    public sealed class IncludesUser : BaseSpecification<Review>
    {
        public IncludesUser()
        {
            AddInclude(entity => entity.User);
        }
    }
}