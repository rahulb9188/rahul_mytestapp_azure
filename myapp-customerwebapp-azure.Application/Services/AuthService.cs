using Microsoft.AspNetCore.Identity;
using myapp_customerwebapp_azure.Application.Interfaces.Repositories;
using myapp_customerwebapp_azure.Application.Interfaces.Services;
using myapp_customerwebapp_azure.Application.Models;
using myapp_customerwebapp_azure.Application.Models.Request;
using myapp_customerwebapp_azure.Domain.Entities;
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
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest request)
        {
            // Simple example: lookup user by email (replace with real DB call)
            var customerly_user = await _userRepository.GetByEmailAsync(request.Email);

            // Validate user here
            if (customerly_user == null)
                return null;

            User user = new User
            {
                Username = customerly_user.Email,
            };

            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, customerly_user.PasswordHash, request.Password);
            LoginUser loginUser = new LoginUser(
                customerly_user.Email,
                customerly_user.FullName,
                customerly_user.Email
            );
            if (result == PasswordVerificationResult.Success)
            {
                var accessToken = _jwtTokenGenerator.GenerateAccessToken(customerly_user.Email);
                var refreshToken = _jwtTokenGenerator.GenerateRefreshToken(customerly_user.Email);
                return new TokenResponse(accessToken, refreshToken, loginUser);
            }
            return null;
            // Generate JWT token

        }
        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                return false;

            var user = new User
            {
                Username = request.Email,
            };

            var newUser = new Customerly_User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = request.Password,
                //Role = request.Role?? 1,
                Role = 1,
                CreatedAt = DateTime.UtcNow
            };
            newUser.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            await _userRepository.AddAsync(newUser);
            return true;
        }

    }

}
