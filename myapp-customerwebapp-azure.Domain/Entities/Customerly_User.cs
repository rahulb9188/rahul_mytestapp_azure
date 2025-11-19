using System;
using System.Collections.Generic;

namespace myapp_customerwebapp_azure.Domain.Entities;

public partial class Customerly_User
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public int? Role { get; set; }

    public bool? IsApproved { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Customerly_Role? RoleNavigation { get; set; }
}
