using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Products.Api.Exceptions;

namespace Products.Api.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PermissionsAttribute : TypeFilterAttribute
{
    public PermissionsAttribute(UserPermissions permissions) : base(typeof(PermissionsFilter))
    {
        Arguments = new object[]
        {
            permissions
        };
    }
}

public class PermissionsFilter : IAuthorizationFilter
{
    public UserPermissions Access;

    public PermissionsFilter(UserPermissions access) => Access = access;
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity!.IsAuthenticated)
            throw new Exception("UNAUTHENTICATED_USER");
        
        var userAccess = (UserPermissions)uint.Parse(context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Permissions")!.Value);
        
        if (!IsPermitted(userAccess))
            throw new UnauthorizedAccessException();

    }
    private bool IsPermitted(UserPermissions userPermissions) =>
        (Access & userPermissions) == Access;
}