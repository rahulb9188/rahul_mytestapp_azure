using myapp_customerwebapp_azure.Application.Models;
using myapp_customerwebapp_azure.Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myapp_customerwebapp_azure.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<TokenResponse?> LoginAsync(LoginRequest request);
        Task<bool> RegisterAsync(RegisterRequest request);
        Task<TokenResponse> RefreshTokenAsync(string accessToken, string refreshToken);
    }
}
