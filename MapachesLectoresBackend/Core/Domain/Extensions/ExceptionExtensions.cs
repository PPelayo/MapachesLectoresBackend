using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Core.Domain.Extensions;

public static class ExceptionExtensions
{
    public static IError CreateExceptionResult(this Exception ex)
    {
        return new BaseError($"Internal Server Error: {ex.Message}", StatusCodes.Status500InternalServerError);
    }
}