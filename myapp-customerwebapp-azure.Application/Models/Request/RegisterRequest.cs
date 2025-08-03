using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myapp_customerwebapp_azure.Application.Models.Request
{
    public class RegisterRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; } // "Customer" or "Staff"
        public string RequestedRole { get; set; } // Only for staff: "SalesRep", "Support", etc.
    }
}
