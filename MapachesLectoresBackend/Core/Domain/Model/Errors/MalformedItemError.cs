using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Core.Domain.Model.Errors
{
    public class MalformedItemError(string field) : BaseError($"El campo {field} no es válido", StatusCodes.Status400BadRequest)
    {
    }
}
