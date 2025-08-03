using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myapp_customerwebapp_azure.Application.Models.Response
{
    public class OrderStatusSummaryDto
    {
        public string Status { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
