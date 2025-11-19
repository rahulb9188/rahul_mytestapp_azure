using System;
using System.Collections.Generic;

namespace myapp_customerwebapp_azure.Domain.Entities;

public partial class Customerly_Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Customerly_Order> Customerly_Orders { get; set; } = new List<Customerly_Order>();
}
