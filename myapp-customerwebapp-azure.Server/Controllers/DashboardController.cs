using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace myapp_customerwebapp_azure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        [HttpGet("loadCustomers")]
        public async Task<IActionResult> GetDashboard()
        {
            var dto = await _dashboardService.GetDashboardAsync();
            return Ok(dto);
        }
    }
}