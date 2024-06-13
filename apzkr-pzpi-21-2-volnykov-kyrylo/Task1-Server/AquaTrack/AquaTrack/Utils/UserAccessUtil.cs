using AquaTrack.Models;
using Microsoft.AspNetCore.Http;

namespace AquaTrack.Utils
{
    public static class UserAccessUtil
    {
        public static Role? GetCurrentUserRole(IHttpContextAccessor httpContextAccessor)
        {
            var roleFromCookie = httpContextAccessor.HttpContext.Request.Cookies["role"];
            if (string.IsNullOrEmpty(roleFromCookie))
            {
                return null;
            }
            if (Enum.TryParse(roleFromCookie, out Role role))
            {
                return role;
            }
            return null;
        }
    }
}
