using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Core.Domain.Model.Errors;

public class NotFoundError() : BaseError("Objeto no encontrado", StatusCodes.Status404NotFound);
