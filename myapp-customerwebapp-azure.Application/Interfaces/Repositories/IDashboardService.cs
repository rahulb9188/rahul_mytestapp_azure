using myapp_customerwebapp_azure.Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myapp_customerwebapp_azure.Application.Interfaces.Repositories
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardAsync();
    }
}
