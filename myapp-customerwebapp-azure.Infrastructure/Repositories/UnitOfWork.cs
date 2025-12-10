using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using myapp_customerwebapp_azure.Application.Interfaces;
using myapp_customerwebapp_azure.Application.Interfaces.Repositories;
using myapp_customerwebapp_azure.Application.Models;
using myapp_customerwebapp_azure.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myapp_customerwebapp_azure.Infrastructure.Repositories
{
    public class UnitOfWork:IUnitOfWork, IDisposable
    {
        private readonly CustomerlyDbContext _context;
        private IDbContextTransaction? _transaction;

        public IUserRepository Users { get; }

        public UnitOfWork(CustomerlyDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task BeginTransactionAsync()
        {
             await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _transaction!.CommitAsync();
            await _transaction.DisposeAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction!.RollbackAsync();
            await _transaction.DisposeAsync();
        }

        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    await action();
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }
    }
}
