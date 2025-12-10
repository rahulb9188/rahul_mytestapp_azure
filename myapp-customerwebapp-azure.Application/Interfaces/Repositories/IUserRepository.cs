using myapp_customerwebapp_azure.Infrastructure;

namespace myapp_customerwebapp_azure.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<Customerly_User?> GetByEmailAsync(string email);
        Task<Customerly_User?> GetByRefreshTokenAsync(string refreshToken);
        Task AddAsync(Customerly_User user);
        Task AddUserRoleAsync(Customerly_UserRole userRole);
        Task UpdateRefreshTokenAsync(Customerly_User user);
    }
}
