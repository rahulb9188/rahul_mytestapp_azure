using System;
using System.Collections.Generic;

namespace myapp_customerwebapp_azure.Infrastructure.Data;

public partial class CustomerlyCustomer
{
    public int CustomerId { get; set; }

    public int UserId { get; set; }

    public string CustomerName { get; set; } = null!;

    public virtual CustomerlyUser User { get; set; } = null!;
}
