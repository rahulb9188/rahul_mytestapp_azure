using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myapp_customerwebapp_azure.Application.Interfaces.Repositories;

namespace myapp_customerwebapp_azure.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [HttpGet("loadCustomers")]
        public async Task<IActionResult> GetDashboard()
        {
            var dto = await _dashboardService.GetDashboardAsync();
            return Ok(dto);
        }
    }
}