using Microsoft.EntityFrameworkCore;
using myapp_customerwebapp_azure.Application.Interfaces.Repositories;
using myapp_customerwebapp_azure.Application.Models.Response;
using myapp_customerwebapp_azure.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myapp_customerwebapp_azure.Infrastructure.Repositories
{
    public class DashboardService : IDashboardService
    {
        private readonly CustomerlyDbContext _context;

        public DashboardService(CustomerlyDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboardAsync()
        {
            var totalCustomers = await _context.Customerly_Customers.CountAsync();
            var totalOrders = await _context.Customerly_Orders.CountAsync();
            var totalProducts = await _context.Customerly_Products.CountAsync();

            var recentOrders = await _context.Customerly_Orders
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .Select(o => new RecentOrderDto
                {
                    CustomerName = o.Customer.CustomerName,
                    ProductName = o.Product.Name,
                    OrderedAt = o.OrderDate,
                    Status = o.Status.StatusName
                })
                .ToListAsync();

            var orderStatusSummary = await _context.Customerly_Orders
                .GroupBy(o => o.StatusId)
                .Select(g => new OrderStatusSummaryDto
                {
                    Status = "g.Key",
                    Count = g.Count()
                })
                .ToListAsync();

            return new DashboardDto
            {
                TotalCustomers = totalCustomers,
                TotalOrders = totalOrders,
                TotalProducts = totalProducts,
                RecentOrders = recentOrders,
                OrderStatusSummary = orderStatusSummary
            };
        }
    }

}

