using System.Security.Claims;
using CleanRazor.Security;

namespace CleanRazor.Web.Services
{
    public sealed class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
    {
        public string? UserId => IsAuthenticated ? httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) : null;

        public string? UserName => IsAuthenticated ? httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.Name) : null;

        public bool IsAuthenticated => httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
