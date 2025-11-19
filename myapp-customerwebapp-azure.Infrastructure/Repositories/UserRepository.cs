using Microsoft.EntityFrameworkCore;
using myapp_customerwebapp_azure.Application.Interfaces.Repositories;
using myapp_customerwebapp_azure.Domain.Entities;
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

        public async Task AddAsync(Customerly_User user)
        {
            await _context.Customerly_Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }

}
