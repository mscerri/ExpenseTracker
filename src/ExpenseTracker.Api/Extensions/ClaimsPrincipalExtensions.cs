using IdentityModel;
using System;
using System.Linq;
using System.Security.Claims;

namespace ExpenseTracker.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserGuid(this ClaimsPrincipal claimsPrincipal)
        {
            var subject = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject)?.Value;

            if (!string.IsNullOrEmpty(subject) && Guid.TryParse(subject, out var id))
            {
                return id;
            }

            return null;
        }
    }
}
