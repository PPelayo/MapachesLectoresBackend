using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Auth.Domain.Model.Errors;

public class UnauthorizedError() : BaseError("Error al intentar identificar al usuario", StatusCodes.Status401Unauthorized);