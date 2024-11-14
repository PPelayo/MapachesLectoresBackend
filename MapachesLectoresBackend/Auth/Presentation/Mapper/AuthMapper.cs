using MapachesLectoresBackend.Auth.Domain.Model.Wrapper;
using MapachesLectoresBackend.Auth.Presentation.Dto;
using MapachesLectoresBackend.Users.Presentation.Mappers;

namespace MapachesLectoresBackend.Auth.Presentation.Mapper;

public static class AuthMapper {

    public static RegisterUserResponseDto ToResponseDto(this RegisterWrapperResponse wresponse){
        return new RegisterUserResponseDto(wresponse.User.ToResponseDto(),wresponse.Tokens);
    }

    public static LoginResponseDto ToResponseDto(this LoginWrapperResponse wrapperResponse)
        => new LoginResponseDto(wrapperResponse.User.ToResponseDto(), wrapperResponse.Tokens);
}