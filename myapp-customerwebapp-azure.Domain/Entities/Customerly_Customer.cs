using System;
using System.Collections.Generic;

namespace myapp_customerwebapp_azure.Infrastructure;

public partial class Customerly_Customer
{
    public int CustomerId { get; set; }

    public int UserId { get; set; }

    public string CustomerName { get; set; } = null!;

    public virtual ICollection<Customerly_Order> Customerly_Orders { get; set; } = new List<Customerly_Order>();
}
