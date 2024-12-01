﻿using MapachesLectoresBackend.Auth.Domain.Model.Vo;
using MapachesLectoresBackend.Auth.Domain.UseCase;
using MapachesLectoresBackend.Auth.Presentation.Dto;
using MapachesLectoresBackend.Auth.Presentation.Mapper;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace MapachesLectoresBackend.Auth.Presentation.Controller;


[ApiController]
[Route("[controller]")]
public class AuthController(
    LoginUseCase loginUseCase,
    RegisterUserUseCase registerUserUseCase,
    RefreshTokenUseCase refreshTokenUseCase
) : ControllerBase
{

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await loginUseCase.InvokeAsync(request.Email, request.Password);

        return result.ActionResultHanlder(
            wrapper => Ok(BaseResponse.CreateSuccess(StatusCodes.Status200OK, wrapper.ToResponseDto())),
            error => error.ActionResult        
        );
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerRequest) {
        var result = await registerUserUseCase.InvokeAsync(registerRequest.Email, registerRequest.Name, registerRequest.Password);

        return result.ActionResultHanlder(
            registerWrapper => Created("", BaseResponse.CreateSuccess(StatusCodes.Status201Created, registerWrapper.ToResponseDto())),
            error => error.ActionResult
        );

    }

    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        var result = await refreshTokenUseCase.InvokeAsync(new JwtVo(request.RefreshToken));
        return result.ActionResultHanlder(
            jwt => Ok(BaseResponse.CreateSuccess(StatusCodes.Status200OK, jwt.Value)),
            error => error.ActionResult
        );
    }

}