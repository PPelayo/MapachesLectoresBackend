using MapachesLectoresBackend.Core.Domain.Model.ResultPattern;

namespace MapachesLectoresBackend.Core.Domain.Model.Errors;

public class ResourceAlreadyExists(string resource = "") 
    : BaseError($"The resource {resource} already exists", StatusCodes.Status400BadRequest);