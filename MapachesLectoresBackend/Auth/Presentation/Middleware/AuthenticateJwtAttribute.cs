using System.Text.Json;
using MapachesLectoresBackend.Auth.Domain.Service;
using MapachesLectoresBackend.Core.Domain.Model.Vo;
using MapachesLectoresBackend.Core.Domain.Services;
using MapachesLectoresBackend.Core.Presentation.Dtos;

namespace MapachesLectoresBackend.Auth.Presentation.Middleware;

public class AuthenticatedMiddleware(
    RequestDelegate next
)
{
    private const string TokenHeader = "Authorization";

    public async Task InvokeAsync(HttpContext context, JwtService jwtService, IHttpContextService httpContextService)
    {
        var endpoint = context.GetEndpoint();

        // Comprueba si el endpoint tiene el atributo `[Authenticated]`
        if (endpoint?.Metadata.GetMetadata<AuthenticatedAttribute>() != null)
        {
            if (!context.Request.Headers.TryGetValue(TokenHeader, out var bearerToken) || string.IsNullOrEmpty(bearerToken))
            {
                ReturnsUnauthorized(context);
                return;
            }

            var jwtToken = GetTokenFromHeader(bearerToken.ToString());

            // Validar el token JWT
            var validateTokenResult = jwtService.ValidateAccessToken(jwtToken);

            if (validateTokenResult.IsFailure)
            {
                ReturnsUnauthorized(context);
                return;
            }

            // Almacenar datos del token en el contexto
            httpContextService.UserUuid = new UserUuidVo(validateTokenResult.SuccessResult.Data.userId);
            httpContextService.UserRole = validateTokenResult.SuccessResult.Data.role;
        }

        // Continuar con el pipeline
        await next(context);
    }

    private static void ReturnsUnauthorized(HttpContext context)
    {
        var response = BaseResponse.CreateError(StatusCodes.Status401Unauthorized, "El token no es válido");
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";

        context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private string GetTokenFromHeader(string header) =>
        header.Split(" ")[1];
}

// Atributo decorador para los endpoints o controladores
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthenticatedAttribute : Attribute
{
}