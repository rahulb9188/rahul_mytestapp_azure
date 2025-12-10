using System;
using System.Collections.Generic;

namespace myapp_customerwebapp_azure.Infrastructure;

public partial class Customerly_Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public int? ProductId { get; set; }

    public DateTime OrderDate { get; set; }

    public int? StatusId { get; set; }

    public virtual Customerly_Customer? Customer { get; set; }

    public virtual Customerly_Product? Product { get; set; }

    public virtual Customerly_Status? Status { get; set; }
}
