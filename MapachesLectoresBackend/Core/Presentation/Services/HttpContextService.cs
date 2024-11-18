using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.Services;
using MapachesLectoresBackend.Users.Domain.Model.Enums;

namespace MapachesLectoresBackend.Core.Presentation.Specification;


public class HttpContextService : IHttpContextService
{
    private UuidVo? _userUuid;
    public UuidVo Uuid
    {
        get {
            if (_userUuid == null) throw new UnauthorizedAccessException();
            return _userUuid;
        }
        set {
            if(_userUuid != null) throw new ArgumentException(nameof(Uuid));

            _userUuid = value;
        }
    }

    private UserRoleEnum? _userRole;
    public UserRoleEnum UserRole {
        get {
            if (_userRole == null) throw new UnauthorizedAccessException();
            return _userRole!.Value;
        }
        set {
            if(_userRole != null) throw new ArgumentException("Error al recuperar el Rol del usuario",nameof(Uuid));

            _userRole = value;
        }
    }
}