using Microsoft.EntityFrameworkCore;
using myapp_customerwebapp_azure.Application.Interfaces.Repositories;
using myapp_customerwebapp_azure.Infrastructure;
using myapp_customerwebapp_azure.Infrastructure.Data;

namespace myapp_customerwebapp_azure.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly CustomerlyDbContext _context;
        public UserRepository(CustomerlyDbContext context)
        {
            _context = context;
        }
        public async Task<Customerly_User?> GetByEmailAsync(string email)
        {
            return await _context.Customerly_Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<Customerly_User?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Customerly_Users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
        public async Task AddAsync(Customerly_User user)
        {
            await _context.Customerly_Users.AddAsync(user);
        }
        public async Task AddUserRoleAsync(Customerly_UserRole userRole)
        {
            await _context.Customerly_UserRoles.AddAsync(userRole);
        }
        public async Task UpdateRefreshTokenAsync(Customerly_User user)
        {
            _context.Customerly_Users.Update(user);
        }

    }

}
