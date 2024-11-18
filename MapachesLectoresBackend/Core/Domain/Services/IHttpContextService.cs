using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Users.Domain.Model.Enums;

namespace MapachesLectoresBackend.Core.Domain.Services;


public interface IHttpContextService {

    public UuidVo Uuid { get; set; }
    public UserRoleEnum UserRole { get; set; }

}