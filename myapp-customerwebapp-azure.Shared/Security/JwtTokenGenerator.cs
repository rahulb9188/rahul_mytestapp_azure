using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace myapp_customerwebapp_azure.Shared.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private const string SecretKey = "3f#8v!bZx9@Lp2&Nj7qRw6$YgTmD1FsUc";
        private const string Issuer = "https://mytestapp-a3byb0auhje7b5av.southeastasia-01.azurewebsites.net/";
        private const string Audience = "https://mytestapp-a3byb0auhje7b5av.southeastasia-01.azurewebsites.net/";
        // Access Token expires quickly (e.g., 15 minutes)
        private static readonly TimeSpan AccessTokenLifetime = TimeSpan.FromMinutes(15);
        // Refresh Token expires much later (e.g., 7 days)
        private static readonly TimeSpan RefreshTokenLifetime = TimeSpan.FromDays(7);

        private static readonly Dictionary<string, string> ValidRefreshTokens = new();

        public string GenerateAccessToken(string username,string role)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name,username),
            new Claim(ClaimTypes.Role,role),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(AccessTokenLifetime),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken(string username)
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var refreshToken = Convert.ToBase64String(randomNumber);

            // In a real app: Store this token, its expiry, and the associated userId in your DB.
            // Mock storage update (replace this in production)
            ValidRefreshTokens[username] = refreshToken;

            return refreshToken;
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            // In a real app: Check if the refresh token exists in the database and is not expired.
            return ValidRefreshTokens.ContainsValue(refreshToken);
        }

    }
}
