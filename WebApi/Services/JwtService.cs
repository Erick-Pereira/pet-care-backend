using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entities;
using Microsoft.IdentityModel.Tokens;

namespace web_api.Services
{
    public class JwtService
    {
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expireMinutes;

        public JwtService(IConfiguration config)
        {
            _secret = Environment.GetEnvironmentVariable("JWT_KEY") ??
                     throw new InvalidOperationException("JWT_KEY is not configured");
            _issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ??
                     throw new InvalidOperationException("JWT_ISSUER is not configured");
            _audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ??
                     throw new InvalidOperationException("JWT_AUDIENCE is not configured");
            _expireMinutes = 60; // Default to 60 minutes if not specified
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("PermissionLevel", user.PermissionLevel)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_expireMinutes),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}