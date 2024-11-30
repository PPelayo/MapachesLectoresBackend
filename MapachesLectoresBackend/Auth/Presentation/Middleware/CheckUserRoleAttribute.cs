using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MapachesLectoresBackend.Core.Domain.Model.Errors;
using MapachesLectoresBackend.Core.Domain.Services;
using MapachesLectoresBackend.Core.Presentation.Dtos;
using MapachesLectoresBackend.Users.Domain.Model.Enums;

namespace MapachesLectoresBackend.Auth.Presentation.Middleware;

/// <summary>
/// Atributo para verificar el rol del usuario.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CheckUserRoleAttribute : TypeFilterAttribute
{
    public CheckUserRoleAttribute(params UserRoleEnum[] roles) : base(typeof(CheckUserRoleFilter))
    {
        Arguments = new object[] { roles };
    }
}

/// <summary>
/// Filtro para verificar el rol del usuario.
/// </summary>
public class CheckUserRoleFilter : IAsyncActionFilter
{
    private readonly IHttpContextService _httpContextService;
    private readonly UserRoleEnum[] _requiredRoles;

    public CheckUserRoleFilter(IHttpContextService httpContextService, UserRoleEnum[] requiredRoles)
    {
        _httpContextService = httpContextService ?? throw new ArgumentNullException(nameof(httpContextService));
        _requiredRoles = requiredRoles;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userRole = _httpContextService.UserRole;

        if (!_requiredRoles.Contains(userRole))
        {
            // Si el usuario no tiene el rol requerido, retorna un 403 Forbidden
            context.Result = new ObjectResult(BaseResponse.CreateError(403, "No tienes acceso para esta acción")) { StatusCode = 403 };
            return;
        }

        // Si el rol coincide, continúa con la ejecución de la acción
        await next();
    }
}