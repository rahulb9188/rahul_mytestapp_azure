using myapp_customerwebapp_azure.Application.Interfaces.Repositories;
using myapp_customerwebapp_azure.Application.Interfaces.Services;
using myapp_customerwebapp_azure.Application.Models.Request;
using myapp_customerwebapp_azure.Shared.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myapp_customerwebapp_azure.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string?> LoginAsync(LoginRequest request)
        {
            //// Simple example: lookup user by email (replace with real DB call)
            //var user = await _userRepository.GetByEmailAsync(request.Email);

            //// Validate password here (hash check, etc)
            //if (user == null || user.PasswordHash != request.Password)
            //    return null;

            //// Generate JWT token
            //return _jwtTokenGenerator.GenerateToken(user.Email);

            return "";
        }
    }
}
