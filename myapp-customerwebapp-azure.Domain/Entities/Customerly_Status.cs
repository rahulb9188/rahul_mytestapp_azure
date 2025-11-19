using System;
using System.Collections.Generic;

namespace myapp_customerwebapp_azure.Domain.Entities;

public partial class Customerly_Status
{
    public int StatusId { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Customerly_Order> Customerly_Orders { get; set; } = new List<Customerly_Order>();
}
