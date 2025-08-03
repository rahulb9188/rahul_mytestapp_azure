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
        Task<string?> LoginAsync(LoginRequest request);
    }
}
