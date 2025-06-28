using System.Security.Claims;

namespace WebApi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetEmailFromToken(this ClaimsPrincipal user)
        {
            var emailClaim = user.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.Email ||
                c.Type == "email" ||
                c.Type == "emails");
            return emailClaim?.Value;
        }
    }
}