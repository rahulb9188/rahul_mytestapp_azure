using System;
using System.Collections.Generic;

namespace myapp_customerwebapp_azure.Infrastructure.Data;

public partial class CustomerlyUser
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public bool IsApproved { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual CustomerlyCustomer? CustomerlyCustomer { get; set; }
}
