using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace myapp_customerwebapp_azure.Application.Models
{
        // A record is an immutable reference type perfect for DTOs.
        public record TokenResponse(string AccessToken, string RefreshToken, LoginUser LoginUser);
    public record LoginUser(string Email, string Name, string UserName );
    // model for incoming login data
    public record LoginRequest(string Email, string Password);

    // Model for incoming refresh request
    public record RefreshTokenRequest(string RefreshToken);
}
