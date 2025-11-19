using myapp_customerwebapp_azure.Domain.Entities;

namespace myapp_customerwebapp_azure.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<Customerly_User?> GetByEmailAsync(string email);
        Task AddAsync(Customerly_User user);
    }
}
