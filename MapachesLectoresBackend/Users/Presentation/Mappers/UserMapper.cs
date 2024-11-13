using MapachesLectoresBackend.Core.Domain.Utils;
using MapachesLectoresBackend.Users.Domain.Model;
using MapachesLectoresBackend.Users.Domain.Model.Enums;
using MapachesLectoresBackend.Users.Presentation.Dtos;

namespace MapachesLectoresBackend.Users.Presentation.Mappers;

public static class UserMapper
{

    public static UserResponseDto ToResponseDto(this User user){
        var roleString = Enum.GetName((UserRoleEnum) user.Role);
        return new UserResponseDto(user.Name, user.UserName, roleString!);
    }
    public static User ToUser(this CreateUserDto createUserDto)
    {
        return new User
        {
            Name = createUserDto.Name,
            UserName = createUserDto.UserName,
            Password = createUserDto.Password,
            Role = (int)UserRoleEnum.Regular
        };
    }
}