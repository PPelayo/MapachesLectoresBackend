using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.Services;
using MapachesLectoresBackend.Users.Domain.Model.Enums;

namespace MapachesLectoresBackend.Core.Presentation.Specification;


public class httpContextService : IHttpContextService
{
    private UserUuidVo? _userUuid;
    public UserUuidVo UserUuid
    {
        get {
            if (_userUuid == null) throw new UnauthorizedAccessException();
            return _userUuid;
        }
        set {
            if(_userUuid != null) throw new ArgumentException(nameof(UserUuid));

            _userUuid = value;
        }
    }

    private UserRoleEnum? _userRole;
    public UserRoleEnum UserRole {
        get {
            if (_userUuid == null) throw new UnauthorizedAccessException();
            return _userRole!.Value;
        }
        set {
            if(_userUuid != null) throw new ArgumentException(nameof(UserUuid));

            _userRole = value;
        }
    }
}