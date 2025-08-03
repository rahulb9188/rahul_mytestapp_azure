using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myapp_customerwebapp_azure.Application.Models.Response
{
    public class DashboardDto
    {
        public int TotalCustomers { get; set; }
        public int TotalOrders { get; set; }
        public int TotalProducts { get; set; }

        public List<RecentOrderDto> RecentOrders { get; set; } = new();
        public List<OrderStatusSummaryDto> OrderStatusSummary { get; set; } = new();
    }
}
