

using myapp_customerwebapp_azure.Application.Interfaces.Repositories;

namespace myapp_customerwebapp_azure.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task ExecuteInTransactionAsync(Func<Task> action);
    }
}
