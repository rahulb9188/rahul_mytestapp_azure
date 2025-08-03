using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace myapp_customerwebapp_azure.Shared.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public string GenerateToken(string username)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3f#8v!bZx9@Lp2&Nj7qRw6$YgTmD1FsUc"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://mytestapp-a3byb0auhje7b5av.southeastasia-01.azurewebsites.net/",
                audience: "https://mytestapp-a3byb0auhje7b5av.southeastasia-01.azurewebsites.net/",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
