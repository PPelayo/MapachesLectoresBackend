using MapachesLectoresBackend.Auth.Domain.Service;
using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.Services;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MapachesLectoresBackend.Auth.Presentation.Middleware;


public class AuthenticatedAttribute() : TypeFilterAttribute(typeof(AuthenticatedJwtFilter));

public class AuthenticatedJwtFilter(
    JwtService jwtService,
    IHttpContextService httpContextService
) : IAsyncActionFilter
{
    private const string TokenHeader = "Authorization";
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Verifica si el header contiene el token
        if (!context.HttpContext.Request.Headers.TryGetValue(TokenHeader, out var bearerToken) || string.IsNullOrEmpty(bearerToken))
        {
            // Si no existe el token o es nulo, retorna un 401 Unauthorized
            ReturnsUnauthorized(context);
            return;
        }
        var jwtToken = GetTokenFromHeader(bearerToken.ToString());
        
        // Aquí puedes agregar lógica adicional para validar el token si es necesario
        var validateTokenResult = jwtService.ValidateAccessToken(jwtToken);

        if (validateTokenResult.IsFailure)
        {
            ReturnsUnauthorized(context);
            return;
        }
        
        // Guardamos el token en el contexto actual
        httpContextService.UserUuid = new UserUuidVo(validateTokenResult.SuccessResult.Data.userId);
        httpContextService.UserRole = validateTokenResult.SuccessResult.Data.role;
        
        // Si el token es válido, continua con la ejecución de la acción
        await next();
    }
    
    private static void ReturnsUnauthorized(ActionExecutingContext context)
    {
        var response = BaseResponse.CreateError(StatusCodes.Status401Unauthorized, "El token no es válido");
        context.Result = new UnauthorizedObjectResult(response);
    }
    
    private string GetTokenFromHeader(string header) =>
        header.Split(" ")[1];
    
}