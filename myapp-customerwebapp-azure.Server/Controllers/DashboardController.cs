using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myapp_customerwebapp_azure.Models;

namespace myapp_customerwebapp_azure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        [HttpGet("loadCustomers")]
        public List<Customer> Load()
        {
            List<Customer> customers= new List<Customer>
            {
                new Customer { Name = "Alice", Product = "Laptop", Order = 1, Status = true },
                new Customer { Name = "Bob", Product = "Smartphone", Order = 2, Status = false },
                new Customer { Name = "Charlie", Product = "Tablet", Order = 3, Status = true }
            };

            return customers;
        }
    }
}