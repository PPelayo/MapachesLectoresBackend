using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Books.Domain.Model.Error;

public class GetBookError : BaseError
{
    private GetBookError(string msg, int statusCode) : base(msg, statusCode)
    {
    }
    
    public static GetBookError NotFound_404(uint id) => new GetBookError($"Book with id {id} not found", 404);
}