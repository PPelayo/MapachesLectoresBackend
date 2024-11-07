using MapachesLectoresBackend.Core.Domain.Utils;
using MapachesLectoresBackend.Users.Domain.Model;
using MapachesLectoresBackend.Users.Domain.Model.Enums;
using MapachesLectoresBackend.Users.Presentation.Dtos;

namespace MapachesLectoresBackend.Users.Presentation.Mappers;

public static class UserMapper
{
    public static User ToUser(this CreateUserDto createUserDto)
    {
        return new User
        {
            Name = createUserDto.Name,
            UserName = createUserDto.UserName,
            Password = createUserDto.Password,
            Role = UserRoleEnum.Regular
        };
    }
}