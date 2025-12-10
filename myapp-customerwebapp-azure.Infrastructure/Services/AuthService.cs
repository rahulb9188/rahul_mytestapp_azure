using Microsoft.AspNetCore.Identity;
using myapp_customerwebapp_azure.Application.Interfaces;
using myapp_customerwebapp_azure.Application.Interfaces.Repositories;
using myapp_customerwebapp_azure.Application.Interfaces.Services;
using myapp_customerwebapp_azure.Application.Models;
using myapp_customerwebapp_azure.Application.Models.Request;
using myapp_customerwebapp_azure.Infrastructure;
using myapp_customerwebapp_azure.Shared.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace myapp_customerwebapp_azure.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<TokenResponse> LoginAsync(LoginRequest request)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (user == null)
                return null;

            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(new User(), user.PasswordHash, request.Password);
            if (result != PasswordVerificationResult.Success)
            {
                return null;
            }
            //LoginUser loginUser = new LoginUser(user.Email, user.FullName, user.Email);

            var accessToken = _jwtTokenGenerator.GenerateAccessToken(user.Email, "user");
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken(user.Email);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = DateTime.UtcNow.AddDays(7);
            _unitOfWork.Users.UpdateRefreshTokenAsync(user);
            return new TokenResponse(accessToken, refreshToken);
        }
        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            try
            {
                await _unitOfWork.ExecuteInTransactionAsync(async () =>
                {
                    var existingUser = await _unitOfWork.Users.GetByEmailAsync(request.Email);
                    if (existingUser != null)
                        throw new InvalidOperationException("User already exists");

                    var user = new User
                    {
                        Username = request.Email,
                    };

                    var newUser = new Customerly_User
                    {
                        UserId = Guid.NewGuid().ToString(),
                        FullName = request.FullName,
                        Email = request.Email,
                        PasswordHash = request.Password,
                        Role = 1,
                        CreatedAt = DateTime.UtcNow
                    };
                    newUser.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

                    await _unitOfWork.Users.AddAsync(newUser);
                    var role = new Customerly_UserRole
                    {
                        RoleID = 1,
                        UserID = newUser.UserId
                    };
                    await _unitOfWork.Users.AddUserRoleAsync(role);
                });
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<TokenResponse> RefreshTokenAsync(string accessToken, string refreshToken)
        {
            try
            {
                string newAccessToken = string.Empty;
                string newRefreshToken = string.Empty;
                await _unitOfWork.ExecuteInTransactionAsync(async () =>
                {
                    var user = await _unitOfWork.Users.GetByRefreshTokenAsync(refreshToken);

                    if (user == null || user.RefreshTokenExpires < DateTime.UtcNow)
                        throw new InvalidOperationException("User is unauthorized"); ;

                    // Generate new access token
                    newAccessToken = _jwtTokenGenerator.GenerateAccessToken(user.Email, "user");

                    // Generate new refresh token
                    var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken(user.Email);

                    user.RefreshToken = newRefreshToken;
                    user.RefreshTokenExpires = DateTime.UtcNow.AddDays(7);

                    await _unitOfWork.Users.UpdateRefreshTokenAsync(user);
                });
                return new TokenResponse(newAccessToken, newRefreshToken);
            }
            catch (Exception e)
            {
                return new TokenResponse(null, null);
            }
        }
    }

}
